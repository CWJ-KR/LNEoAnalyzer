using System.Diagnostics;
using LnEoAnalyzer.Core;
using TiePie.Hardware;

namespace LnEoAnalyzer.Hardware;

/// <summary>
/// TiePie Handyscope HS5 (오실로스코프 + 신호발생기 내장) 제어.
///  - 신호발생기: DC 신호, 오프셋 = 목표 전압 / 증폭기 이득  →  Matsusada AMJ-1B20 입력
///  - CH1: Thorlabs DET410 광검출기 출력
///  - CH2: AMJ-1B20 전압 모니터 출력
/// 라이브러리: NuGet TiePie.Hardware (libtiepie-hw) — 폴더의 TiePie 예제와 같은 API 사용
/// </summary>
public sealed class TiePieHs5Hardware : EoHardwareBase
{
    private Oscilloscope? _scp;
    private Generator? _gen;
    private string _name = "TiePie Handyscope HS5";
    private double _offsetMin = -12, _offsetMax = 12;
    private double _ch1Range = 8, _ch2Range = 20;
    private bool _ch2Enabled;
    private double _acqTimeSec = 0.01;

    public TiePieHs5Hardware(HardwareConfig config) : base(config) { }

    public override string Name => _name;
    public override bool IsSimulated => false;

    private string _details = "";
    public override string Details => _details;

    public override void Connect()
    {
        lock (Sync)
        {
            if (IsConnected) return;

            DeviceList.Update();
            Oscilloscope? scp = null;
            Generator? gen = null;
            for (var i = 0u; i < DeviceList.Count; i++)
            {
                using var item = DeviceList.GetItemByIndex(i);
                if (item.CanOpen(DeviceType.Oscilloscope) && item.CanOpen(DeviceType.Generator))
                {
                    scp = item.OpenOscilloscope();
                    if (scp.MeasureModes.HasFlag(MeasureMode.Block))
                    {
                        gen = item.OpenGenerator();
                        break;
                    }
                    scp.Dispose();
                    scp = null;
                }
            }

            if (scp is null || gen is null)
            {
                scp?.Dispose();
                gen?.Dispose();
                throw new InvalidOperationException(
                    "신호발생기가 내장된 TiePie 오실로스코프(HS5)를 찾지 못했습니다.\n" +
                    "· USB 케이블과 전원을 확인하세요.\n" +
                    "· TiePie Multi Channel 등 다른 프로그램이 장비를 사용 중이면 종료하세요.\n" +
                    "· 장비 관리자에서 드라이버(WinUSB)가 설치되어 있는지 확인하세요.");
            }

            _scp = scp;
            _gen = gen;
            try
            {
                _name = $"{scp.Name} (S/N {scp.SerialNumber})";

                // 신호발생기: DC 0 V 로 시작
                gen.SignalType = SignalType.DC;
                gen.Offset = 0;
                _offsetMin = gen.OffsetMin;
                _offsetMax = gen.OffsetMax;
                gen.OutputEnable = true;
                gen.Start();
                OutputKv = 0;

                ConfigureScope(_acqTimeSec);
                IsConnected = true;
            }
            catch
            {
                CloseDevices();
                throw;
            }
        }
    }

    public override void Disconnect()
    {
        lock (Sync)
        {
            if (_gen is not null)
            {
                try { _gen.Offset = 0; } catch { }
                try { _gen.Stop(); } catch { }
                try { _gen.OutputEnable = false; } catch { }
            }
            OutputKv = 0;
            CloseDevices();
            IsConnected = false;
        }
    }

    private void CloseDevices()
    {
        try { _scp?.Dispose(); } catch { }
        try { _gen?.Dispose(); } catch { }
        _scp = null;
        _gen = null;
    }

    public override void ConfigureAcquisition(double averageTimeSec)
    {
        lock (Sync)
        {
            if (_scp is null) throw new InvalidOperationException("장비가 연결되어 있지 않습니다.");
            ConfigureScope(averageTimeSec);
        }
    }

