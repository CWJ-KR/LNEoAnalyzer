using System;
using System.Collections.Generic;

namespace LnEoAnalyzer.Core;

/// <summary>
/// 장비 설정값. HS5 신호발생기(AWG) → Matsusada AMJ-1B20 → LN 전극,
/// DET410 → HS5 CH1, AMJ 전압 모니터 → HS5 CH2.
/// </summary>
public sealed class HardwareConfig
{
    /// <summary>Matsusada AMJ-1B20 정격 출력 (±1 kV). 소프트웨어는 이 값을 절대 넘지 않습니다.</summary>
    public const double AmjSpecMaxKv = 1.0;

    /// <summary>증폭기 전압 이득 (출력 V / 입력 V). 수동 측정 기록: 10 V 입력 → 1 kV 출력 = 100</summary>
    public double AmpGainVPerV { get; set; } = 100.0;

    /// <summary>소프트웨어 출력 제한 (kV). AMJ 정격(1 kV) 이하로만 설정됩니다.</summary>
    public double MaxOutputKv { get; set; } = 1.0;

    /// <summary>CH2(증폭기 전압 모니터) 사용 여부</summary>
    public bool UseMonitorChannel { get; set; } = true;

    /// <summary>모니터 출력 1 V 가 의미하는 실제 출력 전압 (kV/V). 1/100 모니터이면 0.1</summary>
    public double MonitorKvPerV { get; set; } = 0.1;

    /// <summary>CH1(광검출기) 입력 레인지 (V)</summary>
    public double OpticalRangeV { get; set; } = 8.0;

    /// <summary>CH2(전압 모니터) 입력 레인지 (V)</summary>
    public double MonitorRangeV { get; set; } = 20.0;

    /// <summary>오실로스코프 샘플링 속도 (Sa/s)</summary>
    public double SampleRateHz { get; set; } = 1.0e6;

    /// <summary>실제 적용되는 출력 제한 (kV) = min(설정값, AMJ 정격)</summary>
    public double EffectiveMaxKv => Math.Min(Math.Abs(MaxOutputKv), AmjSpecMaxKv);

    /// <summary>목표 출력 전압(kV) → 신호발생기 DC 오프셋(V)</summary>
    public double KvToGeneratorV(double kv) => kv * 1000.0 / AmpGainVPerV;

    public HardwareConfig Clone() => (HardwareConfig)MemberwiseClone();
}

/// <summary>블록 측정 1회의 통계 결과</summary>
public sealed class AcquisitionResult
{
    public double OpticalMeanV { get; set; } = double.NaN;
    public double OpticalStdV { get; set; } = double.NaN;
    public double OpticalMinV { get; set; } = double.NaN;
    public double OpticalMaxV { get; set; } = double.NaN;
    public double MonitorMeanV { get; set; } = double.NaN;
    public double MonitorStdV { get; set; } = double.NaN;
    public int SampleCount { get; set; }
    /// <summary>CH1 신호가 입력 레인지 끝(±99%)에 닿음 → 레인지 초과 또는 검출기 포화 의심</summary>
    public bool OpticalClipped { get; set; }

    public static void Stats(IReadOnlyList<float>? data, double range, out double mean, out double std,
        out double min, out double max, out bool clipped)
    {
        mean = std = min = max = double.NaN; clipped = false;
        if (data == null || data.Count == 0) return;
        double s = 0, s2 = 0, mn = double.MaxValue, mx = double.MinValue;
        int n = 0;
        double lim = Math.Abs(range) * 0.99;
        for (int i = 0; i < data.Count; i++)
        {
            double v = data[i];
            if (double.IsNaN(v)) continue;
            n++; s += v; s2 += v * v;
            if (v < mn) mn = v;
            if (v > mx) mx = v;
            if (range > 0 && Math.Abs(v) >= lim) clipped = true;
        }
        if (n == 0) return;
        mean = s / n;
        double var = n > 1 ? (s2 - n * mean * mean) / (n - 1) : 0;
        std = Math.Sqrt(Math.Max(0, var));
        min = mn; max = mx;
    }

    public static AcquisitionResult FromSamples(IReadOnlyList<float>? optical, IReadOnlyList<float>? monitor,
        double opticalRange, double monitorRange)
    {
        var r = new AcquisitionResult();
        Stats(optical, opticalRange, out var m, out var s, out var mn, out var mx, out var clip);
        r.OpticalMeanV = m; r.OpticalStdV = s; r.OpticalMinV = mn; r.OpticalMaxV = mx; r.OpticalClipped = clip;
        r.SampleCount = optical?.Count ?? 0;
        if (monitor != null)
        {
            Stats(monitor, monitorRange, out var mm, out var ms, out _, out _, out _);
            r.MonitorMeanV = mm; r.MonitorStdV = ms;
        }
        return r;
    }
}

/// <summary>측정 시스템(전압 출력 + 2채널 측정) 추상화. 실제 HS5 와 데모 장비가 구현합니다.</summary>
public interface IEoHardware : IDisposable
{
    string Name { get; }
    bool IsSimulated { get; }
    bool IsConnected { get; }
    HardwareConfig Config { get; }
    /// <summary>마지막으로 명령한 출력 전압 (kV)</summary>
    double OutputKv { get; }

    void Connect();
    /// <summary>출력을 0 V 로 내리고 장비를 닫습니다.</summary>
    void Disconnect();
    /// <summary>측정점 1개당 평균 시간(초)에 맞춰 레코드 길이 등을 설정합니다.</summary>
    void ConfigureAcquisition(double averageTimeSec);
    /// <summary>LN 인가 전압(kV) 설정. 제한을 넘으면 ArgumentOutOfRangeException.</summary>
    void SetOutputKv(double kv);
    /// <summary>블록 측정 1회 (CH1 광신호, CH2 모니터)</summary>
    AcquisitionResult Acquire();
}

/// <summary>공통 제한 검사 / 잠금</summary>
public abstract class EoHardwareBase : IEoHardware
{
    protected readonly object Sync = new object();

    protected EoHardwareBase(HardwareConfig config) { Config = config; }

    public abstract string Name { get; }
    public abstract bool IsSimulated { get; }
    public bool IsConnected { get; protected set; }
    public HardwareConfig Config { get; }
    public double OutputKv { get; protected set; }
    /// <summary>연결 후 실제 적용된 설정 요약 (로그 표시용)</summary>
    public virtual string Details => "";

    public abstract void Connect();
    public abstract void Disconnect();
    public abstract void ConfigureAcquisition(double averageTimeSec);
    public abstract AcquisitionResult Acquire();

    public void SetOutputKv(double kv)
    {
        CheckLimit(kv);
        lock (Sync)
        {
            if (!IsConnected) throw new InvalidOperationException("장비가 연결되어 있지 않습니다.");
            ApplyOutputKv(kv);
            OutputKv = kv;
        }
    }

    protected abstract void ApplyOutputKv(double kv);

    public void CheckLimit(double kv)
    {
        double max = Config.EffectiveMaxKv;
        if (!Num.IsFinite(kv) || Math.Abs(kv) > max + 1e-9)
            throw new ArgumentOutOfRangeException(nameof(kv),
                string.Format(Num.Inv, "요청 전압 {0:0.###} kV 가 출력 제한 ±{1:0.###} kV 를 벗어납니다.", kv, max));
    }

    public virtual void Dispose()
    {
        try { if (IsConnected) Disconnect(); } catch { /* 종료 중 예외 무시 */ }
    }
}
