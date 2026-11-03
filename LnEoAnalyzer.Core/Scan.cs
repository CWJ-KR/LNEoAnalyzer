using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace LnEoAnalyzer.Core;

public enum ScanMode
{
    /// <summary>시작 → 끝</summary>
    OneWay = 0,
    /// <summary>시작 → 끝 → 시작 (왕복)</summary>
    RoundTrip = 1,
}

public enum SweepDirection { Forward = 0, Backward = 1 }

/// <summary>전압 스캔 조건</summary>
public sealed class ScanSettings
{
    public double StartKv { get; set; } = -1.0;
    public double EndKv { get; set; } = 1.0;
    public double StepKv { get; set; } = 0.1;
    /// <summary>전압 변경 후 측정 전 대기 시간 (ms)</summary>
    public double SettleMs { get; set; } = 50;
    /// <summary>측정점 1개의 평균(측정) 시간 (ms)</summary>
    public double AverageMs { get; set; } = 10;
    /// <summary>시작점으로 이동하는 등 큰 전압 변화 후 대기 시간 (ms)</summary>
    public double JumpSettleMs { get; set; } = 300;
    public ScanMode Mode { get; set; } = ScanMode.OneWay;
    /// <summary>반복 횟수 (1 = 반복 없음)</summary>
    public int Repeats { get; set; } = 1;

    public ScanSettings Clone() => (ScanSettings)MemberwiseClone();
}

public sealed class ScanStep
{
    public ScanStep(int index, int repeat, SweepDirection direction, double setKv, bool isJump)
    {
        Index = index; Repeat = repeat; Direction = direction; SetKv = setKv; IsJump = isJump;
    }
    public int Index { get; }
    /// <summary>1부터 시작하는 반복 번호</summary>
    public int Repeat { get; }
    public SweepDirection Direction { get; }
    public double SetKv { get; }
    /// <summary>직전 전압과 차이가 커서 긴 안정화 시간을 쓰는 점</summary>
    public bool IsJump { get; }
}

public static class ScanPlanner
{
    public const int MaxPoints = 20000;

    /// <summary>한 방향 스캔 전압 목록 (끝점 포함)</summary>
    public static List<double> SweepValues(double start, double end, double step)
    {
        if (!Num.IsFinite(start) || !Num.IsFinite(end) || !Num.IsFinite(step))
            throw new ArgumentException("전압 값이 올바르지 않습니다.");
        step = Math.Abs(step);
        if (step <= 0) throw new ArgumentException("전압 간격은 0보다 커야 합니다.");
        double span = end - start;
        if (Math.Abs(span) < 1e-12) throw new ArgumentException("시작 전압과 끝 전압이 같습니다.");
        double sign = Math.Sign(span);
        double nExact = Math.Abs(span) / step;
        int n = (int)Math.Floor(nExact + 1e-6);
        if (n + 1 > MaxPoints) throw new ArgumentException($"측정점이 너무 많습니다 (최대 {MaxPoints}개).");
        var list = new List<double>(n + 2);
        for (int i = 0; i <= n; i++)
            list.Add(Math.Round(start + sign * step * i, 9));
        // 간격으로 나누어떨어지지 않으면 끝점을 추가
        if (Math.Abs(list[list.Count - 1] - end) > step * 1e-4) list.Add(end);
        else list[list.Count - 1] = end;
        return list;
    }

    public static List<ScanStep> Build(ScanSettings s, double maxAbsKv)
    {
        if (s.Repeats < 1) throw new ArgumentException("반복 횟수는 1 이상이어야 합니다.");
        if (Math.Abs(s.StartKv) > maxAbsKv + 1e-9 || Math.Abs(s.EndKv) > maxAbsKv + 1e-9)
            throw new ArgumentException(string.Format(Num.Inv,
                "전압 범위가 출력 제한(±{0:0.###} kV)을 벗어납니다. (AMJ-1B20 정격 ±1 kV)", maxAbsKv));
        if (s.SettleMs < 0 || s.AverageMs <= 0 || s.JumpSettleMs < 0)
            throw new ArgumentException("안정화/평균 시간이 올바르지 않습니다.");

        var fwd = SweepValues(s.StartKv, s.EndKv, s.StepKv);
        var steps = new List<ScanStep>();
        double prev = 0.0; // 측정 시작 전 출력은 0 V
        double jumpThreshold = Math.Abs(s.StepKv) * 1.5 + 1e-9;
        int idx = 0;
        for (int rep = 1; rep <= s.Repeats; rep++)
        {
            foreach (var v in fwd)
            {
                steps.Add(new ScanStep(idx++, rep, SweepDirection.Forward, v, Math.Abs(v - prev) > jumpThreshold));
                prev = v;
            }
            if (s.Mode == ScanMode.RoundTrip)
            {
                for (int i = fwd.Count - 2; i >= 0; i--)
                {
                    double v = fwd[i];
                    steps.Add(new ScanStep(idx++, rep, SweepDirection.Backward, v, Math.Abs(v - prev) > jumpThreshold));
                    prev = v;
                }
            }
            if (steps.Count > MaxPoints) throw new ArgumentException($"측정점이 너무 많습니다 (최대 {MaxPoints}개).");
        }
        return steps;
    }