    private void ConfigureScope(double averageTimeSec)
    {
        var scp = _scp!;
        scp.MeasureMode = MeasureMode.Block;

        // 가능한 가장 높은 분해능 (16 → 14 → 12 bit 순서로 시도)
        foreach (var bits in new byte[] { 16, 14, 12 })
        {
            try { scp.Resolution = bits; break; } catch { }
        }

        scp.SampleRate = Config.SampleRateHz;
        double fs = scp.SampleRate; // 실제 적용된 값
        ulong len = (ulong)Math.Max(16, Math.Round(averageTimeSec * fs));
        if (len > scp.RecordLengthMax) len = scp.RecordLengthMax;
        scp.RecordLength = len;
        scp.PreSampleRatio = 0;

        // CH1: 광검출기
        var ch1 = scp.Channels[0];
        ch1.Enabled = true;
        ch1.Coupling = Coupling.DCV;
        ch1.Range = Config.OpticalRangeV;
        _ch1Range = ch1.Range;

        // CH2: 증폭기 전압 모니터
        _ch2Enabled = false;
        if (scp.Channels.Count > 1)
        {
            var ch2 = scp.Channels[1];
            ch2.Enabled = Config.UseMonitorChannel;
            if (Config.UseMonitorChannel)
            {
                ch2.Coupling = Coupling.DCV;
                ch2.Range = Config.MonitorRangeV;
                _ch2Range = ch2.Range;
                _ch2Enabled = true;
            }
        }

        // 트리거 없이 즉시 측정 (free-run)
        foreach (var ch in scp.Channels)
        {
            if (ch.Trigger is not null)
                ch.Trigger.Enabled = false;
        }
        for (var i = 0; i < scp.TriggerInputs.Count; i++)
        {
            var ti = scp.TriggerInputs[i];
            if (ti.IsAvailable)
                ti.Enabled = false;
        }
        scp.Trigger.TimeOut = 0; // 0 = 측정 시작 즉시 강제 트리거

        _acqTimeSec = scp.RecordLength / scp.SampleRate;
        _details = string.Format(Num.Inv,
            "샘플링 {0:0.###} MSa/s, 레코드 {1} 샘플 ({2:0.###} ms), CH1 ±{3} V, CH2 {4}",
            scp.SampleRate / 1e6, scp.RecordLength, _acqTimeSec * 1000, _ch1Range,
            _ch2Enabled ? string.Format(Num.Inv, "±{0} V", _ch2Range) : "사용 안 함");
    }

    protected override void ApplyOutputKv(double kv)
    {
        var gen = _gen ?? throw new InvalidOperationException("신호발생기가 연결되어 있지 않습니다.");
        double v = Config.KvToGeneratorV(kv);
        if (v < _offsetMin - 1e-9 || v > _offsetMax + 1e-9)
            throw new ArgumentOutOfRangeException(nameof(kv),
                string.Format(Num.Inv, "신호발생기 오프셋 {0:0.###} V 가 허용 범위({1}~{2} V)를 벗어납니다.", v, _offsetMin, _offsetMax));
        gen.Offset = v;
    }

    public override AcquisitionResult Acquire()
    {
        lock (Sync)
        {
            var scp = _scp ?? throw new InvalidOperationException("장비가 연결되어 있지 않습니다.");
            scp.Start();
            var sw = Stopwatch.StartNew();
            double timeout = 2.0 + 3.0 * _acqTimeSec;
            while (!scp.IsDataReady)
            {
                if (sw.Elapsed.TotalSeconds > timeout)
                    throw new TimeoutException("오실로스코프 측정이 시간 안에 끝나지 않았습니다.");
                Thread.Sleep(1);
            }
            var data = scp.GetData().ToArray(); // [채널][샘플], 꺼진 채널은 null
            var ch1 = ToFloat(data.Length > 0 ? data[0] : null);
            var ch2 = _ch2Enabled && data.Length > 1 ? ToFloat(data[1]) : null;
            return AcquisitionResult.FromSamples(ch1, ch2, _ch1Range, _ch2Range);
        }
    }

    private static float[]? ToFloat(object? o) => o switch
    {
        float[] f => f,
        double[] d => d.Select(x => (float)x).ToArray(),
        _ => null,
    };
}
