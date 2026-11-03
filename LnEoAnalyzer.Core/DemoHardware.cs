using System;
using System.Diagnostics;
using System.Threading;

namespace LnEoAnalyzer.Core;

/// <summary>가상 장비(데모) 응답 파라미터: y = y0 + A·sin(π(V−xc)/w)</summary>
public sealed class DemoParameters
{
    /// <summary>반파장 전압 w (kV)</summary>
    public double VpiKv { get; set; } = 2.08675;
    public double Y0V { get; set; } = 1.49137;
    public double AmplitudeV { get; set; } = 0.86449;
    public double XcKv { get; set; } = 2.66175;
    /// <summary>측정점 평균값의 잡음 (V rms)</summary>
    public double NoiseV { get; set; } = 0.004;
    /// <summary>고전압 증폭기 + 전극의 응답 시정수 (ms). 안정화 시간이 짧으면 값이 덜 따라옵니다.</summary>
    public double TimeConstantMs { get; set; } = 3.0;
    /// <summary>검출기 포화 전압 (V)</summary>
    public double SaturationV { get; set; } = 10.0;
    /// <summary>USB 통신 등 측정점마다의 추가 지연 (ms)</summary>
    public double OverheadMs { get; set; } = 2.0;

    public static DemoParameters Preset1310() => new DemoParameters { VpiKv = 2.08675, Y0V = 1.49137, AmplitudeV = 0.86449, XcKv = 2.66175 };
    public static DemoParameters Preset1550() => new DemoParameters { VpiKv = 2.57086, Y0V = 1.47532, AmplitudeV = 1.09538, XcKv = 1.46252 };

    public double Model(double kv)
    {
        double y = Y0V + AmplitudeV * Math.Sin(Math.PI * (kv - XcKv) / VpiKv);
        return Num.Clamp(y, 0.0, SaturationV);
    }

    public DemoParameters Clone() => (DemoParameters)MemberwiseClone();
}

/// <summary>
/// 장비 없이 전체 측정 흐름을 시험하기 위한 가상 HS5 + AMJ-1B20 + DET410.
/// 실제와 같은 시간이 걸리도록 대기하며, 증폭기 응답 지연과 잡음을 흉내 냅니다.
/// </summary>
public sealed class DemoHardware : EoHardwareBase
{
    private readonly DemoParameters _p;
    private readonly Random _rng;
    private readonly Stopwatch _clock = Stopwatch.StartNew();
    private double _avgSec = 0.01;
    private double _target, _vFrom, _tSet;

    public DemoHardware(HardwareConfig config, DemoParameters parameters, int? seed = null) : base(config)
    {
        _p = parameters.Clone();
        _rng = seed.HasValue ? new Random(seed.Value) : new Random();
    }

    public DemoParameters Parameters => _p;
    public override string Name => "데모 (가상 HS5 + AMJ-1B20 + DET410)";
    public override bool IsSimulated => true;

    public override void Connect()
    {
        lock (Sync)
        {
            Thread.Sleep(150); // 연결 지연 흉내
            _target = _vFrom = 0; _tSet = Now;
            IsConnected = true;
            OutputKv = 0;
        }
    }

    public override void Disconnect()
    {
        lock (Sync)
        {
            if (!IsConnected) return;
            ApplyOutputKv(0);
            OutputKv = 0;
            IsConnected = false;
        }
    }

    public override void ConfigureAcquisition(double averageTimeSec)
    {
        lock (Sync) { _avgSec = Math.Max(1e-4, averageTimeSec); }
    }

    private double Now => _clock.Elapsed.TotalSeconds;

    private double ActualKvAt(double t)
    {
        double tau = Math.Max(1e-6, _p.TimeConstantMs / 1000.0);
        double dt = Math.Max(0, t - _tSet);
        return _target + (_vFrom - _target) * Math.Exp(-dt / tau);
    }

    protected override void ApplyOutputKv(double kv)
    {
        double now = Now;
        _vFrom = ActualKvAt(now);
        _target = kv;
        _tSet = now;
    }

    private double Gauss()
    {
        double u1 = 1.0 - _rng.NextDouble(), u2 = _rng.NextDouble();
        return Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);
    }

    public override AcquisitionResult Acquire()
    {
        lock (Sync)
        {
            if (!IsConnected) throw new InvalidOperationException("장비가 연결되어 있지 않습니다.");
            double t0 = Now;
            PreciseTimer.Delay(_avgSec * 1000.0 + _p.OverheadMs, CancellationToken.None);

            int m = 200;
            int nReal = (int)Math.Max(1, Math.Round(_avgSec * Config.SampleRateHz));
            var opt = new float[m];
            var mon = Config.UseMonitorChannel ? new float[m] : null;
            double sampleNoise = _p.NoiseV * Math.Sqrt(m);
            for (int i = 0; i < m; i++)
            {
                double t = t0 + _avgSec * (i + 0.5) / m;
                double kv = ActualKvAt(t);
                double y = _p.Model(kv) + sampleNoise * Gauss();
                y = Num.Clamp(y, -Config.OpticalRangeV, Config.OpticalRangeV);
                opt[i] = (float)y;
                if (mon != null)
                {
                    double v = kv / Config.MonitorKvPerV + 0.002 * Gauss();
                    mon[i] = (float)Num.Clamp(v, -Config.MonitorRangeV, Config.MonitorRangeV);
                }
            }
            var r = AcquisitionResult.FromSamples(opt, mon, Config.OpticalRangeV, Config.MonitorRangeV);
            r.SampleCount = nReal;
            return r;
        }
    }
}
