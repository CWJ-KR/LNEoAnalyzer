using System.Diagnostics;
using LnEoAnalyzer.Core;
using LnEoAnalyzer.Hardware;
using Alignment = ScottPlot.Alignment;
using LinePattern = ScottPlot.LinePattern;
using MarkerShape = ScottPlot.MarkerShape;
using Plot = ScottPlot.Plot;
using SPColor = ScottPlot.Color;

namespace LnEoAnalyzer.Views
{
    /// <summary>
    /// 반파장 전압 측정 화면
    ///  1) 설정한 범위/간격으로 전압을 바꾸며 광검출기 신호를 측정 (실시간 그래프)
    ///  2) 측정이 모두 끝난 뒤 y = y0 + A·sin(π(x−xc)/w) 로 1회 피팅 → w = Vπ
    ///  3) r = λd/(n³LVπ) 로 전기-광학 계수 계산
    /// </summary>
    public partial class ucMeasure : UserControl
    {
        private AppState? _state;
        private CancellationTokenSource? _cts;
        private Task<ScanResult>? _scanTask;
        private readonly List<MeasurementPoint> _points = new List<MeasurementPoint>();
        private readonly Stopwatch _scanClock = new Stopwatch();
        private ScanResult? _lastResult;
        private ScanSettings? _lastScan;
        private MeasurementInfo? _lastInfo;
        private SineFitResult? _lastFit;
        private ElectroOptic.Output? _lastEo;
        private string? _lastSavedPath;
        private double _currentKv = double.NaN;
        private bool _loading;
        private bool _liveBusy;

        private static readonly SPColor ColForward = new SPColor((byte)50, (byte)50, (byte)50, (byte)255);
        private static readonly SPColor ColBackward = new SPColor((byte)229, (byte)57, (byte)53, (byte)255);
        private static readonly SPColor ColExcluded = new SPColor((byte)255, (byte)152, (byte)0, (byte)255);
        private static readonly SPColor ColFit = new SPColor((byte)220, (byte)20, (byte)20, (byte)255);
        private static readonly SPColor ColCursor = new SPColor((byte)98, (byte)102, (byte)242, (byte)255);

        public ucMeasure()
        {
            InitializeComponent();
        }

        private AppState State => _state ?? throw new InvalidOperationException("Bind() 가 호출되지 않았습니다.");

        public void Bind(AppState state)
        {
            _state = state;
            _state.HardwareChanged += (s, e) => { UpdateUiState(); UpdateHvLabel(); UpdatePlan(); };
            ReloadFromSettings();
            RedrawPlot();
            UpdateUiState();
            Log("프로그램 시작. 장비를 선택하고 [연결]을 누르세요. 장비 없이 시험하려면 '데모 (가상 장비)'를 선택하세요.");
        }

        // =====================================================================
        //  설정 ↔ 화면
        // =====================================================================
        public void ReloadFromSettings()
        {
            if (_state == null) return;
            var s = State.Settings;
            _loading = true;
            try
            {
                double max = s.Hardware.EffectiveMaxKv;
                foreach (var n in new[] { nudStart, nudEnd, nudManualKv })
                {
                    n.Minimum = (decimal)(-max);
                    n.Maximum = (decimal)max;
                }
                cboDevice.SelectedIndex = s.General.DeviceKind == DeviceKind.Hs5 ? 0 : 1;
                txtSample.Text = s.Info.Sample;
                SetNud(nudWavelength, s.Info.WavelengthNm);
                SetNud(nudTemperature, s.Info.TemperatureC);
                txtMemo.Text = s.Info.Memo;

                SetNud(nudStart, s.Scan.StartKv);
                SetNud(nudEnd, s.Scan.EndKv);
                SetNud(nudStep, s.Scan.StepKv);
                SetNud(nudSettle, s.Scan.SettleMs);
                SetNud(nudAverage, s.Scan.AverageMs);
                cboMode.SelectedIndex = s.Scan.Mode == ScanMode.RoundTrip ? 1 : 0;
                SetNud(nudRepeats, s.Scan.Repeats);

                cboXSource.SelectedIndex = s.Analysis.UseMonitorX ? 1 : 0;
                cboSelection.SelectedIndex = (int)s.Analysis.Selection;
                chkExclude.Checked = s.Analysis.ExcludeClipped;
                chkFixY0.Checked = s.Analysis.FixY0;
                SetNud(nudFixY0, s.Analysis.Y0);
                chkFixA.Checked = s.Analysis.FixA;
                SetNud(nudFixA, s.Analysis.A);

                SetNud(nudN, s.Eo.RefractiveIndex);
                SetNud(nudD, s.Eo.ElectrodeGapMm);
                SetNud(nudL, s.Eo.LengthMm);
            }
            finally { _loading = false; }
            UpdatePlan();
            UpdateEo();
            UpdateUiState();
        }

