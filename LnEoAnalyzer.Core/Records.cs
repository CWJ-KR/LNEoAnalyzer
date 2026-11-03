using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LnEoAnalyzer.Core;

/// <summary>사용자가 입력하는 측정 조건 (기록용)</summary>
public sealed class MeasurementInfo
{
    public string Sample { get; set; } = "LN";
    public double WavelengthNm { get; set; } = 1310;
    public double TemperatureC { get; set; } = 25;
    public string Memo { get; set; } = "";
    public string DeviceName { get; set; } = "";

    public string Label => string.Format(Num.Inv, "{0} nm @{1}℃", Num.F(WavelengthNm, "0.#"), Num.F(TemperatureC, "0.#"));
}

public enum FitDataSelection
{
    All = 0,
    ForwardOnly = 1,
    BackwardOnly = 2,
}

/// <summary>피팅에 사용할 데이터와 옵션</summary>
public sealed class AnalysisSettings
{
    /// <summary>x축에 CH2 모니터 전압을 사용 (false = 설정 전압)</summary>
    public bool UseMonitorX { get; set; } = false;
    public FitDataSelection Selection { get; set; } = FitDataSelection.All;
    /// <summary>레인지 초과/포화 의심 점을 피팅에서 제외</summary>
    public bool ExcludeClipped { get; set; } = true;
    public bool FixY0 { get; set; }
    public double Y0 { get; set; } = 1.5;
    public bool FixA { get; set; }
    public double A { get; set; } = 1.0;

    public bool IsUsed(MeasurementPoint p)
    {
        if (ExcludeClipped && p.Clipped) return false;
        if (!Num.IsFinite(p.OpticalV)) return false;
        switch (Selection)
        {
            case FitDataSelection.ForwardOnly: return p.Direction == SweepDirection.Forward;
            case FitDataSelection.BackwardOnly: return p.Direction == SweepDirection.Backward;
            default: return true;
        }
    }

    public SineFitOptions ToFitOptions() => new SineFitOptions { FixY0 = FixY0, Y0 = Y0, FixA = FixA, A = A };

    public SineFitResult Fit(IReadOnlyList<MeasurementPoint> points)
    {
        var used = points.Where(IsUsed).ToList();
        return SineFitter.Fit(used.Select(p => p.X(UseMonitorX)).ToList(), used.Select(p => p.OpticalV).ToList(), ToFitOptions());
    }

    public AnalysisSettings Clone() => (AnalysisSettings)MemberwiseClone();
}

/// <summary>결과 기록표의 한 행 (PPT 결과표와 같은 항목 + 전기-광학 계수)</summary>
public sealed class ResultRecord
{
    public DateTime Time { get; set; }
    public string Sample { get; set; } = "";
    public double WavelengthNm { get; set; }
    public double TemperatureC { get; set; }
    public double Y0 { get; set; }
    public double Y0Err { get; set; }
    public double Xc { get; set; }
    public double XcErr { get; set; }
    public double W { get; set; }
    public double WErr { get; set; }
    public double A { get; set; }
    public double AErr { get; set; }
    public double ReducedChiSq { get; set; }
    public double AdjRSquare { get; set; }
    public int N { get; set; }
    public double RefractiveIndex { get; set; } = double.NaN;
    public double ElectrodeGapMm { get; set; } = double.NaN;
    public double LengthMm { get; set; } = double.NaN;
    public double RPmPerV { get; set; } = double.NaN;
    public double RErrPmPerV { get; set; } = double.NaN;
    public double DurationSec { get; set; }
    /// <summary>측정 조건의 메모</summary>
    public string Memo { get; set; } = "";
    public string Note { get; set; } = "";
    public string FilePath { get; set; } = "";

    public string Label => string.Format(Num.Inv, "{0} nm @{1}℃", Num.F(WavelengthNm, "0.#"), Num.F(TemperatureC, "0.#"));

