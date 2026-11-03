namespace LnEoAnalyzer.Views
{
    partial class ucSettings
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            flowGroups = new FlowLayoutPanel();
            grpAmp = new GroupBox();
            tlpAmp = new TableLayoutPanel();
            lblGain = new Label();
            nudGain = new NumericUpDown();
            lblMaxKv = new Label();
            nudMaxKv = new NumericUpDown();
            lblAmpNote = new Label();
            grpScope = new GroupBox();
            tlpScope = new TableLayoutPanel();
            lblCh1Range = new Label();
            cboCh1Range = new ComboBox();
            chkUseMonitor = new CheckBox();
            lblCh2Range = new Label();
            cboCh2Range = new ComboBox();
            lblMonRatio = new Label();
            nudMonRatio = new NumericUpDown();
            lblSampleRate = new Label();
            cboSampleRate = new ComboBox();
            lblScopeNote = new Label();
            grpTiming = new GroupBox();
            tlpTiming = new TableLayoutPanel();
            lblJump = new Label();
            nudJumpSettle = new NumericUpDown();
            lblOverheadName = new Label();
            lblOverhead = new Label();
            lblTimingNote = new Label();
            grpData = new GroupBox();
            tlpData = new TableLayoutPanel();
            lblFolder = new Label();
            txtFolder = new TextBox();
            btnBrowse = new Button();
            chkAutoSave = new CheckBox();
            chkSavePng = new CheckBox();
            chkAutoHistory = new CheckBox();
            chkExtendFit = new CheckBox();
            grpDemo = new GroupBox();
            tlpDemo = new TableLayoutPanel();
            lblPreset = new Label();
            cboPreset = new ComboBox();
            btnPreset = new Button();
            lblDemoVpi = new Label();
            nudDemoVpi = new NumericUpDown();
            lblDemoY0 = new Label();
            nudDemoY0 = new NumericUpDown();
            lblDemoA = new Label();
            nudDemoA = new NumericUpDown();
            lblDemoXc = new Label();
            nudDemoXc = new NumericUpDown();
            lblDemoNoise = new Label();
            nudDemoNoise = new NumericUpDown();
            lblDemoTau = new Label();
            nudDemoTau = new NumericUpDown();
            lblDemoNote = new Label();
            panelButtons = new Panel();
            btnApply = new FontAwesome.Sharp.IconButton();
            btnDefaults = new FontAwesome.Sharp.IconButton();
            lblApplied = new Label();
            flowGroups.SuspendLayout();
            grpAmp.SuspendLayout();
            tlpAmp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudGain).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMaxKv).BeginInit();
            grpScope.SuspendLayout();
            tlpScope.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudMonRatio).BeginInit();
            grpTiming.SuspendLayout();
            tlpTiming.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudJumpSettle).BeginInit();
            grpData.SuspendLayout();
            tlpData.SuspendLayout();
            grpDemo.SuspendLayout();
            tlpDemo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudDemoVpi).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDemoY0).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDemoA).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDemoXc).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDemoNoise).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDemoTau).BeginInit();
            panelButtons.SuspendLayout();
            SuspendLayout();
            // 
            // flowGroups
            // 
            flowGroups.Controls.Add(grpAmp);
            flowGroups.Controls.Add(grpScope);
            flowGroups.Controls.Add(grpTiming);
            flowGroups.Controls.Add(grpData);
            flowGroups.Controls.Add(grpDemo);
            flowGroups.AutoScroll = true;
            flowGroups.Dock = DockStyle.Fill;
            flowGroups.Name = "flowGroups";
            flowGroups.Padding = new Padding(6);
            // 
            // grpAmp
            // 
            grpAmp.Controls.Add(tlpAmp);
            grpAmp.Font = new Font("Malgun Gothic", 9.5F, FontStyle.Bold);
            grpAmp.Margin = new Padding(8);
            grpAmp.Name = "grpAmp";
            grpAmp.Padding = new Padding(8, 6, 8, 8);
            grpAmp.Size = new Size(400, 170);
            grpAmp.Text = "고전압 증폭기 (Matsusada AMJ-1B20)";
            // 
            // tlpAmp
            // 
            tlpAmp.Controls.Add(lblGain, 0, 0);
            tlpAmp.Controls.Add(nudGain, 1, 0);
            tlpAmp.Controls.Add(lblMaxKv, 0, 1);
            tlpAmp.Controls.Add(nudMaxKv, 1, 1);
            tlpAmp.Controls.Add(lblAmpNote, 0, 2);
            tlpAmp.ColumnCount = 2;
            tlpAmp.Dock = DockStyle.Fill;
            tlpAmp.Name = "tlpAmp";
            tlpAmp.RowCount = 3;
            tlpAmp.SetColumnSpan(lblAmpNote, 2);
            tlpAmp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tlpAmp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tlpAmp.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpAmp.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpAmp.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            // 
            // lblGain
            // 
            lblGain.Dock = DockStyle.Fill;
            lblGain.Name = "lblGain";
            lblGain.Text = "전압 이득 (V/V)";
            lblGain.TextAlign = ContentAlignment.MiddleLeft;
            lblGain.AutoSize = false;
            lblGain.Font = new Font("Malgun Gothic", 9F);
            // 
            // nudGain
            // 
            nudGain.DecimalPlaces = 1;
            nudGain.Dock = DockStyle.Fill;
            nudGain.Increment = new decimal(new int[] { 1, 0, 0, 0 });
            nudGain.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudGain.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudGain.Name = "nudGain";
            nudGain.TextAlign = HorizontalAlignment.Right;
            nudGain.Value = new decimal(new int[] { 100, 0, 0, 0 });
            nudGain.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblMaxKv
            // 
            lblMaxKv.Dock = DockStyle.Fill;
            lblMaxKv.Name = "lblMaxKv";
            lblMaxKv.Text = "출력 제한 (kV)";
            lblMaxKv.TextAlign = ContentAlignment.MiddleLeft;
            lblMaxKv.AutoSize = false;
            lblMaxKv.Font = new Font("Malgun Gothic", 9F);
            // 
            // nudMaxKv
            // 
            nudMaxKv.DecimalPlaces = 3;
            nudMaxKv.Dock = DockStyle.Fill;
            nudMaxKv.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            nudMaxKv.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            nudMaxKv.Minimum = new decimal(new int[] { 1, 0, 0, 196608 });
            nudMaxKv.Name = "nudMaxKv";
            nudMaxKv.TextAlign = HorizontalAlignment.Right;
            nudMaxKv.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudMaxKv.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblAmpNote
            // 
            lblAmpNote.Dock = DockStyle.Fill;
            lblAmpNote.Name = "lblAmpNote";
            lblAmpNote.Text = "AMJ-1B20 정격 ±1 kV 를 넘는 값은 입력할 수 없습니다.\r\n출력 전압 = 신호발생기 DC 오프셋 × 이득\r\n(수동 측정: 오프셋 ±10 V → ±1 kV, 이득 100)";
            lblAmpNote.TextAlign = ContentAlignment.TopLeft;
            lblAmpNote.AutoSize = false;
            lblAmpNote.ForeColor = Color.DimGray;
            lblAmpNote.Font = new Font("Malgun Gothic", 8.5F);
            // 
            // grpScope
            // 
            grpScope.Controls.Add(tlpScope);
            grpScope.Font = new Font("Malgun Gothic", 9.5F, FontStyle.Bold);
            grpScope.Margin = new Padding(8);
            grpScope.Name = "grpScope";
            grpScope.Padding = new Padding(8, 6, 8, 8);
            grpScope.Size = new Size(400, 250);
            grpScope.Text = "오실로스코프 (TiePie Handyscope HS5)";
            // 
            // tlpScope
            // 
            tlpScope.Controls.Add(lblCh1Range, 0, 0);
            tlpScope.Controls.Add(cboCh1Range, 1, 0);
            tlpScope.Controls.Add(chkUseMonitor, 0, 1);
            tlpScope.Controls.Add(lblCh2Range, 0, 2);
            tlpScope.Controls.Add(cboCh2Range, 1, 2);
            tlpScope.Controls.Add(lblMonRatio, 0, 3);
            tlpScope.Controls.Add(nudMonRatio, 1, 3);
            tlpScope.Controls.Add(lblSampleRate, 0, 4);
            tlpScope.Controls.Add(cboSampleRate, 1, 4);
            tlpScope.Controls.Add(lblScopeNote, 0, 5);
            tlpScope.ColumnCount = 2;
            tlpScope.Dock = DockStyle.Fill;
            tlpScope.Name = "tlpScope";
            tlpScope.RowCount = 6;
            tlpScope.SetColumnSpan(chkUseMonitor, 2);
            tlpScope.SetColumnSpan(lblScopeNote, 2);
            tlpScope.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tlpScope.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tlpScope.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpScope.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpScope.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpScope.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpScope.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpScope.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            // 
            // lblCh1Range
            // 
            lblCh1Range.Dock = DockStyle.Fill;
            lblCh1Range.Name = "lblCh1Range";
            lblCh1Range.Text = "CH1 레인지 · 광검출기 (±V)";
            lblCh1Range.TextAlign = ContentAlignment.MiddleLeft;
            lblCh1Range.AutoSize = false;
            lblCh1Range.Font = new Font("Malgun Gothic", 9F);
            // 
            // cboCh1Range
            // 
            cboCh1Range.Dock = DockStyle.Fill;
            cboCh1Range.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCh1Range.Name = "cboCh1Range";
            cboCh1Range.Font = new Font("Malgun Gothic", 9F);
            cboCh1Range.Items.AddRange(new object[] { "0.2", "0.4", "0.8", "2", "4", "8", "20", "40", "80" });
            // 
            // chkUseMonitor
            // 
            chkUseMonitor.Dock = DockStyle.Fill;
            chkUseMonitor.Name = "chkUseMonitor";
            chkUseMonitor.Text = "CH2 전압 모니터 사용";
            chkUseMonitor.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblCh2Range
            // 
            lblCh2Range.Dock = DockStyle.Fill;
            lblCh2Range.Name = "lblCh2Range";
            lblCh2Range.Text = "CH2 레인지 · 모니터 (±V)";
            lblCh2Range.TextAlign = ContentAlignment.MiddleLeft;
            lblCh2Range.AutoSize = false;
            lblCh2Range.Font = new Font("Malgun Gothic", 9F);
            // 
            // cboCh2Range
            // 
            cboCh2Range.Dock = DockStyle.Fill;
            cboCh2Range.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCh2Range.Name = "cboCh2Range";
            cboCh2Range.Font = new Font("Malgun Gothic", 9F);
            cboCh2Range.Items.AddRange(new object[] { "0.2", "0.4", "0.8", "2", "4", "8", "20", "40", "80" });
            // 
            // lblMonRatio
            // 
            lblMonRatio.Dock = DockStyle.Fill;
            lblMonRatio.Name = "lblMonRatio";
            lblMonRatio.Text = "모니터 비율 (kV / V)";
            lblMonRatio.TextAlign = ContentAlignment.MiddleLeft;
            lblMonRatio.AutoSize = false;
            lblMonRatio.Font = new Font("Malgun Gothic", 9F);
            // 
            // nudMonRatio
            // 
            nudMonRatio.DecimalPlaces = 4;
            nudMonRatio.Dock = DockStyle.Fill;
            nudMonRatio.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            nudMonRatio.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            nudMonRatio.Minimum = new decimal(new int[] { 1, 0, 0, 262144 });
            nudMonRatio.Name = "nudMonRatio";
            nudMonRatio.TextAlign = HorizontalAlignment.Right;
            nudMonRatio.Value = new decimal(new int[] { 1, 0, 0, 65536 });
            nudMonRatio.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblSampleRate
            // 
            lblSampleRate.Dock = DockStyle.Fill;
            lblSampleRate.Name = "lblSampleRate";
            lblSampleRate.Text = "샘플링 속도";
            lblSampleRate.TextAlign = ContentAlignment.MiddleLeft;
            lblSampleRate.AutoSize = false;
            lblSampleRate.Font = new Font("Malgun Gothic", 9F);
            // 
            // cboSampleRate
            // 
            cboSampleRate.Dock = DockStyle.Fill;
            cboSampleRate.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSampleRate.Name = "cboSampleRate";
            cboSampleRate.Font = new Font("Malgun Gothic", 9F);
            cboSampleRate.Items.AddRange(new object[] { "100 kSa/s", "500 kSa/s", "1 MSa/s", "5 MSa/s" });
            // 
            // lblScopeNote
            // 
            lblScopeNote.Dock = DockStyle.Fill;
            lblScopeNote.Name = "lblScopeNote";
            lblScopeNote.Text = "DET410 출력이 레인지를 넘으면 해당 점이 '레인지 초과'로 표시됩니다.\r\n모니터 비율: 모니터 출력 1 V 가 실제 몇 kV 인지 (1/100 모니터 = 0.1).\r\n측정 후 로그의 'CH2 모니터 확인' 기울기가 1 에 가까운지 확인하세요.";
            lblScopeNote.TextAlign = ContentAlignment.TopLeft;
            lblScopeNote.AutoSize = false;
            lblScopeNote.ForeColor = Color.DimGray;
            lblScopeNote.Font = new Font("Malgun Gothic", 8.5F);
            // 
            // grpTiming
            // 
            grpTiming.Controls.Add(tlpTiming);
            grpTiming.Font = new Font("Malgun Gothic", 9.5F, FontStyle.Bold);
            grpTiming.Margin = new Padding(8);
            grpTiming.Name = "grpTiming";
            grpTiming.Padding = new Padding(8, 6, 8, 8);
            grpTiming.Size = new Size(400, 170);
            grpTiming.Text = "측정 타이밍";
            // 
            // tlpTiming
            // 
            tlpTiming.Controls.Add(lblJump, 0, 0);
            tlpTiming.Controls.Add(nudJumpSettle, 1, 0);
            tlpTiming.Controls.Add(lblOverheadName, 0, 1);
            tlpTiming.Controls.Add(lblOverhead, 1, 1);
            tlpTiming.Controls.Add(lblTimingNote, 0, 2);
            tlpTiming.ColumnCount = 2;
            tlpTiming.Dock = DockStyle.Fill;
            tlpTiming.Name = "tlpTiming";
            tlpTiming.RowCount = 3;
            tlpTiming.SetColumnSpan(lblTimingNote, 2);
            tlpTiming.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tlpTiming.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tlpTiming.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpTiming.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpTiming.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            // 
            // lblJump
            // 
            lblJump.Dock = DockStyle.Fill;
            lblJump.Name = "lblJump";
            lblJump.Text = "큰 전압 변화 후 안정화 (ms)";
            lblJump.TextAlign = ContentAlignment.MiddleLeft;
            lblJump.AutoSize = false;
            lblJump.Font = new Font("Malgun Gothic", 9F);
            // 
            // nudJumpSettle
            // 
            nudJumpSettle.DecimalPlaces = 0;
            nudJumpSettle.Dock = DockStyle.Fill;
            nudJumpSettle.Increment = new decimal(new int[] { 50, 0, 0, 0 });
            nudJumpSettle.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudJumpSettle.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            nudJumpSettle.Name = "nudJumpSettle";
            nudJumpSettle.TextAlign = HorizontalAlignment.Right;
            nudJumpSettle.Value = new decimal(new int[] { 300, 0, 0, 0 });
            nudJumpSettle.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblOverheadName
            // 
            lblOverheadName.Dock = DockStyle.Fill;
            lblOverheadName.Name = "lblOverheadName";
            lblOverheadName.Text = "측정점당 추가 지연 (학습값)";
            lblOverheadName.TextAlign = ContentAlignment.MiddleLeft;
            lblOverheadName.AutoSize = false;
            lblOverheadName.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblOverhead
            // 
            lblOverhead.Dock = DockStyle.Fill;
            lblOverhead.Name = "lblOverhead";
            lblOverhead.Text = "-";
            lblOverhead.TextAlign = ContentAlignment.MiddleRight;
            lblOverhead.AutoSize = false;
            lblOverhead.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblTimingNote
            // 
            lblTimingNote.Dock = DockStyle.Fill;
            lblTimingNote.Name = "lblTimingNote";
            lblTimingNote.Text = "측정 시작 시 0 V → 시작 전압처럼 간격보다 크게 바뀌는 점에 적용합니다.\r\n추가 지연은 측정이 끝날 때마다 자동으로 갱신되어 예상 시간 계산에 쓰입니다.";
            lblTimingNote.TextAlign = ContentAlignment.TopLeft;
            lblTimingNote.AutoSize = false;
            lblTimingNote.ForeColor = Color.DimGray;
            lblTimingNote.Font = new Font("Malgun Gothic", 8.5F);
            // 
            // grpData
            // 
            grpData.Controls.Add(tlpData);
            grpData.Font = new Font("Malgun Gothic", 9.5F, FontStyle.Bold);
            grpData.Margin = new Padding(8);
            grpData.Name = "grpData";
            grpData.Padding = new Padding(8, 6, 8, 8);
            grpData.Size = new Size(400, 240);
            grpData.Text = "데이터 저장 · 표시";
            // 
            // tlpData
            // 
            tlpData.Controls.Add(lblFolder, 0, 0);
            tlpData.Controls.Add(txtFolder, 0, 1);
            tlpData.Controls.Add(btnBrowse, 1, 1);
            tlpData.Controls.Add(chkAutoSave, 0, 2);
            tlpData.Controls.Add(chkSavePng, 0, 3);
            tlpData.Controls.Add(chkAutoHistory, 0, 4);
            tlpData.Controls.Add(chkExtendFit, 0, 5);
            tlpData.ColumnCount = 2;
            tlpData.Dock = DockStyle.Fill;
            tlpData.Name = "tlpData";
            tlpData.RowCount = 7;
            tlpData.SetColumnSpan(lblFolder, 2);
            tlpData.SetColumnSpan(chkAutoSave, 2);
            tlpData.SetColumnSpan(chkSavePng, 2);
            tlpData.SetColumnSpan(chkAutoHistory, 2);
            tlpData.SetColumnSpan(chkExtendFit, 2);
            tlpData.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 72F));
            tlpData.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
            tlpData.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            tlpData.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpData.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            tlpData.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            tlpData.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            tlpData.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
            tlpData.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            // 
            // lblFolder
            // 
            lblFolder.Dock = DockStyle.Fill;
            lblFolder.Name = "lblFolder";
            lblFolder.Text = "저장 폴더";
            lblFolder.TextAlign = ContentAlignment.MiddleLeft;
            lblFolder.AutoSize = false;
            lblFolder.Font = new Font("Malgun Gothic", 9F);
            // 
            // txtFolder
            // 
            txtFolder.Dock = DockStyle.Fill;
            txtFolder.Name = "txtFolder";
            txtFolder.Font = new Font("Malgun Gothic", 9F);
            // 
            // btnBrowse
            // 
            btnBrowse.Dock = DockStyle.Fill;
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Text = "찾아보기";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // chkAutoSave
            // 
            chkAutoSave.Dock = DockStyle.Fill;
            chkAutoSave.Name = "chkAutoSave";
            chkAutoSave.Text = "측정 완료 시 CSV 자동 저장";
            chkAutoSave.Font = new Font("Malgun Gothic", 9F);
            // 
            // chkSavePng
            // 
            chkSavePng.Dock = DockStyle.Fill;
            chkSavePng.Name = "chkSavePng";
            chkSavePng.Text = "CSV 저장 시 그래프 PNG 함께 저장";
            chkSavePng.Font = new Font("Malgun Gothic", 9F);
            // 
            // chkAutoHistory
            // 
            chkAutoHistory.Dock = DockStyle.Fill;
            chkAutoHistory.Name = "chkAutoHistory";
            chkAutoHistory.Text = "측정 완료 시 결과표에 자동 추가";
            chkAutoHistory.Font = new Font("Malgun Gothic", 9F);
            // 
            // chkExtendFit
            // 
            chkExtendFit.Dock = DockStyle.Fill;
            chkExtendFit.Name = "chkExtendFit";
            chkExtendFit.Text = "피팅 곡선을 측정 범위 밖까지 표시";
            chkExtendFit.Font = new Font("Malgun Gothic", 9F);
            // 
            // grpDemo
            // 
            grpDemo.Controls.Add(tlpDemo);
            grpDemo.Font = new Font("Malgun Gothic", 9.5F, FontStyle.Bold);
            grpDemo.Margin = new Padding(8);
            grpDemo.Name = "grpDemo";
            grpDemo.Padding = new Padding(8, 6, 8, 8);
            grpDemo.Size = new Size(400, 360);
            grpDemo.Text = "데모 (가상 장비)";
            // 
            // tlpDemo
            // 
            tlpDemo.Controls.Add(lblPreset, 0, 0);
            tlpDemo.Controls.Add(cboPreset, 1, 0);
            tlpDemo.Controls.Add(btnPreset, 1, 1);
            tlpDemo.Controls.Add(lblDemoVpi, 0, 2);
            tlpDemo.Controls.Add(nudDemoVpi, 1, 2);
            tlpDemo.Controls.Add(lblDemoY0, 0, 3);
            tlpDemo.Controls.Add(nudDemoY0, 1, 3);
            tlpDemo.Controls.Add(lblDemoA, 0, 4);
            tlpDemo.Controls.Add(nudDemoA, 1, 4);
            tlpDemo.Controls.Add(lblDemoXc, 0, 5);
            tlpDemo.Controls.Add(nudDemoXc, 1, 5);
            tlpDemo.Controls.Add(lblDemoNoise, 0, 6);
            tlpDemo.Controls.Add(nudDemoNoise, 1, 6);
            tlpDemo.Controls.Add(lblDemoTau, 0, 7);
            tlpDemo.Controls.Add(nudDemoTau, 1, 7);
            tlpDemo.Controls.Add(lblDemoNote, 0, 8);
            tlpDemo.ColumnCount = 2;
            tlpDemo.Dock = DockStyle.Fill;
            tlpDemo.Name = "tlpDemo";
            tlpDemo.RowCount = 9;
            tlpDemo.SetColumnSpan(lblDemoNote, 2);
            tlpDemo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tlpDemo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tlpDemo.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpDemo.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpDemo.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpDemo.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpDemo.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpDemo.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpDemo.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpDemo.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tlpDemo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            // 
            // lblPreset
            // 
            lblPreset.Dock = DockStyle.Fill;
            lblPreset.Name = "lblPreset";
            lblPreset.Text = "예시 값";
            lblPreset.TextAlign = ContentAlignment.MiddleLeft;
            lblPreset.AutoSize = false;
            lblPreset.Font = new Font("Malgun Gothic", 9F);
            // 
            // cboPreset
            // 
            cboPreset.Dock = DockStyle.Fill;
            cboPreset.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPreset.Name = "cboPreset";
            cboPreset.Font = new Font("Malgun Gothic", 9F);
            cboPreset.Items.AddRange(new object[] { "1310 nm @25℃", "1550 nm @25℃" });
            // 
            // btnPreset
            // 
            btnPreset.Dock = DockStyle.Fill;
            btnPreset.Name = "btnPreset";
            btnPreset.Text = "불러오기";
            btnPreset.UseVisualStyleBackColor = true;
            btnPreset.Click += btnPreset_Click;
            // 
            // lblDemoVpi
            // 
            lblDemoVpi.Dock = DockStyle.Fill;
            lblDemoVpi.Name = "lblDemoVpi";
            lblDemoVpi.Text = "반파장 전압 w = Vπ (kV)";
            lblDemoVpi.TextAlign = ContentAlignment.MiddleLeft;
            lblDemoVpi.AutoSize = false;
            lblDemoVpi.Font = new Font("Malgun Gothic", 9F);
            // 
            // nudDemoVpi
            // 
            nudDemoVpi.DecimalPlaces = 5;
            nudDemoVpi.Dock = DockStyle.Fill;
            nudDemoVpi.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            nudDemoVpi.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            nudDemoVpi.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            nudDemoVpi.Name = "nudDemoVpi";
            nudDemoVpi.TextAlign = HorizontalAlignment.Right;
            nudDemoVpi.Value = new decimal(new int[] { 208675, 0, 0, 327680 });
            nudDemoVpi.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblDemoY0
            // 
            lblDemoY0.Dock = DockStyle.Fill;
            lblDemoY0.Name = "lblDemoY0";
            lblDemoY0.Text = "y0 (V)";
            lblDemoY0.TextAlign = ContentAlignment.MiddleLeft;
            lblDemoY0.AutoSize = false;
            lblDemoY0.Font = new Font("Malgun Gothic", 9F);
            // 
            // nudDemoY0
            // 
            nudDemoY0.DecimalPlaces = 5;
            nudDemoY0.Dock = DockStyle.Fill;
            nudDemoY0.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            nudDemoY0.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            nudDemoY0.Minimum = new decimal(new int[] { 10, 0, 0, -2147483648 });
            nudDemoY0.Name = "nudDemoY0";
            nudDemoY0.TextAlign = HorizontalAlignment.Right;
            nudDemoY0.Value = new decimal(new int[] { 149137, 0, 0, 327680 });
            nudDemoY0.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblDemoA
            // 
            lblDemoA.Dock = DockStyle.Fill;
            lblDemoA.Name = "lblDemoA";
            lblDemoA.Text = "A (V)";
            lblDemoA.TextAlign = ContentAlignment.MiddleLeft;
            lblDemoA.AutoSize = false;
            lblDemoA.Font = new Font("Malgun Gothic", 9F);
            // 
            // nudDemoA
            // 
            nudDemoA.DecimalPlaces = 5;
            nudDemoA.Dock = DockStyle.Fill;
            nudDemoA.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            nudDemoA.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            nudDemoA.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            nudDemoA.Name = "nudDemoA";
            nudDemoA.TextAlign = HorizontalAlignment.Right;
            nudDemoA.Value = new decimal(new int[] { 86449, 0, 0, 327680 });
            nudDemoA.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblDemoXc
            // 
            lblDemoXc.Dock = DockStyle.Fill;
            lblDemoXc.Name = "lblDemoXc";
            lblDemoXc.Text = "xc (kV)";
            lblDemoXc.TextAlign = ContentAlignment.MiddleLeft;
            lblDemoXc.AutoSize = false;
            lblDemoXc.Font = new Font("Malgun Gothic", 9F);
            // 
            // nudDemoXc
            // 
            nudDemoXc.DecimalPlaces = 5;
            nudDemoXc.Dock = DockStyle.Fill;
            nudDemoXc.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            nudDemoXc.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            nudDemoXc.Minimum = new decimal(new int[] { 100, 0, 0, -2147483648 });
            nudDemoXc.Name = "nudDemoXc";
            nudDemoXc.TextAlign = HorizontalAlignment.Right;
            nudDemoXc.Value = new decimal(new int[] { 266175, 0, 0, 327680 });
            nudDemoXc.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblDemoNoise
            // 
            lblDemoNoise.Dock = DockStyle.Fill;
            lblDemoNoise.Name = "lblDemoNoise";
            lblDemoNoise.Text = "잡음 (V rms)";
            lblDemoNoise.TextAlign = ContentAlignment.MiddleLeft;
            lblDemoNoise.AutoSize = false;
            lblDemoNoise.Font = new Font("Malgun Gothic", 9F);
            // 
            // nudDemoNoise
            // 
            nudDemoNoise.DecimalPlaces = 4;
            nudDemoNoise.Dock = DockStyle.Fill;
            nudDemoNoise.Increment = new decimal(new int[] { 1, 0, 0, 196608 });
            nudDemoNoise.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            nudDemoNoise.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            nudDemoNoise.Name = "nudDemoNoise";
            nudDemoNoise.TextAlign = HorizontalAlignment.Right;
            nudDemoNoise.Value = new decimal(new int[] { 4, 0, 0, 196608 });
            nudDemoNoise.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblDemoTau
            // 
            lblDemoTau.Dock = DockStyle.Fill;
            lblDemoTau.Name = "lblDemoTau";
            lblDemoTau.Text = "증폭기 응답 시정수 (ms)";
            lblDemoTau.TextAlign = ContentAlignment.MiddleLeft;
            lblDemoTau.AutoSize = false;
            lblDemoTau.Font = new Font("Malgun Gothic", 9F);
            // 
            // nudDemoTau
            // 
            nudDemoTau.DecimalPlaces = 1;
            nudDemoTau.Dock = DockStyle.Fill;
            nudDemoTau.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            nudDemoTau.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudDemoTau.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            nudDemoTau.Name = "nudDemoTau";
            nudDemoTau.TextAlign = HorizontalAlignment.Right;
            nudDemoTau.Value = new decimal(new int[] { 3, 0, 0, 0 });
            nudDemoTau.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblDemoNote
            // 
            lblDemoNote.Dock = DockStyle.Fill;
            lblDemoNote.Name = "lblDemoNote";
            lblDemoNote.Text = "가상 장비 응답: y = y0 + A·sin(π(V − xc)/w) + 잡음. 다음 [연결] 때 적용됩니다.";
            lblDemoNote.TextAlign = ContentAlignment.TopLeft;
            lblDemoNote.AutoSize = false;
            lblDemoNote.ForeColor = Color.DimGray;
            lblDemoNote.Font = new Font("Malgun Gothic", 8.5F);
            // 
            // panelButtons
            // 
            panelButtons.Controls.Add(btnApply);
            panelButtons.Controls.Add(btnDefaults);
            panelButtons.Controls.Add(lblApplied);
            panelButtons.BackColor = Color.White;
            panelButtons.Dock = DockStyle.Bottom;
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(1400, 56);
            // 
            // btnApply
            // 
            btnApply.BackColor = Color.FromArgb(46, 160, 67);
            btnApply.Cursor = Cursors.Hand;
            btnApply.FlatStyle = FlatStyle.Flat;
            btnApply.Font = new Font("Malgun Gothic", 9.5F, FontStyle.Bold);
            btnApply.ForeColor = Color.White;
            btnApply.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            btnApply.IconColor = Color.White;
            btnApply.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnApply.IconSize = 18;
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(150, 40);
            btnApply.Text = "적용 및 저장";
            btnApply.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnApply.UseVisualStyleBackColor = false;
            btnApply.Location = new Point(14, 8);
            btnApply.FlatAppearance.BorderSize = 0;
            btnApply.Click += btnApply_Click;
            // 
            // btnDefaults
            // 
            btnDefaults.BackColor = Color.FromArgb(108, 117, 125);
            btnDefaults.Cursor = Cursors.Hand;
            btnDefaults.FlatStyle = FlatStyle.Flat;
            btnDefaults.Font = new Font("Malgun Gothic", 9.5F, FontStyle.Bold);
            btnDefaults.ForeColor = Color.White;
            btnDefaults.IconChar = FontAwesome.Sharp.IconChar.RotateLeft;
            btnDefaults.IconColor = Color.White;
            btnDefaults.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnDefaults.IconSize = 18;
            btnDefaults.Name = "btnDefaults";
            btnDefaults.Size = new Size(150, 40);
            btnDefaults.Text = "기본값 불러오기";
            btnDefaults.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDefaults.UseVisualStyleBackColor = false;
            btnDefaults.Location = new Point(172, 8);
            btnDefaults.FlatAppearance.BorderSize = 0;
            btnDefaults.Click += btnDefaults_Click;
            // 
            // lblApplied
            // 
            lblApplied.AutoSize = true;
            lblApplied.ForeColor = Color.DimGray;
            lblApplied.Location = new Point(336, 19);
            lblApplied.Name = "lblApplied";
            lblApplied.Text = "설정 파일: %LocalAppData%\\LnEoAnalyzer\\settings.ini";
            // 
            // ucSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 255);
            Font = new Font("Malgun Gothic", 9F);
            Name = "ucSettings";
            Size = new Size(1400, 800);
            Controls.Add(flowGroups);
            Controls.Add(panelButtons);
            ((System.ComponentModel.ISupportInitialize)nudGain).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMaxKv).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMonRatio).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudJumpSettle).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDemoVpi).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDemoY0).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDemoA).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDemoXc).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDemoNoise).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDemoTau).EndInit();
            flowGroups.ResumeLayout(false);
            flowGroups.PerformLayout();
            grpAmp.ResumeLayout(false);
            grpAmp.PerformLayout();
            tlpAmp.ResumeLayout(false);
            tlpAmp.PerformLayout();
            grpScope.ResumeLayout(false);
            grpScope.PerformLayout();
            tlpScope.ResumeLayout(false);
            tlpScope.PerformLayout();
            grpTiming.ResumeLayout(false);
            grpTiming.PerformLayout();
            tlpTiming.ResumeLayout(false);
            tlpTiming.PerformLayout();
            grpData.ResumeLayout(false);
            grpData.PerformLayout();
            tlpData.ResumeLayout(false);
            tlpData.PerformLayout();
            grpDemo.ResumeLayout(false);
            grpDemo.PerformLayout();
            tlpDemo.ResumeLayout(false);
            tlpDemo.PerformLayout();
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowGroups;
        private GroupBox grpAmp;
        private TableLayoutPanel tlpAmp;
        private Label lblGain;
        private NumericUpDown nudGain;
        private Label lblMaxKv;
        private NumericUpDown nudMaxKv;
        private Label lblAmpNote;
        private GroupBox grpScope;
        private TableLayoutPanel tlpScope;
        private Label lblCh1Range;
        private ComboBox cboCh1Range;
        private CheckBox chkUseMonitor;
        private Label lblCh2Range;
        private ComboBox cboCh2Range;
        private Label lblMonRatio;
        private NumericUpDown nudMonRatio;
        private Label lblSampleRate;
        private ComboBox cboSampleRate;
        private Label lblScopeNote;
        private GroupBox grpTiming;
        private TableLayoutPanel tlpTiming;
        private Label lblJump;
        private NumericUpDown nudJumpSettle;
        private Label lblOverheadName;
        private Label lblOverhead;
        private Label lblTimingNote;
        private GroupBox grpData;
        private TableLayoutPanel tlpData;
        private Label lblFolder;
        private TextBox txtFolder;
        private Button btnBrowse;
        private CheckBox chkAutoSave;
        private CheckBox chkSavePng;
        private CheckBox chkAutoHistory;
        private CheckBox chkExtendFit;
        private GroupBox grpDemo;
        private TableLayoutPanel tlpDemo;
        private Label lblPreset;
        private ComboBox cboPreset;
        private Button btnPreset;
        private Label lblDemoVpi;
        private NumericUpDown nudDemoVpi;
        private Label lblDemoY0;
        private NumericUpDown nudDemoY0;
        private Label lblDemoA;
        private NumericUpDown nudDemoA;
        private Label lblDemoXc;
        private NumericUpDown nudDemoXc;
        private Label lblDemoNoise;
        private NumericUpDown nudDemoNoise;
        private Label lblDemoTau;
        private NumericUpDown nudDemoTau;
        private Label lblDemoNote;
        private Panel panelButtons;
        private FontAwesome.Sharp.IconButton btnApply;
        private FontAwesome.Sharp.IconButton btnDefaults;
        private Label lblApplied;
    }
}