        /// <summary>장비 설정 화면에서 설정이 바뀌었을 때: 현재 입력값을 보존한 채 제한값 등을 다시 적용</summary>
        public void ApplySettingsChange()
        {
            StoreToSettings();
            ReloadFromSettings();
            RedrawPlot();
        }

        public void StoreToSettings()
        {
            if (_state == null) return;
            var s = State.Settings;
            s.General.DeviceKind = cboDevice.SelectedIndex == 0 ? DeviceKind.Hs5 : DeviceKind.Demo;
            var info = ReadInfo();
            s.Info.Sample = info.Sample; s.Info.WavelengthNm = info.WavelengthNm; s.Info.TemperatureC = info.TemperatureC; s.Info.Memo = info.Memo;
            var scan = ReadScanSettings();
            s.Scan.StartKv = scan.StartKv; s.Scan.EndKv = scan.EndKv; s.Scan.StepKv = scan.StepKv;
            s.Scan.SettleMs = scan.SettleMs; s.Scan.AverageMs = scan.AverageMs; s.Scan.Mode = scan.Mode; s.Scan.Repeats = scan.Repeats;
            var an = ReadAnalysis();
            s.Analysis.UseMonitorX = an.UseMonitorX; s.Analysis.Selection = an.Selection; s.Analysis.ExcludeClipped = an.ExcludeClipped;
            s.Analysis.FixY0 = an.FixY0; s.Analysis.Y0 = an.Y0; s.Analysis.FixA = an.FixA; s.Analysis.A = an.A;
            s.Eo.RefractiveIndex = (double)nudN.Value; s.Eo.ElectrodeGapMm = (double)nudD.Value; s.Eo.LengthMm = (double)nudL.Value;
            s.Eo.WavelengthNm = info.WavelengthNm;
        }

        private static void SetNud(NumericUpDown n, double v)
        {
            if (!Num.IsFinite(v)) return;
            decimal d = (decimal)v;
            if (d < n.Minimum) d = n.Minimum;
            if (d > n.Maximum) d = n.Maximum;
            n.Value = d;
        }

        private MeasurementInfo ReadInfo() => new MeasurementInfo
        {
            Sample = txtSample.Text.Trim(),
            WavelengthNm = (double)nudWavelength.Value,
            TemperatureC = (double)nudTemperature.Value,
            Memo = txtMemo.Text.Trim(),
            DeviceName = _state?.Hardware?.Name ?? "",
        };

        private ScanSettings ReadScanSettings() => new ScanSettings
        {
            StartKv = (double)nudStart.Value,
            EndKv = (double)nudEnd.Value,
            StepKv = (double)nudStep.Value,
            SettleMs = (double)nudSettle.Value,
            AverageMs = (double)nudAverage.Value,
            JumpSettleMs = _state?.Settings.Scan.JumpSettleMs ?? 300,
            Mode = cboMode.SelectedIndex == 1 ? ScanMode.RoundTrip : ScanMode.OneWay,
            Repeats = (int)nudRepeats.Value,
        };

        private AnalysisSettings ReadAnalysis() => new AnalysisSettings
        {
            UseMonitorX = cboXSource.SelectedIndex == 1,
            Selection = cboSelection.SelectedIndex < 0 ? FitDataSelection.All : (FitDataSelection)cboSelection.SelectedIndex,
            ExcludeClipped = chkExclude.Checked,
            FixY0 = chkFixY0.Checked,
            Y0 = (double)nudFixY0.Value,
            FixA = chkFixA.Checked,
            A = (double)nudFixA.Value,
        };

        // =====================================================================
        //  화면 상태
        // =====================================================================
        private void UpdateUiState()
        {
            if (_state == null) return;
            bool conn = State.IsConnected, busy = State.IsBusy;
            cboDevice.Enabled = !conn && !busy;
            btnConnect.Enabled = !conn && !busy;
            btnDisconnect.Enabled = conn && !busy;
            btnStart.Enabled = conn && !busy;
            btnStop.Enabled = busy;
            grpInfo.Enabled = !busy;
            grpScan.Enabled = !busy;
            grpFit.Enabled = !busy;
            nudManualKv.Enabled = btnApplyOutput.Enabled = btnZero.Enabled = conn && !busy;
            chkLive.Enabled = conn && !busy;
            btnSaveCsv.Enabled = !busy && _lastResult != null && _lastResult.Points.Count > 0;
            btnAddHistory.Enabled = !busy && _lastFit != null;
            nudFixY0.Enabled = chkFixY0.Checked;
            nudFixA.Enabled = chkFixA.Checked;
            btnStart.BackColor = btnStart.Enabled ? System.Drawing.Color.FromArgb(46, 160, 67) : System.Drawing.Color.FromArgb(160, 200, 170);
            btnStop.BackColor = btnStop.Enabled ? System.Drawing.Color.FromArgb(220, 53, 69) : System.Drawing.Color.FromArgb(230, 170, 175);
            if (!conn) { chkLive.Checked = false; lblLive.Text = "-"; }
        }