    public static ResultRecord Create(MeasurementInfo info, SineFitResult fit, ElectroOptic.Input? eoIn, ElectroOptic.Output? eoOut, TimeSpan duration, string note)
    {
        var r = new ResultRecord
        {
            Time = DateTime.Now,
            Sample = info.Sample,
            WavelengthNm = info.WavelengthNm,
            TemperatureC = info.TemperatureC,
            Memo = info.Memo,
            Y0 = fit.Y0, Y0Err = fit.Y0Err, Xc = fit.Xc, XcErr = fit.XcErr,
            W = fit.W, WErr = fit.WErr, A = fit.A, AErr = fit.AErr,
            ReducedChiSq = fit.ReducedChiSq, AdjRSquare = fit.AdjRSquare, N = fit.N,
            DurationSec = duration.TotalSeconds,
            Note = note,
        };
        if (eoIn != null)
        {
            r.RefractiveIndex = eoIn.RefractiveIndex; r.ElectrodeGapMm = eoIn.ElectrodeGapMm; r.LengthMm = eoIn.LengthMm;
        }
        if (eoOut != null && eoOut.Valid) { r.RPmPerV = eoOut.RPmPerV; r.RErrPmPerV = eoOut.RErrPmPerV; }
        return r;
    }
}

/// <summary>CSV 저장 (UTF-8 BOM: 엑셀에서 한글이 깨지지 않음)</summary>
public static class CsvExport
{
    public static readonly Encoding Utf8Bom = new UTF8Encoding(true);

    private static string Q(string s)
    {
        s ??= "";
        if (s.IndexOfAny(new[] { ',', '"', '\n', '\r' }) >= 0) return "\"" + s.Replace("\"", "\"\"") + "\"";
        return s;
    }

    private static string D(double v, string fmt = "G8") => Num.F(v, fmt);

    public static string SafeFileName(string s)
    {
        var bad = Path.GetInvalidFileNameChars();
        var sb = new StringBuilder();
        foreach (var ch in s ?? "") sb.Append(bad.Contains(ch) || ch == ' ' ? '_' : ch);
        return sb.ToString();
    }

    public static string DefaultFileName(MeasurementInfo info, DateTime t)
        => SafeFileName(string.Format(Num.Inv, "{0:yyyyMMdd_HHmmss}_{1}_{2}nm_{3}C",
            t, string.IsNullOrWhiteSpace(info.Sample) ? "LN" : info.Sample.Trim(),
            Num.F(info.WavelengthNm, "0.#"), Num.F(info.TemperatureC, "0.#"))) + ".csv";

