using LnEoAnalyzer.Core;
using LnEoAnalyzer.Views;

namespace LnEoAnalyzer
{
    public partial class MainForm : Form
    {
        private readonly AppState _state;
        private readonly ucMeasure _ucMeasure;
        private readonly ucResults _ucResults;
        private readonly ucSettings _ucSettings;
        private UserControl? _currentView;

        private const int borderSize = 2;
        private Size formSize;

        public MainForm() : this(false) { }

        public MainForm(bool startInDemo)
        {
            InitializeComponent();

            var settings = AppSettings.Load();
            if (startInDemo) settings.General.DeviceKind = DeviceKind.Demo;
            _state = new AppState(settings);
            _state.LoadHistory();

            _ucMeasure = new ucMeasure { Dock = DockStyle.Fill, Visible = false };
            _ucResults = new ucResults { Dock = DockStyle.Fill, Visible = false };
            _ucSettings = new ucSettings { Dock = DockStyle.Fill, Visible = false };
            _ucMeasure.Bind(_state);
            _ucResults.Bind(_state);
            _ucSettings.Bind(_state);
            panelDesktop.Controls.Add(_ucMeasure);
            panelDesktop.Controls.Add(_ucResults);
            panelDesktop.Controls.Add(_ucSettings);

            _state.HardwareChanged += (s, e) => UpdateDeviceLabel();
            _ucSettings.SettingsApplied += (s, e) => _ucMeasure.ApplySettingsChange();
            UpdateDeviceLabel();

            Padding = new Padding(borderSize);
            ChangeView(_ucMeasure);
        }

        private void UpdateDeviceLabel()
        {
            var hw = _state.Hardware;
            if (hw != null && hw.IsConnected)
            {
                lblDevice.Text = (hw.IsSimulated ? "● " : "● 연결됨 : ") + hw.Name;
                lblDevice.ForeColor = hw.IsSimulated ? Color.DarkOrange : Color.ForestGreen;
            }
            else
            {
                lblDevice.Text = "○ 장비 연결 안 됨";
                lblDevice.ForeColor = Color.DimGray;
            }
        }

        private void ChangeView(UserControl newView)
        {
            if (_currentView == newView) return;
            if (_currentView != null) _currentView.Visible = false;
            newView.Visible = true;
            newView.BringToFront();
            _currentView = newView;

            foreach (var b in new[] { btnMeasureView, btnResultsView, btnSettingsView })
                b.BackColor = Color.FromArgb(98, 102, 242);
            var active = newView == _ucMeasure ? btnMeasureView : newView == _ucResults ? btnResultsView : btnSettingsView;
            active.BackColor = Color.FromArgb(72, 76, 210);
        }

        private void CollapseMenu()
        {
            var buttons = new[] { btnMeasureView, btnResultsView, btnSettingsView, btnExit };
            if (panelMenu.Width > 100)
            {
                panelMenu.Width = 64;
                lblLogo.Visible = false;
                btnMenu.Location = new Point(12, 14);
                foreach (var b in buttons)
                {
                    b.Text = "";
                    b.ImageAlign = ContentAlignment.MiddleCenter;
                    b.Padding = new Padding(0);
                }
            }
            else
            {
                panelMenu.Width = 190;
                lblLogo.Visible = true;
                btnMenu.Location = new Point(146, 14);
                foreach (var b in buttons)
                {
                    b.Text = "  " + b.Tag;
                    b.ImageAlign = ContentAlignment.MiddleLeft;
                    b.Padding = new Padding(10, 0, 0, 0);
                }
            }
        }

        private void MainForm_Load(object? sender, EventArgs e) => formSize = ClientSize;
        private void btnMenu_Click(object? sender, EventArgs e) => CollapseMenu();
        private void btnMeasureView_Click(object? sender, EventArgs e) => ChangeView(_ucMeasure);
        private void btnResultsView_Click(object? sender, EventArgs e) => ChangeView(_ucResults);
        private void btnSettingsView_Click(object? sender, EventArgs e) => ChangeView(_ucSettings);
        private void btnExit_Click(object? sender, EventArgs e) => Close();
        private void btnClose_Click(object? sender, EventArgs e) => Close();

        private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (_state.IsBusy)
            {
                var r = MessageBox.Show("측정이 진행 중입니다. 측정을 중단하고 종료할까요?\n(출력 전압은 0 V 로 내려갑니다)",
                    "종료 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (r != DialogResult.Yes) { e.Cancel = true; return; }
            }
            try
            {
                // 어떤 방식으로 닫히더라도 고전압 출력을 0 V 로 내리고 장비를 해제합니다.
                _ucMeasure.Shutdown();
                _ucMeasure.StoreToSettings();
                _state.Settings.Save();
                _state.SaveHistory();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("종료 처리 중 오류: " + ex.Message);
            }
        }
    }
}