        private void UpdateHvLabel(double? kvOverride = null)
        {
            double kv = kvOverride ?? (_state?.Hardware != null && _state.Hardware.IsConnected ? _state.Hardware.OutputKv : 0.0);
            lblHv.Text = string.Format(Num.Inv, "출력 {0:+0.000;-0.000;0.000} kV", kv);
            lblHv.ForeColor = Math.Abs(kv) > 0.0005 ? System.Drawing.Color.Red : System.Drawing.Color.DimGray;
        }

        private void UpdatePlan()
        {
            if (_state == null || _loading) return;
            try
            {
                var s = ReadScanSettings();
                var steps = ScanPlanner.Build(s, State.Settings.Hardware.EffectiveMaxKv);
                var est = ScanPlanner.Estimate(steps, s, State.OverheadMs);
                lblPlan.ForeColor = System.Drawing.Color.DarkBlue;
                lblPlan.Text = $"측정점 {steps.Count}개 · 예상 소요 {Num.FormatDuration(est)}";
                if (!State.IsBusy) lblEstimate.Text = Num.FormatDuration(est);
            }
            catch (ArgumentException ex)
            {
                lblPlan.ForeColor = System.Drawing.Color.Red;
                lblPlan.Text = ex.Message;
            }
        }

        private void Log(string msg)
        {
            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}{Environment.NewLine}");
        }