    /// <summary>
    /// 측정 CSV 저장.
    ///  - 맨 위 '#' 줄: 측정 조건, 피팅 파라미터, 전기-광학 계수
    ///  - 표: 측정점 열 + 각 점의 피팅값/잔차 열 + 피팅 곡선(라인) 열 (Fit curve V, Fit curve Optical)
    ///    피팅 곡선은 그래프에 그린 것과 같은 범위·점 수이며, 측정점보다 행이 많으면 측정점 열은 비워 둡니다.
    /// </summary>
    public static void WriteMeasurement(string path, MeasurementInfo info, ScanSettings scan, HardwareConfig hw,
        ScanResult result, AnalysisSettings analysis, SineFitResult? fit, ElectroOptic.Input? eoIn, ElectroOptic.Output? eoOut,
        bool extendFitCurve = true, int fitCurvePoints = FitCurve.DefaultPoints)
    {
        bool hasFit = fit != null && fit.Success;
        FitCurve.Build(result.Points, analysis, fit, extendFitCurve, fitCurvePoints, out var cx, out var cy);
        string xName = analysis.UseMonitorX ? "V_monitor" : "V_set";

        var sb = new StringBuilder();
        sb.AppendLine("# LN Electro-Optic Measurement (LnEoAnalyzer)");
        sb.AppendLine("# Date," + result.StartedAt.ToString("yyyy-MM-dd HH:mm:ss", Num.Inv));
        sb.AppendLine("# Device," + Q(info.DeviceName));
        sb.AppendLine("# Sample," + Q(info.Sample));
        sb.AppendLine("# Wavelength (nm)," + D(info.WavelengthNm));
        sb.AppendLine("# Temperature (C)," + D(info.TemperatureC));
        sb.AppendLine("# Memo," + Q(info.Memo));
        sb.AppendLine(string.Format(Num.Inv, "# Scan,Start {0} kV,End {1} kV,Step {2} kV,Settle {3} ms,Average {4} ms,Mode {5},Repeats {6}",
            D(scan.StartKv), D(scan.EndKv), D(scan.StepKv), D(scan.SettleMs), D(scan.AverageMs), scan.Mode, scan.Repeats));
        sb.AppendLine(string.Format(Num.Inv, "# Hardware,Amp gain {0} V/V,Monitor {1} kV/V,CH1 range {2} V,CH2 range {3} V,Sample rate {4} Sa/s",
            D(hw.AmpGainVPerV), D(hw.MonitorKvPerV), D(hw.OpticalRangeV), D(hw.MonitorRangeV), D(hw.SampleRateHz)));
        sb.AppendLine(string.Format(Num.Inv, "# Total time (s),{0},Points,{1},Status,{2}",
            D(result.Elapsed.TotalSeconds, "0.000"), result.Points.Count,
            result.Completed ? "Completed" : (result.Cancelled ? "Stopped" : "Error")));
        sb.AppendLine(string.Format(Num.Inv, "# Fit model,y = y0 + A*sin(pi*(x-xc)/w),X axis,{0},Data,{1},Exclude clipped,{2}",
            analysis.UseMonitorX ? "Monitor (CH2)" : "Set voltage", analysis.Selection, analysis.ExcludeClipped));
        if (hasFit)
        {
            sb.AppendLine("# Fit,Value,Standard Error");
            sb.AppendLine("# y0 (V)," + D(fit!.Y0) + "," + D(fit.Y0Err));
            sb.AppendLine("# xc (kV)," + D(fit.Xc) + "," + D(fit.XcErr));
            sb.AppendLine("# w = Vpi (kV)," + D(fit.W) + "," + D(fit.WErr));
            sb.AppendLine("# A (V)," + D(fit.A) + "," + D(fit.AErr));
            sb.AppendLine("# Reduced Chi-Sqr," + D(fit.ReducedChiSq));
            sb.AppendLine("# Adj. R-Square," + D(fit.AdjRSquare));
            if (cx.Length > 0)
                sb.AppendLine(string.Format(Num.Inv, "# Fit curve,{0} points,{1} to {2} kV ({3})",
                    cx.Length, D(cx[0]), D(cx[cx.Length - 1]), xName));
        }
        else
        {
            sb.AppendLine("# Fit,none");
        }
        if (eoIn != null && eoOut != null && eoOut.Valid)
        {
            sb.AppendLine(string.Format(Num.Inv, "# EO coefficient,n {0},d {1} mm,L {2} mm,r {3} pm/V,r err {4} pm/V",
                D(eoIn.RefractiveIndex), D(eoIn.ElectrodeGapMm), D(eoIn.LengthMm), D(eoOut.RPmPerV), D(eoOut.RErrPmPerV)));
        }

        sb.AppendLine("Index,Repeat,Direction,V_set (kV),V_monitor (kV),Optical (V),Optical Std (V),V_monitor Std (kV),Time (s),Clipped,Used in fit," +
                      $"Fit (V),Residual (V),Fit curve {xName} (kV),Fit curve Optical (V)");
        int rows = Math.Max(result.Points.Count, cx.Length);
        for (int i = 0; i < rows; i++)
        {
            if (i < result.Points.Count)
            {
                var p = result.Points[i];
                double yFit = hasFit ? fit!.Evaluate(p.X(analysis.UseMonitorX)) : double.NaN;
                sb.Append(p.Index + 1).Append(',')
                  .Append(p.Repeat).Append(',')
                  .Append(p.Direction == SweepDirection.Forward ? "Forward" : "Backward").Append(',')
                  .Append(D(p.SetKv)).Append(',')
                  .Append(D(p.MonitorKv)).Append(',')
                  .Append(D(p.OpticalV)).Append(',')
                  .Append(D(p.OpticalStdV)).Append(',')
                  .Append(D(p.MonitorStdKv)).Append(',')
                  .Append(D(p.TimeSec, "0.0000")).Append(',')
                  .Append(p.Clipped ? 1 : 0).Append(',')
                  .Append(analysis.IsUsed(p) ? 1 : 0).Append(',')
                  .Append(D(yFit)).Append(',')
                  .Append(D(p.OpticalV - yFit)).Append(',');
            }
            else
            {
                sb.Append(',', 13);
            }
            if (i < cx.Length) sb.Append(D(cx[i])).Append(',').Append(D(cy[i]));
            else sb.Append(',');
            sb.AppendLine();
        }
        var dir = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(dir)) Directory.CreateDirectory(dir);
        File.WriteAllText(path, sb.ToString(), Utf8Bom);
    }

    public static readonly string[] HistoryHeader =
    {
        "Time", "Sample", "Wavelength (nm)", "Temperature (C)", "Memo",
        "y0", "y0 Err", "xc (kV)", "xc Err", "w = Vpi (kV)", "w Err", "A", "A Err",
        "Reduced Chi-Sqr", "Adj. R-Square", "N", "n", "d (mm)", "L (mm)", "r (pm/V)", "r Err (pm/V)",
        "Duration (s)", "Note", "File",
    };

    public static string[] HistoryRow(ResultRecord r) => new[]
    {
        r.Time.ToString("yyyy-MM-dd HH:mm:ss", Num.Inv), r.Sample, D(r.WavelengthNm), D(r.TemperatureC), r.Memo,
        D(r.Y0, "0.#####"), D(r.Y0Err, "0.#####"), D(r.Xc, "0.#####"), D(r.XcErr, "0.#####"),
        D(r.W, "0.#####"), D(r.WErr, "0.#####"), D(r.A, "0.#####"), D(r.AErr, "0.#####"),
        D(r.ReducedChiSq, "0.#####E+0"), D(r.AdjRSquare, "0.#####"), r.N.ToString(Num.Inv),
        D(r.RefractiveIndex), D(r.ElectrodeGapMm), D(r.LengthMm), D(r.RPmPerV, "0.####"), D(r.RErrPmPerV, "0.####"),
        D(r.DurationSec, "0.00"), r.Note, r.FilePath,
    };

    public static void WriteHistory(string path, IEnumerable<ResultRecord> records)
    {
        var sb = new StringBuilder();
        sb.AppendLine(string.Join(",", HistoryHeader.Select(Q)));
        foreach (var r in records) sb.AppendLine(string.Join(",", HistoryRow(r).Select(Q)));
        File.WriteAllText(path, sb.ToString(), Utf8Bom);
    }

    /// <summary>
    /// 결과 기록 CSV 읽기. 열 이름으로 찾으므로 열 순서가 바뀐 예전 파일(Condition 열 포함)도 읽을 수 있습니다.
    /// </summary>
    public static List<ResultRecord> ReadHistory(string path)
    {
        var list = new List<ResultRecord>();
        if (!File.Exists(path)) return list;
        var lines = File.ReadAllLines(path, Encoding.UTF8);
        if (lines.Length == 0) return list;
        var head = SplitLine(lines[0].TrimStart('\uFEFF'));
        var idx = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        for (int i = 0; i < head.Count; i++) idx[head[i].Trim()] = i;

        for (int li = 1; li < lines.Length; li++)
        {
            if (string.IsNullOrWhiteSpace(lines[li])) continue;
            var c = SplitLine(lines[li]);
            string S(string name) => idx.TryGetValue(name, out var k) && k < c.Count ? c[k] : "";
            double Dv(string name) => double.TryParse(S(name), NumberStyles.Float, Num.Inv, out var v) ? v : double.NaN;
            var r = new ResultRecord
            {
                Time = DateTime.TryParse(S("Time"), Num.Inv, DateTimeStyles.None, out var t) ? t : DateTime.MinValue,
                Sample = S("Sample"),
                WavelengthNm = Dv("Wavelength (nm)"),
                TemperatureC = Dv("Temperature (C)"),
                Memo = S("Memo"),
                Y0 = Dv("y0"), Y0Err = Dv("y0 Err"), Xc = Dv("xc (kV)"), XcErr = Dv("xc Err"),
                W = Dv("w = Vpi (kV)"), WErr = Dv("w Err"), A = Dv("A"), AErr = Dv("A Err"),
                ReducedChiSq = Dv("Reduced Chi-Sqr"), AdjRSquare = Dv("Adj. R-Square"),
                N = int.TryParse(S("N"), NumberStyles.Integer, Num.Inv, out var n) ? n : 0,
                RefractiveIndex = Dv("n"), ElectrodeGapMm = Dv("d (mm)"), LengthMm = Dv("L (mm)"),
                RPmPerV = Dv("r (pm/V)"), RErrPmPerV = Dv("r Err (pm/V)"),
                DurationSec = Dv("Duration (s)"), Note = S("Note"), FilePath = S("File"),
            };
            list.Add(r);
        }
        return list;
    }

    /// <summary>CSV 한 줄을 열로 나눕니다 (따옴표 처리 포함)</summary>
    public static List<string> SplitLine(string line)
    {
        var res = new List<string>();
        var sb = new StringBuilder();
        bool q = false;
        for (int i = 0; i < line.Length; i++)
        {
            char ch = line[i];
            if (q)
            {
                if (ch == '"')
                {
                    if (i + 1 < line.Length && line[i + 1] == '"') { sb.Append('"'); i++; }
                    else q = false;
                }
                else sb.Append(ch);
            }
            else if (ch == '"') q = true;
            else if (ch == ',') { res.Add(sb.ToString()); sb.Clear(); }
            else sb.Append(ch);
        }
        res.Add(sb.ToString());
        return res;
    }
}

