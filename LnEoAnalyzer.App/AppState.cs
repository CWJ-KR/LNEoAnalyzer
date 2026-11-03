using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using LnEoAnalyzer.Core;

namespace LnEoAnalyzer;

public enum DeviceKind
{
    /// <summary>가상 장비 (장비 없이 시험)</summary>
    Demo = 0,
    /// <summary>TiePie Handyscope HS5 + AMJ-1B20 + DET410</summary>
    Hs5 = 1,
}

/// <summary>프로그램 설정 (%LocalAppData%\LnEoAnalyzer\settings.ini 에 저장)</summary>
public sealed class AppSettings
{
    public ScanSettings Scan { get; } = new ScanSettings();
    public HardwareConfig Hardware { get; } = new HardwareConfig();
    public DemoParameters Demo { get; } = DemoParameters.Preset1310();
    public AnalysisSettings Analysis { get; } = new AnalysisSettings();
    public MeasurementInfo Info { get; } = new MeasurementInfo();
    public ElectroOptic.Input Eo { get; } = new ElectroOptic.Input();
    public GeneralSettings General { get; } = new GeneralSettings();

    public static string Folder => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "LnEoAnalyzer");
    public static string FilePath => Path.Combine(Folder, "settings.ini");
    public static string HistoryPath => Path.Combine(Folder, "history.csv");

    public static AppSettings Load()
    {
        var s = new AppSettings();
        try
        {
            if (File.Exists(FilePath))
            {
                var d = KeyValueSettings.Parse(File.ReadAllText(FilePath, Encoding.UTF8));
                KeyValueSettings.Read(d, "Scan", s.Scan);
                KeyValueSettings.Read(d, "Hardware", s.Hardware);
                KeyValueSettings.Read(d, "Demo", s.Demo);
                KeyValueSettings.Read(d, "Analysis", s.Analysis);
                KeyValueSettings.Read(d, "Info", s.Info);
                KeyValueSettings.Read(d, "Eo", s.Eo);
                KeyValueSettings.Read(d, "General", s.General);
            }
        }
        catch { /* 손상된 설정 파일은 무시하고 기본값 사용 */ }
        // AMJ-1B20 정격 초과 설정은 허용하지 않음
        if (s.Hardware.MaxOutputKv > HardwareConfig.AmjSpecMaxKv || s.Hardware.MaxOutputKv <= 0)
            s.Hardware.MaxOutputKv = HardwareConfig.AmjSpecMaxKv;
        return s;
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory(Folder);
            var sb = new StringBuilder();
            sb.AppendLine("# LnEoAnalyzer settings");
            KeyValueSettings.Write(sb, "Scan", Scan);
            KeyValueSettings.Write(sb, "Hardware", Hardware);
            KeyValueSettings.Write(sb, "Demo", Demo);
            KeyValueSettings.Write(sb, "Analysis", Analysis);
            KeyValueSettings.Write(sb, "Info", Info);
            KeyValueSettings.Write(sb, "Eo", Eo);
            KeyValueSettings.Write(sb, "General", General);
            File.WriteAllText(FilePath, sb.ToString(), Encoding.UTF8);
        }
        catch { /* 저장 실패는 치명적이지 않음 */ }
    }
}

public sealed class GeneralSettings
{
    public DeviceKind DeviceKind { get; set; } = DeviceKind.Demo;
    public string DataFolder { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LnEoAnalyzer");
    /// <summary>측정 완료 시 CSV 자동 저장</summary>
    public bool AutoSave { get; set; } = true;
    /// <summary>CSV 저장 시 그래프 PNG 도 저장</summary>
    public bool SavePng { get; set; } = true;
    /// <summary>측정 완료 시 결과표에 자동 추가</summary>
    public bool AutoAddHistory { get; set; } = true;
    /// <summary>피팅 곡선을 측정 범위 밖까지 확장해 표시 (PPT 그래프처럼)</summary>
    public bool ExtendFitCurve { get; set; } = true;
    /// <summary>측정점마다 통신 등으로 추가되는 시간 (예상 시간 계산용, 측정 후 자동 갱신)</summary>
    public double OverheadMsHs5 { get; set; } = 15;
    public double OverheadMsDemo { get; set; } = 2.5;
}

/// <summary>화면들이 공유하는 상태</summary>
public sealed class AppState
{
    public AppState(AppSettings settings) { Settings = settings; }

    public AppSettings Settings { get; }
    public IEoHardware? Hardware { get; private set; }
    public List<ResultRecord> History { get; } = new List<ResultRecord>();

    public bool IsConnected => Hardware != null && Hardware.IsConnected;
    /// <summary>측정(스캔) 진행 중</summary>
    public bool IsBusy { get; set; }

    public event EventHandler? HardwareChanged;
    public event EventHandler? HistoryChanged;

    public void SetHardware(IEoHardware? hw)
    {
        Hardware = hw;
        HardwareChanged?.Invoke(this, EventArgs.Empty);
    }

    public void AddRecord(ResultRecord r)
    {
        History.Add(r);
        HistoryChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RaiseHistoryChanged() => HistoryChanged?.Invoke(this, EventArgs.Empty);

    public double OverheadMs =>
        Settings.General.DeviceKind == DeviceKind.Demo ? Settings.General.OverheadMsDemo : Settings.General.OverheadMsHs5;

    public void SaveHistory()
    {
        try
        {
            Directory.CreateDirectory(AppSettings.Folder);
            CsvExport.WriteHistory(AppSettings.HistoryPath, History);
        }
        catch { }
    }

    public void LoadHistory()
    {
        try { History.AddRange(CsvExport.ReadHistory(AppSettings.HistoryPath)); }
        catch { }
    }
}

/// <summary>측정 중 Windows 타이머 해상도를 1 ms 로 높여 대기 시간 정확도를 확보합니다.</summary>
public sealed class HighResTimerScope : IDisposable
{
    [DllImport("winmm.dll")] private static extern uint timeBeginPeriod(uint uMilliseconds);
    [DllImport("winmm.dll")] private static extern uint timeEndPeriod(uint uMilliseconds);

    private readonly bool _active;

    public HighResTimerScope()
    {
        try { _active = Environment.OSVersion.Platform == PlatformID.Win32NT && timeBeginPeriod(1) == 0; }
        catch { _active = false; }
    }

    public void Dispose()
    {
        if (_active) { try { timeEndPeriod(1); } catch { } }
    }
}
