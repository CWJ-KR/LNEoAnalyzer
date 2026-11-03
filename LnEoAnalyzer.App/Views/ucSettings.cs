using System.Globalization;
using LnEoAnalyzer.Core;

namespace LnEoAnalyzer.Views
{
    /// <summary>장비 설정 화면 (증폭기 / 오실로스코프 / 타이밍 / 저장 / 데모)</summary>
    public partial class ucSettings : UserControl
    {
        private AppState? _state;
        private static readonly double[] SampleRates = { 1e5, 5e5, 1e6, 5e6 };

        /// <summary>[적용 및 저장] 후 발생</summary>
        public event EventHandler? SettingsApplied;

        public ucSettings()
        {
            InitializeComponent();
        }

        public void Bind(AppState state)
        {
            _state = state;
            LoadToUi(state.Settings.Hardware, state.Settings.Demo, state.Settings.General, state.Settings.Scan.JumpSettleMs);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (Visible && _state != null) UpdateOverheadLabel();
        }

        private void UpdateOverheadLabel()
        {
            if (_state == null) return;
            var g = _state.Settings.General;
            lblOverhead.Text = string.Format(Num.Inv, "HS5 {0:0.0} ms / 데모 {1:0.0} ms", g.OverheadMsHs5, g.OverheadMsDemo);
        }

        private void LoadToUi(HardwareConfig hw, DemoParameters demo, GeneralSettings g, double jumpSettleMs)
        {
            SetNud(nudGain, hw.AmpGainVPerV);
            SetNud(nudMaxKv, hw.EffectiveMaxKv);
            SelectRange(cboCh1Range, hw.OpticalRangeV);
            chkUseMonitor.Checked = hw.UseMonitorChannel;
            SelectRange(cboCh2Range, hw.MonitorRangeV);
            SetNud(nudMonRatio, hw.MonitorKvPerV);
            int si = Array.FindIndex(SampleRates, r => Math.Abs(r - hw.SampleRateHz) < 1);
            cboSampleRate.SelectedIndex = si >= 0 ? si : 2;

            SetNud(nudJumpSettle, jumpSettleMs);

            txtFolder.Text = g.DataFolder;
            chkAutoSave.Checked = g.AutoSave;
            chkSavePng.Checked = g.SavePng;
            chkAutoHistory.Checked = g.AutoAddHistory;
            chkExtendFit.Checked = g.ExtendFitCurve;

            LoadDemo(demo);
            if (cboPreset.SelectedIndex < 0) cboPreset.SelectedIndex = 0;
            UpdateOverheadLabel();
        }

        private void LoadDemo(DemoParameters d)
        {
            SetNud(nudDemoVpi, d.VpiKv);
            SetNud(nudDemoY0, d.Y0V);
            SetNud(nudDemoA, d.AmplitudeV);
            SetNud(nudDemoXc, d.XcKv);
            SetNud(nudDemoNoise, d.NoiseV);
            SetNud(nudDemoTau, d.TimeConstantMs);
        }

        private static void SetNud(NumericUpDown n, double v)
        {
            if (!Num.IsFinite(v)) return;
            decimal d = (decimal)v;
            if (d < n.Minimum) d = n.Minimum;
            if (d > n.Maximum) d = n.Maximum;
            n.Value = d;
        }

        private static void SelectRange(ComboBox c, double v)
        {
            int best = 0; double bestDiff = double.MaxValue;
            for (int i = 0; i < c.Items.Count; i++)
            {
                double r = double.Parse(c.Items[i]!.ToString()!, CultureInfo.InvariantCulture);
                double diff = Math.Abs(r - v);
                if (diff < bestDiff) { bestDiff = diff; best = i; }
            }
            c.SelectedIndex = best;
        }

        private static double RangeOf(ComboBox c) =>
            double.Parse((c.SelectedItem ?? c.Items[0])!.ToString()!, CultureInfo.InvariantCulture);

        private void btnApply_Click(object? sender, EventArgs e)
        {
            if (_state == null) return;
            if (_state.IsBusy)
            {
                MessageBox.Show("측정 중에는 설정을 바꿀 수 없습니다.", "장비 설정", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var s = _state.Settings;
            var hw = s.Hardware;
            hw.AmpGainVPerV = (double)nudGain.Value;
            hw.MaxOutputKv = Math.Min((double)nudMaxKv.Value, HardwareConfig.AmjSpecMaxKv);
            hw.OpticalRangeV = RangeOf(cboCh1Range);
            hw.UseMonitorChannel = chkUseMonitor.Checked;
            hw.MonitorRangeV = RangeOf(cboCh2Range);
            hw.MonitorKvPerV = (double)nudMonRatio.Value;
            hw.SampleRateHz = SampleRates[Math.Max(0, cboSampleRate.SelectedIndex)];

            s.Scan.JumpSettleMs = (double)nudJumpSettle.Value;

            var g = s.General;
            g.DataFolder = txtFolder.Text.Trim();
            g.AutoSave = chkAutoSave.Checked;
            g.SavePng = chkSavePng.Checked;
            g.AutoAddHistory = chkAutoHistory.Checked;
            g.ExtendFitCurve = chkExtendFit.Checked;

            var d = s.Demo;
            d.VpiKv = (double)nudDemoVpi.Value;
            d.Y0V = (double)nudDemoY0.Value;
            d.AmplitudeV = (double)nudDemoA.Value;
            d.XcKv = (double)nudDemoXc.Value;
            d.NoiseV = (double)nudDemoNoise.Value;
            d.TimeConstantMs = (double)nudDemoTau.Value;

            s.Save();
            SettingsApplied?.Invoke(this, EventArgs.Empty);

            string note = _state.IsConnected && !(_state.Hardware?.IsSimulated ?? true)
                ? "  (레인지·샘플링 속도는 다음 측정부터 적용)"
                : _state.IsConnected ? "  (데모 응답 값은 다시 연결하면 적용)" : "";
            lblApplied.Text = $"{DateTime.Now:HH:mm:ss} 저장됨 → {AppSettings.FilePath}{note}";
        }

        private void btnDefaults_Click(object? sender, EventArgs e)
        {
            var r = MessageBox.Show("장비 설정 화면의 값을 기본값으로 되돌릴까요?\n([적용 및 저장]을 눌러야 실제로 적용됩니다)",
                "기본값", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r != DialogResult.Yes) return;
            var g = new GeneralSettings();
            LoadToUi(new HardwareConfig(), DemoParameters.Preset1310(), g, new ScanSettings().JumpSettleMs);
        }

        private void btnPreset_Click(object? sender, EventArgs e)
        {
            var d = cboPreset.SelectedIndex == 1 ? DemoParameters.Preset1550() : DemoParameters.Preset1310();
            d.NoiseV = (double)nudDemoNoise.Value;
            d.TimeConstantMs = (double)nudDemoTau.Value;
            LoadDemo(d);
        }

        private void btnBrowse_Click(object? sender, EventArgs e)
        {
            using var dlg = new FolderBrowserDialog
            {
                Description = "측정 데이터를 저장할 폴더를 선택하세요",
                SelectedPath = Directory.Exists(txtFolder.Text) ? txtFolder.Text : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            };
            if (dlg.ShowDialog(this) == DialogResult.OK) txtFolder.Text = dlg.SelectedPath;
        }
    }
}