/// <summary>설정을 "Prefix.Property=값" 형식의 텍스트로 저장/복원 (외부 라이브러리 없이)</summary>
public static class KeyValueSettings
{
    private static IEnumerable<PropertyInfo> Props(object o) =>
        o.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
         .Where(p => p.CanRead && p.CanWrite && p.GetIndexParameters().Length == 0);

    public static void Write(StringBuilder sb, string prefix, object obj)
    {
        foreach (var p in Props(obj))
        {
            object? v = p.GetValue(obj);
            string s = v switch
            {
                null => "",
                double d => d.ToString("R", Num.Inv),
                float f => f.ToString("R", Num.Inv),
                IFormattable fm => fm.ToString(null, Num.Inv),
                _ => v.ToString() ?? "",
            };
            sb.Append(prefix).Append('.').Append(p.Name).Append('=').AppendLine(s.Replace("\r", " ").Replace("\n", " "));
        }
    }

    public static Dictionary<string, string> Parse(string text)
    {
        var d = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var raw in text.Split('\n'))
        {
            var line = raw.TrimEnd('\r');
            if (line.Length == 0 || line[0] == '#') continue;
            int eq = line.IndexOf('=');
            if (eq <= 0) continue;
            d[line.Substring(0, eq).Trim()] = line.Substring(eq + 1);
        }
        return d;
    }

    public static void Read(IDictionary<string, string> dict, string prefix, object obj)
    {
        foreach (var p in Props(obj))
        {
            if (!dict.TryGetValue(prefix + "." + p.Name, out var s)) continue;
            try
            {
                var t = p.PropertyType;
                object? v = null;
                if (t == typeof(string)) v = s;
                else if (t == typeof(double)) v = double.Parse(s, NumberStyles.Float, Num.Inv);
                else if (t == typeof(int)) v = int.Parse(s, NumberStyles.Integer, Num.Inv);
                else if (t == typeof(bool)) v = bool.Parse(s);
                else if (t.IsEnum) v = Enum.Parse(t, s, true);
                if (v != null) p.SetValue(obj, v);
            }
            catch { /* 잘못된 값은 기본값 유지 */ }
        }
    }
}