    /// <summary>예상 소요 시간. overheadMs = 측정점마다 통신 등으로 추가되는 시간</summary>
    public static TimeSpan Estimate(IReadOnlyList<ScanStep> steps, ScanSettings s, double overheadMs)
    {
        double ms = 0;
        foreach (var st in steps)
            ms += (st.IsJump ? Math.Max(s.JumpSettleMs, s.SettleMs) : s.SettleMs) + s.AverageMs + overheadMs;
        return TimeSpan.FromMilliseconds(ms);
    }
}

/// <summary>측정점 1개</summary>
public sealed class MeasurementPoint
{
    public int Index { get; set; }
    public int Repeat { get; set; }
    public SweepDirection Direction { get; set; }
    /// <summary>명령(설정) 전압 (kV)</summary>
    public double SetKv { get; set; }
    /// <summary>CH2 모니터로 측정한 실제 출력 전압 (kV). 없으면 NaN</summary>
    public double MonitorKv { get; set; } = double.NaN;
    public double MonitorStdKv { get; set; } = double.NaN;
    /// <summary>광검출기 출력 평균 (V)</summary>
    public double OpticalV { get; set; }
    public double OpticalStdV { get; set; }
    /// <summary>측정 시작 후 경과 시간 (s)</summary>
    public double TimeSec { get; set; }
    public bool Clipped { get; set; }

    /// <summary>x축 값 (kV): 모니터 사용 시 모니터 값, 없으면 설정값</summary>
    public double X(bool useMonitor) => useMonitor && Num.IsFinite(MonitorKv) ? MonitorKv : SetKv;
}

public sealed class ScanProgress
{
    public ScanProgress(MeasurementPoint point, int done, int total, TimeSpan elapsed, TimeSpan remaining)
    {
        Point = point; Done = done; Total = total; Elapsed = elapsed; Remaining = remaining;
    }
    public MeasurementPoint Point { get; }
    public int Done { get; }
    public int Total { get; }
    public TimeSpan Elapsed { get; }
    public TimeSpan Remaining { get; }
}

public sealed class ScanResult
{
    public List<MeasurementPoint> Points { get; } = new List<MeasurementPoint>();
    public DateTime StartedAt { get; set; }
    public TimeSpan Elapsed { get; set; }
    public bool Cancelled { get; set; }
    public Exception? Error { get; set; }
    /// <summary>측정점마다 (안정화+평균) 외에 추가로 걸린 평균 시간 (ms)</summary>
    public double MeanOverheadMs { get; set; } = double.NaN;
    public int PlannedCount { get; set; }
    public bool Completed => !Cancelled && Error == null && Points.Count == PlannedCount;
}

/// <summary>전압 스캔 측정 루프 (백그라운드 스레드에서 실행)</summary>
public static class MeasurementEngine
{
    public static ScanResult Run(IEoHardware hw, ScanSettings settings, IProgress<ScanProgress>? progress, CancellationToken ct)
    {
        var s = settings.Clone();
        var steps = ScanPlanner.Build(s, hw.Config.EffectiveMaxKv);
        var result = new ScanResult { StartedAt = DateTime.Now, PlannedCount = steps.Count };
        var sw = Stopwatch.StartNew();
        double waitedMs = 0;
        try
        {
            hw.ConfigureAcquisition(s.AverageMs / 1000.0);
            for (int i = 0; i < steps.Count; i++)
            {
                ct.ThrowIfCancellationRequested();
                var st = steps[i];
                hw.SetOutputKv(st.SetKv);
                double settle = st.IsJump ? Math.Max(s.JumpSettleMs, s.SettleMs) : s.SettleMs;
                PreciseTimer.Delay(settle, ct);
                waitedMs += settle + s.AverageMs;
                var acq = hw.Acquire();

                var p = new MeasurementPoint
                {
                    Index = st.Index,
                    Repeat = st.Repeat,
                    Direction = st.Direction,
                    SetKv = st.SetKv,
                    OpticalV = acq.OpticalMeanV,
                    OpticalStdV = acq.OpticalStdV,
                    Clipped = acq.OpticalClipped,
                    TimeSec = sw.Elapsed.TotalSeconds,
                };
                if (Num.IsFinite(acq.MonitorMeanV))
                {
                    p.MonitorKv = acq.MonitorMeanV * hw.Config.MonitorKvPerV;
                    p.MonitorStdKv = acq.MonitorStdV * hw.Config.MonitorKvPerV;
                }
                result.Points.Add(p);

                var elapsed = sw.Elapsed;
                int done = i + 1;
                var remaining = TimeSpan.FromTicks((long)(elapsed.Ticks / (double)done * (steps.Count - done)));
                progress?.Report(new ScanProgress(p, done, steps.Count, elapsed, remaining));
            }
        }
        catch (OperationCanceledException)
        {
            result.Cancelled = true;
        }
        catch (Exception ex)
        {
            result.Error = ex;
        }
        finally
        {
            // 어떤 경우에도 출력을 0 V 로 되돌립니다.
            try { if (hw.IsConnected) hw.SetOutputKv(0.0); }
            catch (Exception ex) { if (result.Error == null) result.Error = ex; }
            result.Elapsed = sw.Elapsed;
        }
        if (result.Points.Count > 0)
        {
            double lastT = result.Points[result.Points.Count - 1].TimeSec * 1000.0;
            result.MeanOverheadMs = Math.Max(0, (lastT - waitedMs) / result.Points.Count);
        }
        return result;
    }
}