        // =====================================================================
        //  장비 연결
        // =====================================================================
        private void cboDevice_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_loading || _state == null) return;
            State.Settings.General.DeviceKind = cboDevice.SelectedIndex == 0 ? DeviceKind.Hs5 : DeviceKind.Demo;
            UpdatePlan();
        }

        private async void btnConnect_Click(object? sender, EventArgs e)
        {
            if (_state == null || State.IsConnected) return;
            StoreToSettings();
            var s = State.Settings;
            IEoHardware hw;
            try
            {
                hw = s.General.DeviceKind == DeviceKind.Demo
                    ? new DemoHardware(s.Hardware, s.Demo)
                    : CreateHs5(s.Hardware);
            }
            catch (Exception ex)
            {
                string msg = DescribeError(ex);
                Log("연결 실패: " + msg.Replace("\n", " "));
                MessageBox.Show(msg, "연결 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            btnConnect.Enabled = false;
            cboDevice.Enabled = false;
            Cursor = Cursors.WaitCursor;
            Log($"연결 중: {(hw.IsSimulated ? "데모 (가상 장비)" : "TiePie HS5")} ...");
            try
            {
                await Task.Run(() => hw.Connect());
                State.SetHardware(hw);
                Log("연결됨: " + hw.Name);
                if (hw is EoHardwareBase hb && hb.Details.Length > 0) Log("  " + hb.Details);
                if (hw.IsSimulated)
                    Log(string.Format(Num.Inv, "  가상 장비 응답: Vπ = {0} kV, y0 = {1} V, A = {2} V, xc = {3} kV, 잡음 {4} V",
                        s.Demo.VpiKv, s.Demo.Y0V, s.Demo.AmplitudeV, s.Demo.XcKv, s.Demo.NoiseV));
            }
            catch (Exception ex)
            {
                try { hw.Dispose(); } catch { }
                string msg = DescribeError(ex);
                Log("연결 실패: " + msg.Replace("\n", " "));
                MessageBox.Show(msg, "연결 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                UpdateUiState();
                UpdateHvLabel();
            }
        }

        /// <summary>TiePie 라이브러리 로딩 실패를 다른 장비 코드와 분리하기 위해 별도 메서드로 둡니다.</summary>
        private static IEoHardware CreateHs5(HardwareConfig cfg) => new TiePieHs5Hardware(cfg);

        private static string DescribeError(Exception ex)
        {
            var inner = ex;
            while (inner.InnerException != null && (inner is TypeInitializationException || inner is AggregateException))
                inner = inner.InnerException;
            if (inner is DllNotFoundException || inner is BadImageFormatException || inner is FileNotFoundException)
                return "TiePie 라이브러리(libtiepie-hw.dll)를 불러오지 못했습니다.\n" +
                       "· 프로젝트를 x64 로 빌드했는지 확인하세요.\n" +
                       "· NuGet 패키지 TiePie.Hardware 가 복원되었는지 확인하세요.\n" +
                       "· 그래도 안 되면 TiePie 사이트의 LibTiePie HW SDK 를 설치하세요.\n\n" + inner.Message;
            return inner.Message;
        }

        private async void btnDisconnect_Click(object? sender, EventArgs e)
        {
            await DisconnectAsync();
        }

        private async Task DisconnectAsync()
        {
            if (_state?.Hardware == null) return;
            chkLive.Checked = false;
            await WaitLiveIdle();
            var hw = State.Hardware!;
            try { await Task.Run(() => hw.Disconnect()); }
            catch (Exception ex) { Log("해제 중 오류: " + ex.Message); }
            try { hw.Dispose(); } catch { }
            State.SetHardware(null);
            Log("장비 연결 해제 (출력 0 V, 신호발생기 출력 OFF)");
            UpdateUiState();
            UpdateHvLabel();
        }

        /// <summary>프로그램 종료 시: 측정 중단 → 0 V → 장비 해제</summary>
        public void Shutdown()
        {
            try { liveTimer.Stop(); uiTimer.Stop(); } catch { }
            try
            {
                _cts?.Cancel();
                _scanTask?.Wait(3000);
            }
            catch { }
            var hw = _state?.Hardware;
            if (hw != null)
            {
                try { hw.Disconnect(); } catch { }
                try { hw.Dispose(); } catch { }
            }
        }

        // =====================================================================
        //  수동 출력 / 실시간 광신호
        // =====================================================================
        private async void btnApplyOutput_Click(object? sender, EventArgs e)
        {
            await SetManualOutput((double)nudManualKv.Value);
        }

        private async void btnZero_Click(object? sender, EventArgs e)
        {
            await SetManualOutput(0.0);
        }

        private async Task SetManualOutput(double kv)
        {
            var hw = _state?.Hardware;
            if (hw == null || !hw.IsConnected || State.IsBusy) return;
            try
            {
                await Task.Run(() => hw.SetOutputKv(kv));
                Log(string.Format(Num.Inv, "수동 출력: {0:+0.000;-0.000;0.000} kV (신호발생기 {1:+0.000;-0.000;0.000} V)", kv, hw.Config.KvToGeneratorV(kv)));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "출력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            UpdateHvLabel();
        }

        private async void chkLive_CheckedChanged(object? sender, EventArgs e)
        {
            var hw = _state?.Hardware;
            if (!chkLive.Checked || hw == null || !hw.IsConnected)
            {
                liveTimer.Stop();
                if (!chkLive.Checked) lblLive.Text = "-";
                return;
            }
            try
            {
                double avg = (double)nudAverage.Value / 1000.0;
                await Task.Run(() => hw.ConfigureAcquisition(avg));
                liveTimer.Start();
            }
            catch (Exception ex)
            {
                chkLive.Checked = false;
                Log("실시간 측정 오류: " + ex.Message);
            }
        }

        private async void liveTimer_Tick(object? sender, EventArgs e)
        {
            var hw = _state?.Hardware;
            if (_liveBusy || hw == null || !hw.IsConnected || State.IsBusy) return;
            _liveBusy = true;
            try
            {
                var r = await Task.Run(() => hw.Acquire());
                string t = string.Format(Num.Inv, "{0:0.0000} V", r.OpticalMeanV);
                if (Num.IsFinite(r.MonitorMeanV))
                    t += string.Format(Num.Inv, "  (모니터 {0:+0.000;-0.000;0.000} kV)", r.MonitorMeanV * hw.Config.MonitorKvPerV);
                if (r.OpticalClipped) t += "  ⚠ 레인지 초과";
                lblLive.Text = t;
                lblLive.ForeColor = r.OpticalClipped ? System.Drawing.Color.Red : System.Drawing.Color.DarkBlue;
            }
            catch (Exception ex)
            {
                chkLive.Checked = false;
                Log("실시간 측정 오류: " + ex.Message);
            }
            finally { _liveBusy = false; }
        }

        private async Task WaitLiveIdle()
        {
            liveTimer.Stop();
            for (int i = 0; i < 100 && _liveBusy; i++) await Task.Delay(20);
        }

        // =====================================================================
        //  측정
        // =====================================================================
        private async void btnStart_Click(object? sender, EventArgs e)
        {
            if (_state == null || State.IsBusy) return;
            var hw = State.Hardware;
            if (hw == null || !hw.IsConnected)
            {
                MessageBox.Show("먼저 장비를 연결하세요.\n장비가 없으면 '데모 (가상 장비)'를 선택한 뒤 [연결]을 누르세요.",
                    "측정", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            StoreToSettings();
            State.Settings.Save();
            var scan = ReadScanSettings();
            List<ScanStep> steps;
            try { steps = ScanPlanner.Build(scan, hw.Config.EffectiveMaxKv); }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "측정 조건 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            chkLive.Checked = false;
            await WaitLiveIdle();

            // 초기화
            _points.Clear();
            _lastFit = null; _lastEo = null; _lastResult = null; _lastSavedPath = null;
            _lastScan = scan;
            _lastInfo = ReadInfo();
            ClearFitDisplay();
            lblSaved.Text = "";
            var estimate = ScanPlanner.Estimate(steps, scan, State.OverheadMs);
            progressBar.Maximum = steps.Count;
            progressBar.Value = 0;
            lblEstimate.Text = Num.FormatDuration(estimate);
            lblElapsed.Text = Num.FormatDuration(TimeSpan.Zero);
            lblRemaining.Text = Num.FormatDuration(estimate);

            State.IsBusy = true;
            UpdateUiState();
            RedrawPlot();

            Log(string.Format(Num.Inv,
                "측정 시작: {0:+0.000;-0.000;0} → {1:+0.000;-0.000;0} kV, 간격 {2} kV, {3}, 반복 {4}회, 안정화 {5} ms, 평균 {6} ms → {7}점, 예상 {8}",
                scan.StartKv, scan.EndKv, scan.StepKv, scan.Mode == ScanMode.RoundTrip ? "왕복" : "단방향", scan.Repeats,
                scan.SettleMs, scan.AverageMs, steps.Count, Num.FormatDuration(estimate)));

            _cts = new CancellationTokenSource();
            var token = _cts.Token;
            var progress = new Progress<ScanProgress>(OnProgress);
            _scanClock.Restart();
            uiTimer.Start();
            ScanResult result;
            using (new HighResTimerScope())
            {
                _scanTask = Task.Run(() => MeasurementEngine.Run(hw, scan, progress, token));
                result = await _scanTask;
            }
            uiTimer.Stop();
            _scanClock.Stop();
            _cts.Dispose();
            _cts = null;
            State.IsBusy = false;
            _currentKv = double.NaN;

            // 결과 정리 (Progress 콜백 지연과 무관하게 최종 데이터로 교체)
            _lastResult = result;
            _points.Clear();
            _points.AddRange(result.Points);
            progressBar.Value = Math.Min(progressBar.Maximum, result.Points.Count);
            lblElapsed.Text = Num.FormatDuration(result.Elapsed);
            lblRemaining.Text = Num.FormatDuration(TimeSpan.Zero);
            lblTotal.Text = Num.FormatDuration(result.Elapsed);

            if (result.Completed && result.Points.Count >= 5 && Num.IsFinite(result.MeanOverheadMs))
            {
                // 다음 예상 시간 계산을 위해 측정점당 추가 지연을 학습
                var g = State.Settings.General;
                if (hw.IsSimulated) g.OverheadMsDemo = 0.5 * g.OverheadMsDemo + 0.5 * result.MeanOverheadMs;
                else g.OverheadMsHs5 = 0.5 * g.OverheadMsHs5 + 0.5 * result.MeanOverheadMs;
            }

            if (result.Error != null)
            {
                Log($"오류로 측정 중단 ({result.Points.Count}/{result.PlannedCount}점): {result.Error.Message} → 출력 0 V");
                MessageBox.Show(DescribeError(result.Error), "측정 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (result.Cancelled)
                Log($"사용자 정지 ({result.Points.Count}/{result.PlannedCount}점, {Num.FormatDuration(result.Elapsed)}) → 출력 0 V");
            else
                Log(string.Format(Num.Inv, "측정 완료: {0}점, 총 소요 {1} (예상 {2}), 측정점당 추가 지연 {3:0.0} ms → 출력 0 V 복귀",
                    result.Points.Count, Num.FormatDuration(result.Elapsed), Num.FormatDuration(estimate), result.MeanOverheadMs));

            int clipped = result.Points.Count(p => p.Clipped);
            if (clipped > 0)
                Log($"  주의: {clipped}개 점에서 CH1 신호가 입력 레인지 끝에 닿았습니다 (레인지 초과 또는 검출기 포화). 장비 설정에서 CH1 레인지를 확인하세요.");
            LogMonitorCheck(result.Points);

            // ---- 모든 점이 찍힌 뒤 한 번만 피팅 ----
            RunFit(logResult: true);

            var gs = State.Settings.General;
            if (result.Points.Count > 0 && gs.AutoSave)
            {
                try
                {
                    string path = Path.Combine(gs.DataFolder, CsvExport.DefaultFileName(_lastInfo, result.StartedAt));
                    SaveMeasurement(path);
                }
                catch (Exception ex) { Log("자동 저장 실패: " + ex.Message); }
            }
            if (result.Completed && _lastFit != null && gs.AutoAddHistory)
                AddHistory("자동");

            UpdateUiState();
            UpdateHvLabel();
            UpdatePlan();
        }

        private void OnProgress(ScanProgress p)
        {
            if (_cts == null) return; // 측정 종료 후 늦게 도착한 콜백 무시
            if (_points.Count > 0 && _points[_points.Count - 1].Index >= p.Point.Index) return;
            _points.Add(p.Point);
            progressBar.Value = Math.Min(progressBar.Maximum, p.Done);
            lblRemaining.Text = Num.FormatDuration(p.Remaining);
            _currentKv = p.Point.SetKv;
            UpdateHvLabel(p.Point.SetKv);
            RedrawPlot();
        }

        private void uiTimer_Tick(object? sender, EventArgs e)
        {
            lblElapsed.Text = Num.FormatDuration(_scanClock.Elapsed);
        }

        private void btnStop_Click(object? sender, EventArgs e)
        {
            if (_cts != null && !_cts.IsCancellationRequested)
            {
                _cts.Cancel();
                Log("정지 요청...");
            }
        }

        private void LogMonitorCheck(List<MeasurementPoint> pts)
        {
            var withMon = pts.Where(p => Num.IsFinite(p.MonitorKv)).ToList();
            if (withMon.Count < 3) return;
            if (Num.LinearFit(withMon.Select(p => p.SetKv).ToList(), withMon.Select(p => p.MonitorKv).ToList(), out var slope, out var icpt, out var r2))
            {
                Log(string.Format(Num.Inv, "  CH2 모니터 확인: 모니터 = {0:0.0000} × 설정 {1:+0.0;-0.0} V (R² = {2:0.000000})", slope, icpt * 1000, r2));
                if (Math.Abs(slope - 1) > 0.05)
                    Log("  주의: 모니터 전압과 설정 전압이 5% 이상 다릅니다. 장비 설정의 '증폭기 이득'과 '모니터 비율'을 확인하세요.");
            }
        }

        // =====================================================================
        //  피팅 / 전기-광학 계수
        // =====================================================================
        private void RunFit(bool logResult)
        {
            var an = ReadAnalysis();
            if (_points.Count == 0)
            {
                _lastFit = null;
                ClearFitDisplay();
                UpdateEo();
                RedrawPlot();
                return;
            }
            var fit = an.Fit(_points);
            _lastFit = fit.Success ? fit : null;
            ShowFit(fit, _points.Count(an.IsUsed), _points.Count);
            UpdateEo();
            RedrawPlot();
            UpdateUiState();
            if (!logResult) return;
            if (fit.Success)
            {
                Log(string.Format(Num.Inv,
                    "피팅: w = Vπ = {0:0.00000} ± {1:0.00000} kV | y0 = {2:0.00000} ± {3:0.00000} V | xc = {4:0.00000} ± {5:0.00000} kV | A = {6:0.00000} ± {7:0.00000} V | Red.χ² = {8:0.000E+0} | Adj.R² = {9:0.00000}",
                    fit.W, fit.WErr, fit.Y0, fit.Y0Err, fit.Xc, fit.XcErr, fit.A, fit.AErr, fit.ReducedChiSq, fit.AdjRSquare));
                foreach (var w in fit.Warnings) Log("  주의: " + w);
                if (_lastEo != null && _lastEo.Valid)
                    Log(string.Format(Num.Inv, "전기-광학 계수: r = {0:0.0000} ± {1:0.0000} pm/V", _lastEo.RPmPerV, _lastEo.RErrPmPerV));
            }
            else Log("피팅 실패: " + fit.Message);
        }

        private void ClearFitDisplay()
        {
            foreach (var l in new[] { lblY0, lblY0Err, lblXc, lblXcErr, lblW, lblWErr, lblA, lblAErr, lblChi, lblR2, lblN })
                l.Text = "-";
            lblVpi.Text = "Vπ = — kV";
            lblFitMsg.Text = "y = y0 + A·sin(π(x − xc)/w)\n측정이 모두 끝나면 피팅합니다.";
        }

        private void ShowFit(SineFitResult f, int used, int total)
        {
            if (!f.Success)
            {
                ClearFitDisplay();
                lblFitMsg.Text = "피팅 실패: " + f.Message;
                lblN.Text = $"{used} / {total}";
                return;
            }
            string V(double v, string fmt) => Num.IsFinite(v) ? v.ToString(fmt, Num.Inv) : "-";
            lblY0.Text = V(f.Y0, "0.00000"); lblY0Err.Text = f.Y0Err == 0 ? "고정" : V(f.Y0Err, "0.00000");
            lblXc.Text = V(f.Xc, "0.00000"); lblXcErr.Text = V(f.XcErr, "0.00000");
            lblW.Text = V(f.W, "0.00000"); lblWErr.Text = V(f.WErr, "0.00000");
            lblA.Text = V(f.A, "0.00000"); lblAErr.Text = f.AErr == 0 ? "고정" : V(f.AErr, "0.00000");
            lblChi.Text = V(f.ReducedChiSq, "0.000E+0");
            lblR2.Text = V(f.AdjRSquare, "0.00000");
            lblN.Text = $"{used} / {total}";
            lblVpi.Text = string.Format(Num.Inv, "Vπ = {0:0.0000} ± {1:0.0000} kV", f.W, f.WErr);
            lblFitMsg.Text = f.Warnings.Count == 0
                ? "y = y0 + A·sin(π(x − xc)/w)\n피팅 완료 (A > 0, 0 ≤ xc < 2w 로 표시)"
                : "⚠ " + string.Join("\n⚠ ", f.Warnings);
        }

        private void UpdateEo()
        {
            if (_loading) return;
            lblEoLambda.Text = ((double)nudWavelength.Value).ToString("0.#", Num.Inv);
            var input = ReadEoInput();
            lblEoVpi.Text = _lastFit != null ? string.Format(Num.Inv, "{0:0.0000} ± {1:0.0000}", _lastFit.W, _lastFit.WErr) : "-";
            var o = ElectroOptic.Compute(input);
            _lastEo = o;
            if (o.Valid)
            {
                lblR.Text = string.Format(Num.Inv, "r = {0:0.000} ± {1:0.000} pm/V", o.RPmPerV, o.RErrPmPerV);
                lblR.ForeColor = System.Drawing.Color.FromArgb(30, 60, 180);
            }
            else
            {
                lblR.Text = "r = — pm/V";
                lblR.ForeColor = System.Drawing.Color.Gray;
            }
        }

        private ElectroOptic.Input ReadEoInput() => new ElectroOptic.Input
        {
            WavelengthNm = (double)nudWavelength.Value,
            RefractiveIndex = (double)nudN.Value,
            ElectrodeGapMm = (double)nudD.Value,
            LengthMm = (double)nudL.Value,
            VpiKv = _lastFit?.W ?? double.NaN,
            VpiErrKv = _lastFit?.WErr ?? double.NaN,
        };

        private void nudEo_ValueChanged(object? sender, EventArgs e) => UpdateEo();

        /// <summary>굴절률 칸 옆 [?] : n_e / n_o 선택 도움말. [적용] 을 누르면 현재 파장의 값이 굴절률 칸에 들어갑니다.</summary>
        private void btnNHelp_Click(object? sender, EventArgs e)
        {
            using var dlg = new frmIndexHelp((double)nudWavelength.Value);
            if (dlg.ShowDialog(this) == DialogResult.OK && dlg.SelectedIndex.HasValue)
            {
                SetNud(nudN, dlg.SelectedIndex.Value);
                Log(string.Format(Num.Inv, "굴절률 n = {0:0.0000} ({1}, λ = {2:0.#} nm) 적용", dlg.SelectedIndex.Value, dlg.SelectedName, (double)nudWavelength.Value));
            }
        }

        private void ScanInput_Changed(object? sender, EventArgs e) => UpdatePlan();

        private void FitOption_Changed(object? sender, EventArgs e)
        {
            if (_loading || _state == null) return;
            nudFixY0.Enabled = chkFixY0.Checked;
            nudFixA.Enabled = chkFixA.Checked;
            // 측정이 끝난 데이터가 있으면 바뀐 옵션으로 즉시 다시 피팅 (계산은 수 ms)
            if (!State.IsBusy && _points.Count > 0) RunFit(logResult: false);
            else RedrawPlot();
        }

        private void btnRefit_Click(object? sender, EventArgs e)
        {
            if (_points.Count == 0)
            {
                MessageBox.Show("피팅할 측정 데이터가 없습니다.", "피팅", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            RunFit(logResult: true);
        }

        // =====================================================================
        //  그래프
        // =====================================================================
        private void RedrawPlot()
        {
            var an = ReadAnalysis();
            bool busy = _state?.IsBusy ?? false;
            var plt = formsPlot.Plot;
            plt.Clear();
            plt.XLabel(an.UseMonitorX ? "Voltage, CH2 monitor (kV)" : "Voltage (kV)", 14f);
            plt.YLabel("Optical Voltage (V)", 14f);

            var fwd = _points.Where(p => an.IsUsed(p) && p.Direction == SweepDirection.Forward).ToList();
            var bwd = _points.Where(p => an.IsUsed(p) && p.Direction == SweepDirection.Backward).ToList();
            var exc = _points.Where(p => !an.IsUsed(p) && Num.IsFinite(p.OpticalV)).ToList();
            AddPoints(plt, fwd, an, ColForward, MarkerShape.FilledSquare, 8f, "Forward");
            AddPoints(plt, bwd, an, ColBackward, MarkerShape.FilledCircle, 8f, "Backward");
            AddPoints(plt, exc, an, ColExcluded, MarkerShape.Eks, 10f, "Excluded");

            double fitLo = double.NaN, fitHi = double.NaN;
            bool extend = _state?.Settings.General.ExtendFitCurve ?? true;
            if (!busy && FitCurve.Build(_points, an, _lastFit, extend, FitCurve.DefaultPoints, out var cxs, out var cys))
            {
                // CSV 의 'Fit curve' 열과 같은 데이터
                fitLo = cxs[0];
                fitHi = cxs[cxs.Length - 1];
                var line = plt.Add.ScatterLine(cxs, cys, ColFit);
                line.LineWidth = 1.5f;
                line.LegendText = "Sine fit";
                plt.Title(string.Format(Num.Inv, "w = {0:0.0000} ± {1:0.0000} kV", _lastFit!.W, _lastFit.WErr), 13f);
            }
            if (busy && Num.IsFinite(_currentKv))
                plt.Add.VerticalLine(_currentKv, 1f, ColCursor, LinePattern.Dashed);

            plt.Axes.AutoScale();
            if (busy && _lastScan != null)
            {
                double lo = Math.Min(_lastScan.StartKv, _lastScan.EndKv), hi = Math.Max(_lastScan.StartKv, _lastScan.EndKv);
                double pad = Math.Max(0.05 * (hi - lo), 0.01);
                plt.Axes.SetLimitsX(lo - pad, hi + pad);
            }
            else if (Num.IsFinite(fitLo) && fitHi > fitLo)
            {
                plt.Axes.SetLimitsX(fitLo, fitHi);
            }
            if (_points.Count > 0) plt.ShowLegend(Alignment.UpperRight);
            else plt.HideLegend();
            formsPlot.Refresh();
        }

        private static void AddPoints(Plot plt, List<MeasurementPoint> pts, AnalysisSettings an, SPColor color, MarkerShape shape, float size, string legend)
        {
            if (pts.Count == 0) return;
            var xs = pts.Select(p => p.X(an.UseMonitorX)).ToArray();
            var ys = pts.Select(p => p.OpticalV).ToArray();
            var sc = plt.Add.ScatterPoints(xs, ys, color);
            sc.MarkerShape = shape;
            sc.MarkerSize = size;
            sc.LegendText = legend;
        }

        // =====================================================================
        //  저장 / 결과 기록
        // =====================================================================
        private void SaveMeasurement(string path)
        {
            if (_lastResult == null || _lastScan == null) throw new InvalidOperationException("저장할 측정 데이터가 없습니다.");
            var info = _lastInfo ?? ReadInfo();
            var an = ReadAnalysis();
            var eoIn = ReadEoInput();
            CsvExport.WriteMeasurement(path, info, _lastScan, State.Settings.Hardware, _lastResult, an, _lastFit, eoIn, _lastEo,
                State.Settings.General.ExtendFitCurve, FitCurve.DefaultPoints);
            if (State.Settings.General.SavePng)
            {
                try { formsPlot.Plot.SavePng(Path.ChangeExtension(path, ".png"), 1000, 650); }
                catch (Exception ex) { Log("그래프 PNG 저장 실패: " + ex.Message); }
            }
            _lastSavedPath = path;
            lblSaved.Text = "저장됨: " + Path.GetFileName(path);
            Log("저장: " + path);
        }

        private void btnSaveCsv_Click(object? sender, EventArgs e)
        {
            if (_lastResult == null || _lastResult.Points.Count == 0)
            {
                MessageBox.Show("저장할 측정 데이터가 없습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var gs = State.Settings.General;
            using var dlg = new SaveFileDialog
            {
                Filter = "CSV 파일 (*.csv)|*.csv",
                FileName = CsvExport.DefaultFileName(_lastInfo ?? ReadInfo(), _lastResult.StartedAt),
                InitialDirectory = Directory.Exists(gs.DataFolder) ? gs.DataFolder : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            };
            if (dlg.ShowDialog(this) != DialogResult.OK) return;
            try { SaveMeasurement(dlg.FileName); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "저장 실패", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnAddHistory_Click(object? sender, EventArgs e)
        {
            if (_lastFit == null)
            {
                MessageBox.Show("피팅 결과가 없습니다.", "결과표", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            AddHistory("수동 추가");
        }

        private void AddHistory(string note)
        {
            if (_lastFit == null || _state == null) return;
            var info = _lastInfo ?? ReadInfo();
            var rec = ResultRecord.Create(info, _lastFit, ReadEoInput(), _lastEo, _lastResult?.Elapsed ?? TimeSpan.Zero, note);
            rec.FilePath = _lastSavedPath ?? "";
            State.AddRecord(rec);
            Log($"결과표에 추가: {rec.Label}, w = {rec.W:0.00000} kV");
        }
    }
}
