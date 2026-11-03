namespace LnEoAnalyzer.Views
{
    partial class ucMeasure
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
            components = new System.ComponentModel.Container();
            panelCenter = new Panel();
            formsPlot = new ScottPlot.WinForms.FormsPlot();
            panelStatus = new Panel();
            txtLog = new TextBox();
            lblGap = new Label();
            progressBar = new ProgressBar();
            tlpTime = new TableLayoutPanel();
            lblCapEstimate = new Label();
            lblEstimate = new Label();
            lblCapElapsed = new Label();
            lblElapsed = new Label();
            lblCapRemaining = new Label();
            lblRemaining = new Label();
            lblCapTotal = new Label();
            lblTotal = new Label();
            panelRight = new Panel();
            panelSave = new Panel();
            lblSaved = new Label();
            btnSaveCsv = new FontAwesome.Sharp.IconButton();
            btnAddHistory = new FontAwesome.Sharp.IconButton();
            grpEo = new GroupBox();
            lblR = new Label();
            tlpEo = new TableLayoutPanel();
            lblEoLambdaName = new Label();
            lblEoLambda = new Label();
            lblEoN = new Label();
            nudN = new NumericUpDown();
            lblEoD = new Label();
            nudD = new NumericUpDown();
            lblEoL = new Label();
            nudL = new NumericUpDown();
            lblEoVpiName = new Label();
            lblEoVpi = new Label();
            btnNHelp = new FontAwesome.Sharp.IconButton();
            lblEoFormula = new Label();
            grpResult = new GroupBox();
            lblFitMsg = new Label();
            tlpResult = new TableLayoutPanel();
            lblHdrName = new Label();
            lblHdrValue = new Label();
            lblHdrErr = new Label();
            lblNameY0 = new Label();
            lblY0 = new Label();
            lblY0Err = new Label();
            lblNameXc = new Label();
            lblXc = new Label();
            lblXcErr = new Label();
            lblNameW = new Label();
            lblW = new Label();
            lblWErr = new Label();
            lblNameA = new Label();
            lblA = new Label();
            lblAErr = new Label();
            lblNameChi = new Label();
            lblChi = new Label();
            lblNameR2 = new Label();
            lblR2 = new Label();
            lblNameN = new Label();
            lblN = new Label();
            lblVpi = new Label();
            panelLeft = new Panel();
            grpFit = new GroupBox();
            tlpFit = new TableLayoutPanel();
            lblXSource = new Label();
            cboXSource = new ComboBox();
            lblSelection = new Label();
            cboSelection = new ComboBox();
            chkExclude = new CheckBox();
            chkFixY0 = new CheckBox();
            nudFixY0 = new NumericUpDown();
            chkFixA = new CheckBox();
            nudFixA = new NumericUpDown();
            btnRefit = new FontAwesome.Sharp.IconButton();
            grpScan = new GroupBox();
            tlpScan = new TableLayoutPanel();
            lblStart = new Label();
            nudStart = new NumericUpDown();
            lblEnd = new Label();
            nudEnd = new NumericUpDown();
            lblStep = new Label();
            nudStep = new NumericUpDown();
            lblSettle = new Label();
            nudSettle = new NumericUpDown();
            lblAverage = new Label();
            nudAverage = new NumericUpDown();
            lblMode = new Label();
            cboMode = new ComboBox();
            lblRepeats = new Label();
            nudRepeats = new NumericUpDown();
            lblPlan = new Label();
            grpInfo = new GroupBox();
            tlpInfo = new TableLayoutPanel();
            lblSample = new Label();
            txtSample = new TextBox();
            lblWavelength = new Label();
            nudWavelength = new NumericUpDown();
            lblTemperature = new Label();
            nudTemperature = new NumericUpDown();
            lblMemo = new Label();
            txtMemo = new TextBox();
            panelRun = new Panel();
            btnStart = new FontAwesome.Sharp.IconButton();
            btnStop = new FontAwesome.Sharp.IconButton();
            panelTop = new Panel();
            flowTop = new FlowLayoutPanel();
            lblDevKind = new Label();
            cboDevice = new ComboBox();
            btnConnect = new FontAwesome.Sharp.IconButton();
            btnDisconnect = new FontAwesome.Sharp.IconButton();
            lblSep1 = new Label();
            lblManual = new Label();
            nudManualKv = new NumericUpDown();
            btnApplyOutput = new FontAwesome.Sharp.IconButton();
            btnZero = new FontAwesome.Sharp.IconButton();
            lblHv = new Label();
            lblSep2 = new Label();
            chkLive = new CheckBox();
            lblLive = new Label();
            uiTimer = new System.Windows.Forms.Timer(components);
            liveTimer = new System.Windows.Forms.Timer(components);
            toolTip = new ToolTip(components);
            panelCenter.SuspendLayout();
            panelStatus.SuspendLayout();
            tlpTime.SuspendLayout();
            panelRight.SuspendLayout();
            panelSave.SuspendLayout();
            grpEo.SuspendLayout();
            tlpEo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudN).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudL).BeginInit();
            grpResult.SuspendLayout();
            tlpResult.SuspendLayout();
            panelLeft.SuspendLayout();
            grpFit.SuspendLayout();
            tlpFit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudFixY0).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudFixA).BeginInit();
            grpScan.SuspendLayout();
            tlpScan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudStart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudEnd).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStep).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSettle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudAverage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudRepeats).BeginInit();
            grpInfo.SuspendLayout();
            tlpInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudWavelength).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudTemperature).BeginInit();
            panelRun.SuspendLayout();
            panelTop.SuspendLayout();
            flowTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudManualKv).BeginInit();
            SuspendLayout();
            // 
            // panelCenter
            // 
            panelCenter.Controls.Add(formsPlot);
            panelCenter.Controls.Add(panelStatus);
            panelCenter.BackColor = Color.White;
            panelCenter.Dock = DockStyle.Fill;
            panelCenter.Name = "panelCenter";
            panelCenter.Padding = new Padding(6);
            // 
            // formsPlot
            // 
            formsPlot.Dock = DockStyle.Fill;
            formsPlot.Name = "formsPlot";
            // 
            // panelStatus
            // 
            panelStatus.Controls.Add(txtLog);
            panelStatus.Controls.Add(lblGap);
            panelStatus.Controls.Add(progressBar);
            panelStatus.Controls.Add(tlpTime);
            panelStatus.Dock = DockStyle.Bottom;
            panelStatus.Name = "panelStatus";
            panelStatus.Padding = new Padding(0, 4, 0, 0);
            panelStatus.Size = new Size(780, 170);
            // 
            // txtLog
            // 
            txtLog.BackColor = Color.White;
            txtLog.Dock = DockStyle.Fill;
            txtLog.Font = new Font("Consolas", 9F);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            // 
            // lblGap
            // 
            lblGap.Dock = DockStyle.Top;
            lblGap.Name = "lblGap";
            lblGap.Size = new Size(780, 4);
            // 
            // progressBar
            // 
            progressBar.Dock = DockStyle.Top;
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(780, 14);
            // 
            // tlpTime
            // 
            tlpTime.Controls.Add(lblCapEstimate, 0, 0);
            tlpTime.Controls.Add(lblEstimate, 0, 1);
            tlpTime.Controls.Add(lblCapElapsed, 1, 0);
            tlpTime.Controls.Add(lblElapsed, 1, 1);
            tlpTime.Controls.Add(lblCapRemaining, 2, 0);
            tlpTime.Controls.Add(lblRemaining, 2, 1);
            tlpTime.Controls.Add(lblCapTotal, 3, 0);
            tlpTime.Controls.Add(lblTotal, 3, 1);
            tlpTime.ColumnCount = 4;
            tlpTime.Dock = DockStyle.Top;
            tlpTime.Name = "tlpTime";
            tlpTime.RowCount = 2;
            tlpTime.Size = new Size(780, 52);
            tlpTime.BackColor = Color.White;
            tlpTime.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpTime.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpTime.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpTime.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpTime.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlpTime.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            // 
            // lblCapEstimate
            // 
            lblCapEstimate.Dock = DockStyle.Fill;
            lblCapEstimate.Name = "lblCapEstimate";
            lblCapEstimate.Text = "예상 소요 시간";
            lblCapEstimate.TextAlign = ContentAlignment.BottomCenter;
            lblCapEstimate.AutoSize = false;
            lblCapEstimate.Font = new Font("Malgun Gothic", 8.5F);
            lblCapEstimate.ForeColor = Color.DimGray;
            // 
            // lblEstimate
            // 
            lblEstimate.Dock = DockStyle.Fill;
            lblEstimate.Name = "lblEstimate";
            lblEstimate.Text = "--:--.-";
            lblEstimate.TextAlign = ContentAlignment.TopCenter;
            lblEstimate.AutoSize = false;
            lblEstimate.Font = new Font("Malgun Gothic", 13F, FontStyle.Bold);
            // 
            // lblCapElapsed
            // 
            lblCapElapsed.Dock = DockStyle.Fill;
            lblCapElapsed.Name = "lblCapElapsed";
            lblCapElapsed.Text = "경과 시간";
            lblCapElapsed.TextAlign = ContentAlignment.BottomCenter;
            lblCapElapsed.AutoSize = false;
            lblCapElapsed.Font = new Font("Malgun Gothic", 8.5F);
            lblCapElapsed.ForeColor = Color.DimGray;
            // 
            // lblElapsed
            // 
            lblElapsed.Dock = DockStyle.Fill;
            lblElapsed.Name = "lblElapsed";
            lblElapsed.Text = "--:--.-";
            lblElapsed.TextAlign = ContentAlignment.TopCenter;
            lblElapsed.AutoSize = false;
            lblElapsed.Font = new Font("Malgun Gothic", 13F, FontStyle.Bold);
            // 
            // lblCapRemaining
            // 
            lblCapRemaining.Dock = DockStyle.Fill;
            lblCapRemaining.Name = "lblCapRemaining";
            lblCapRemaining.Text = "남은 시간";
            lblCapRemaining.TextAlign = ContentAlignment.BottomCenter;
            lblCapRemaining.AutoSize = false;
            lblCapRemaining.Font = new Font("Malgun Gothic", 8.5F);
            lblCapRemaining.ForeColor = Color.DimGray;
            // 
            // lblRemaining
            // 
            lblRemaining.Dock = DockStyle.Fill;
            lblRemaining.Name = "lblRemaining";
            lblRemaining.Text = "--:--.-";
            lblRemaining.TextAlign = ContentAlignment.TopCenter;
            lblRemaining.AutoSize = false;
            lblRemaining.Font = new Font("Malgun Gothic", 13F, FontStyle.Bold);
            // 
            // lblCapTotal
            // 
            lblCapTotal.Dock = DockStyle.Fill;
            lblCapTotal.Name = "lblCapTotal";
            lblCapTotal.Text = "총 소요 시간 (최근 측정)";
            lblCapTotal.TextAlign = ContentAlignment.BottomCenter;
            lblCapTotal.AutoSize = false;
            lblCapTotal.Font = new Font("Malgun Gothic", 8.5F);
            lblCapTotal.ForeColor = Color.DimGray;
            // 
            // lblTotal
            // 
            lblTotal.Dock = DockStyle.Fill;
            lblTotal.Name = "lblTotal";
            lblTotal.Text = "--:--.-";
            lblTotal.TextAlign = ContentAlignment.TopCenter;
            lblTotal.AutoSize = false;
            lblTotal.Font = new Font("Malgun Gothic", 13F, FontStyle.Bold);
            // 
            // panelRight
            // 
            panelRight.Controls.Add(panelSave);
            panelRight.Controls.Add(grpEo);
            panelRight.Controls.Add(grpResult);
            panelRight.AutoScroll = true;
            panelRight.Dock = DockStyle.Right;
            panelRight.Name = "panelRight";
            panelRight.Padding = new Padding(4, 6, 6, 6);
            panelRight.Size = new Size(300, 760);
            // 
            // panelSave
            // 
            panelSave.Controls.Add(lblSaved);
            panelSave.Controls.Add(btnSaveCsv);
            panelSave.Controls.Add(btnAddHistory);
            panelSave.Dock = DockStyle.Top;
            panelSave.Name = "panelSave";
            panelSave.Size = new Size(288, 100);
            // 
            // lblSaved
            // 
            lblSaved.Font = new Font("Malgun Gothic", 8.5F);
            lblSaved.ForeColor = Color.DimGray;
            lblSaved.Location = new Point(6, 50);
            lblSaved.Name = "lblSaved";
            lblSaved.Size = new Size(276, 44);
            lblSaved.Text = "";
            lblSaved.AutoEllipsis = true;
            // 
            // btnSaveCsv
            // 
            btnSaveCsv.BackColor = Color.FromArgb(98, 102, 242);
            btnSaveCsv.Cursor = Cursors.Hand;
            btnSaveCsv.FlatStyle = FlatStyle.Flat;
            btnSaveCsv.Font = new Font("Malgun Gothic", 9.5F, FontStyle.Bold);
            btnSaveCsv.ForeColor = Color.White;
            btnSaveCsv.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            btnSaveCsv.IconColor = Color.White;
            btnSaveCsv.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnSaveCsv.IconSize = 18;
            btnSaveCsv.Name = "btnSaveCsv";
            btnSaveCsv.Size = new Size(134, 40);
            btnSaveCsv.Text = "CSV 저장";
            btnSaveCsv.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSaveCsv.UseVisualStyleBackColor = false;
            btnSaveCsv.Location = new Point(6, 6);
            btnSaveCsv.FlatAppearance.BorderSize = 0;
            btnSaveCsv.Click += btnSaveCsv_Click;
            // 
            // btnAddHistory
            // 
            btnAddHistory.BackColor = Color.FromArgb(98, 102, 242);
            btnAddHistory.Cursor = Cursors.Hand;
            btnAddHistory.FlatStyle = FlatStyle.Flat;
            btnAddHistory.Font = new Font("Malgun Gothic", 9.5F, FontStyle.Bold);
            btnAddHistory.ForeColor = Color.White;
            btnAddHistory.IconChar = FontAwesome.Sharp.IconChar.Table;
            btnAddHistory.IconColor = Color.White;
            btnAddHistory.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnAddHistory.IconSize = 18;
            btnAddHistory.Name = "btnAddHistory";
            btnAddHistory.Size = new Size(134, 40);
            btnAddHistory.Text = "결과표에 추가";
            btnAddHistory.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAddHistory.UseVisualStyleBackColor = false;
            btnAddHistory.Location = new Point(146, 6);
            btnAddHistory.FlatAppearance.BorderSize = 0;
            btnAddHistory.Click += btnAddHistory_Click;
            // 
            // grpEo
            // 
            grpEo.Controls.Add(lblR);
            grpEo.Controls.Add(tlpEo);
            grpEo.Controls.Add(lblEoFormula);
            grpEo.Dock = DockStyle.Top;
            grpEo.Font = new Font("Malgun Gothic", 9.5F, FontStyle.Bold);
            grpEo.Name = "grpEo";
            grpEo.Padding = new Padding(6, 4, 6, 6);
            grpEo.Size = new Size(288, 250);
            grpEo.Text = "전기광학 계수";
            // 
            // lblR
            // 
            lblR.Dock = DockStyle.Fill;
            lblR.Font = new Font("Malgun Gothic", 13F, FontStyle.Bold);
            lblR.ForeColor = Color.FromArgb(30, 60, 180);
            lblR.Name = "lblR";
            lblR.Text = "r = — pm/V";
            lblR.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tlpEo
            // 
            tlpEo.Controls.Add(lblEoLambdaName, 0, 0);
            tlpEo.Controls.Add(lblEoLambda, 1, 0);
            tlpEo.Controls.Add(lblEoN, 0, 1);
            tlpEo.Controls.Add(nudN, 1, 1);
            tlpEo.Controls.Add(lblEoD, 0, 2);
            tlpEo.Controls.Add(nudD, 1, 2);
            tlpEo.Controls.Add(lblEoL, 0, 3);
            tlpEo.Controls.Add(nudL, 1, 3);
            tlpEo.Controls.Add(lblEoVpiName, 0, 4);
            tlpEo.Controls.Add(lblEoVpi, 1, 4);
            tlpEo.Controls.Add(btnNHelp, 2, 1);
            tlpEo.ColumnCount = 3;
            tlpEo.Dock = DockStyle.Top;
            tlpEo.Name = "tlpEo";
            tlpEo.RowCount = 5;
            tlpEo.Size = new Size(276, 150);
            tlpEo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52F));
            tlpEo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48F));
            tlpEo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 28F));
            tlpEo.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tlpEo.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tlpEo.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tlpEo.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tlpEo.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            // 
            // lblEoLambdaName
            // 
            lblEoLambdaName.Dock = DockStyle.Fill;
            lblEoLambdaName.Name = "lblEoLambdaName";
            lblEoLambdaName.Text = "파장 λ (nm)";
            lblEoLambdaName.TextAlign = ContentAlignment.MiddleLeft;
            lblEoLambdaName.AutoSize = false;
            lblEoLambdaName.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblEoLambda
            // 
            lblEoLambda.Dock = DockStyle.Fill;
            lblEoLambda.Name = "lblEoLambda";
            lblEoLambda.Text = "1310";
            lblEoLambda.TextAlign = ContentAlignment.MiddleRight;
            lblEoLambda.AutoSize = false;
            lblEoLambda.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblEoN
            // 
            lblEoN.Dock = DockStyle.Fill;
            lblEoN.Name = "lblEoN";
            lblEoN.Text = "굴절률 n";
            lblEoN.TextAlign = ContentAlignment.MiddleLeft;
            lblEoN.AutoSize = false;
            lblEoN.Font = new Font("Malgun Gothic", 9F);
            // 
            // nudN
            // 
            nudN.DecimalPlaces = 4;
            nudN.Dock = DockStyle.Fill;
            nudN.Increment = new decimal(new int[] { 1, 0, 0, 196608 });
            nudN.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            nudN.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudN.Name = "nudN";
            nudN.TextAlign = HorizontalAlignment.Right;
            nudN.Value = new decimal(new int[] { 214, 0, 0, 131072 });
            nudN.Font = new Font("Malgun Gothic", 9F);
            nudN.ValueChanged += nudEo_ValueChanged;
            // 
            // lblEoD
            // 
            lblEoD.Dock = DockStyle.Fill;
            lblEoD.Name = "lblEoD";
            lblEoD.Text = "전극 사이 거리 d (mm)";
            lblEoD.TextAlign = ContentAlignment.MiddleLeft;
            lblEoD.AutoSize = false;
            lblEoD.Font = new Font("Malgun Gothic", 9F);
            // 
            // nudD
            // 
            nudD.DecimalPlaces = 3;
            nudD.Dock = DockStyle.Fill;
            nudD.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudD.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudD.Minimum = new decimal(new int[] { 1, 0, 0, 196608 });
            nudD.Name = "nudD";
            nudD.TextAlign = HorizontalAlignment.Right;
            nudD.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudD.Font = new Font("Malgun Gothic", 9F);
            nudD.ValueChanged += nudEo_ValueChanged;
            // 
            // lblEoL
            // 
            lblEoL.Dock = DockStyle.Fill;
            lblEoL.Name = "lblEoL";
            lblEoL.Text = "LN 길이 L (mm)";
            lblEoL.TextAlign = ContentAlignment.MiddleLeft;
            lblEoL.AutoSize = false;
            lblEoL.Font = new Font("Malgun Gothic", 9F);
            // 
            // nudL
            // 
            nudL.DecimalPlaces = 3;
            nudL.Dock = DockStyle.Fill;
            nudL.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudL.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudL.Minimum = new decimal(new int[] { 1, 0, 0, 196608 });
            nudL.Name = "nudL";
            nudL.TextAlign = HorizontalAlignment.Right;
            nudL.Value = new decimal(new int[] { 10, 0, 0, 0 });
            nudL.Font = new Font("Malgun Gothic", 9F);
            nudL.ValueChanged += nudEo_ValueChanged;
            // 
            // lblEoVpiName
            // 
            lblEoVpiName.Dock = DockStyle.Fill;
            lblEoVpiName.Name = "lblEoVpiName";
            lblEoVpiName.Text = "Vπ (kV, 피팅 w)";
            lblEoVpiName.TextAlign = ContentAlignment.MiddleLeft;
            lblEoVpiName.AutoSize = false;
            lblEoVpiName.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblEoVpi
            // 
            lblEoVpi.Dock = DockStyle.Fill;
            lblEoVpi.Name = "lblEoVpi";
            lblEoVpi.Text = "-";
            lblEoVpi.TextAlign = ContentAlignment.MiddleRight;
            lblEoVpi.AutoSize = false;
            lblEoVpi.Font = new Font("Malgun Gothic", 9F);
            // 
            // btnNHelp
            // 
            btnNHelp.BackColor = Color.White;
            btnNHelp.Cursor = Cursors.Hand;
            btnNHelp.FlatStyle = FlatStyle.Flat;
            btnNHelp.Font = new Font("Malgun Gothic", 9.5F, FontStyle.Bold);
            btnNHelp.ForeColor = Color.White;
            btnNHelp.IconChar = FontAwesome.Sharp.IconChar.CircleQuestion;
            btnNHelp.IconColor = Color.FromArgb(98, 102, 242);
            btnNHelp.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnNHelp.IconSize = 20;
            btnNHelp.Name = "btnNHelp";
            btnNHelp.Size = new Size(26, 27);
            btnNHelp.Text = "";
            btnNHelp.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNHelp.UseVisualStyleBackColor = false;
            btnNHelp.Dock = DockStyle.Fill;
            btnNHelp.Margin = new Padding(1);
            btnNHelp.Padding = new Padding(0);
            btnNHelp.FlatAppearance.BorderSize = 0;
            btnNHelp.Click += btnNHelp_Click;
            // 
            // lblEoFormula
            // 
            lblEoFormula.Dock = DockStyle.Top;
            lblEoFormula.Font = new Font("Malgun Gothic", 10.5F, FontStyle.Bold);
            lblEoFormula.Name = "lblEoFormula";
            lblEoFormula.Size = new Size(276, 30);
            lblEoFormula.Text = "r = λ·d / (n³·L·Vπ)";
            lblEoFormula.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // grpResult
            // 
            grpResult.Controls.Add(lblFitMsg);
            grpResult.Controls.Add(tlpResult);
            grpResult.Controls.Add(lblVpi);
            grpResult.Dock = DockStyle.Top;
            grpResult.Font = new Font("Malgun Gothic", 9.5F, FontStyle.Bold);
            grpResult.Name = "grpResult";
            grpResult.Padding = new Padding(6, 4, 6, 6);
            grpResult.Size = new Size(288, 340);
            grpResult.Text = "피팅 결과";
            // 
            // lblFitMsg
            // 
            lblFitMsg.Dock = DockStyle.Fill;
            lblFitMsg.Font = new Font("Malgun Gothic", 8.5F);
            lblFitMsg.ForeColor = Color.FromArgb(180, 90, 0);
            lblFitMsg.Name = "lblFitMsg";
            lblFitMsg.Text = "y = y0 + A·sin(π(x − xc)/w)";
            lblFitMsg.TextAlign = ContentAlignment.TopLeft;
            // 
            // tlpResult
            // 
            tlpResult.Controls.Add(lblHdrName, 0, 0);
            tlpResult.Controls.Add(lblHdrValue, 1, 0);
            tlpResult.Controls.Add(lblHdrErr, 2, 0);
            tlpResult.Controls.Add(lblNameY0, 0, 1);
            tlpResult.Controls.Add(lblY0, 1, 1);
            tlpResult.Controls.Add(lblY0Err, 2, 1);
            tlpResult.Controls.Add(lblNameXc, 0, 2);
            tlpResult.Controls.Add(lblXc, 1, 2);
            tlpResult.Controls.Add(lblXcErr, 2, 2);
            tlpResult.Controls.Add(lblNameW, 0, 3);
            tlpResult.Controls.Add(lblW, 1, 3);
            tlpResult.Controls.Add(lblWErr, 2, 3);
            tlpResult.Controls.Add(lblNameA, 0, 4);
            tlpResult.Controls.Add(lblA, 1, 4);
            tlpResult.Controls.Add(lblAErr, 2, 4);
            tlpResult.Controls.Add(lblNameChi, 0, 5);
            tlpResult.Controls.Add(lblChi, 1, 5);
            tlpResult.Controls.Add(lblNameR2, 0, 6);
            tlpResult.Controls.Add(lblR2, 1, 6);
            tlpResult.Controls.Add(lblNameN, 0, 7);
            tlpResult.Controls.Add(lblN, 1, 7);
            tlpResult.ColumnCount = 3;
            tlpResult.Dock = DockStyle.Top;
            tlpResult.Name = "tlpResult";
            tlpResult.RowCount = 8;
            tlpResult.Size = new Size(276, 200);
            tlpResult.SetColumnSpan(lblChi, 2);
            tlpResult.SetColumnSpan(lblR2, 2);
            tlpResult.SetColumnSpan(lblN, 2);
            tlpResult.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tlpResult.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tlpResult.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tlpResult.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tlpResult.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tlpResult.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tlpResult.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tlpResult.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tlpResult.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tlpResult.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tlpResult.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            // 
            // lblHdrName
            // 
            lblHdrName.Dock = DockStyle.Fill;
            lblHdrName.Name = "lblHdrName";
            lblHdrName.Text = "파라미터";
            lblHdrName.TextAlign = ContentAlignment.MiddleLeft;
            lblHdrName.AutoSize = false;
            lblHdrName.ForeColor = Color.DimGray;
            lblHdrName.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblHdrValue
            // 
            lblHdrValue.Dock = DockStyle.Fill;
            lblHdrValue.Name = "lblHdrValue";
            lblHdrValue.Text = "값";
            lblHdrValue.TextAlign = ContentAlignment.MiddleRight;
            lblHdrValue.AutoSize = false;
            lblHdrValue.ForeColor = Color.DimGray;
            lblHdrValue.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblHdrErr
            // 
            lblHdrErr.Dock = DockStyle.Fill;
            lblHdrErr.Name = "lblHdrErr";
            lblHdrErr.Text = "표준오차";
            lblHdrErr.TextAlign = ContentAlignment.MiddleRight;
            lblHdrErr.AutoSize = false;
            lblHdrErr.ForeColor = Color.DimGray;
            lblHdrErr.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblNameY0
            // 
            lblNameY0.Dock = DockStyle.Fill;
            lblNameY0.Name = "lblNameY0";
            lblNameY0.Text = "y0 (V)";
            lblNameY0.TextAlign = ContentAlignment.MiddleLeft;
            lblNameY0.AutoSize = false;
            lblNameY0.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblY0
            // 
            lblY0.Dock = DockStyle.Fill;
            lblY0.Name = "lblY0";
            lblY0.Text = "-";
            lblY0.TextAlign = ContentAlignment.MiddleRight;
            lblY0.AutoSize = false;
            lblY0.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblY0Err
            // 
            lblY0Err.Dock = DockStyle.Fill;
            lblY0Err.Name = "lblY0Err";
            lblY0Err.Text = "-";
            lblY0Err.TextAlign = ContentAlignment.MiddleRight;
            lblY0Err.AutoSize = false;
            lblY0Err.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblNameXc
            // 
            lblNameXc.Dock = DockStyle.Fill;
            lblNameXc.Name = "lblNameXc";
            lblNameXc.Text = "xc (kV)";
            lblNameXc.TextAlign = ContentAlignment.MiddleLeft;
            lblNameXc.AutoSize = false;
            lblNameXc.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblXc
            // 
            lblXc.Dock = DockStyle.Fill;
            lblXc.Name = "lblXc";
            lblXc.Text = "-";
            lblXc.TextAlign = ContentAlignment.MiddleRight;
            lblXc.AutoSize = false;
            lblXc.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblXcErr
            // 
            lblXcErr.Dock = DockStyle.Fill;
            lblXcErr.Name = "lblXcErr";
            lblXcErr.Text = "-";
            lblXcErr.TextAlign = ContentAlignment.MiddleRight;
            lblXcErr.AutoSize = false;
            lblXcErr.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblNameW
            // 
            lblNameW.Dock = DockStyle.Fill;
            lblNameW.Name = "lblNameW";
            lblNameW.Text = "w = Vπ (kV)";
            lblNameW.TextAlign = ContentAlignment.MiddleLeft;
            lblNameW.AutoSize = false;
            lblNameW.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblW
            // 
            lblW.Dock = DockStyle.Fill;
            lblW.Name = "lblW";
            lblW.Text = "-";
            lblW.TextAlign = ContentAlignment.MiddleRight;
            lblW.AutoSize = false;
            lblW.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblWErr
            // 
            lblWErr.Dock = DockStyle.Fill;
            lblWErr.Name = "lblWErr";
            lblWErr.Text = "-";
            lblWErr.TextAlign = ContentAlignment.MiddleRight;
            lblWErr.AutoSize = false;
            lblWErr.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblNameA
            // 
            lblNameA.Dock = DockStyle.Fill;
            lblNameA.Name = "lblNameA";
            lblNameA.Text = "A (V)";
            lblNameA.TextAlign = ContentAlignment.MiddleLeft;
            lblNameA.AutoSize = false;
            lblNameA.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblA
            // 
            lblA.Dock = DockStyle.Fill;
            lblA.Name = "lblA";
            lblA.Text = "-";
            lblA.TextAlign = ContentAlignment.MiddleRight;
            lblA.AutoSize = false;
            lblA.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblAErr
            // 
            lblAErr.Dock = DockStyle.Fill;
            lblAErr.Name = "lblAErr";
            lblAErr.Text = "-";
            lblAErr.TextAlign = ContentAlignment.MiddleRight;
            lblAErr.AutoSize = false;
            lblAErr.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblNameChi
            // 
            lblNameChi.Dock = DockStyle.Fill;
            lblNameChi.Name = "lblNameChi";
            lblNameChi.Text = "Reduced χ²";
            lblNameChi.TextAlign = ContentAlignment.MiddleLeft;
            lblNameChi.AutoSize = false;
            lblNameChi.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblChi
            // 
            lblChi.Dock = DockStyle.Fill;
            lblChi.Name = "lblChi";
            lblChi.Text = "-";
            lblChi.TextAlign = ContentAlignment.MiddleRight;
            lblChi.AutoSize = false;
            lblChi.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblNameR2
            // 
            lblNameR2.Dock = DockStyle.Fill;
            lblNameR2.Name = "lblNameR2";
            lblNameR2.Text = "Adj. R²";
            lblNameR2.TextAlign = ContentAlignment.MiddleLeft;
            lblNameR2.AutoSize = false;
            lblNameR2.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblR2
            // 
            lblR2.Dock = DockStyle.Fill;
            lblR2.Name = "lblR2";
            lblR2.Text = "-";
            lblR2.TextAlign = ContentAlignment.MiddleRight;
            lblR2.AutoSize = false;
            lblR2.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblNameN
            // 
            lblNameN.Dock = DockStyle.Fill;
            lblNameN.Name = "lblNameN";
            lblNameN.Text = "사용 점 / 전체";
            lblNameN.TextAlign = ContentAlignment.MiddleLeft;
            lblNameN.AutoSize = false;
            lblNameN.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblN
            // 
            lblN.Dock = DockStyle.Fill;
            lblN.Name = "lblN";
            lblN.Text = "-";
            lblN.TextAlign = ContentAlignment.MiddleRight;
            lblN.AutoSize = false;
            lblN.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblVpi
            // 
            lblVpi.Dock = DockStyle.Top;
            lblVpi.Font = new Font("Malgun Gothic", 14F, FontStyle.Bold);
            lblVpi.ForeColor = Color.FromArgb(200, 30, 30);
            lblVpi.Name = "lblVpi";
            lblVpi.Size = new Size(276, 40);
            lblVpi.Text = "Vπ = — kV";
            lblVpi.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelLeft
            // 
            panelLeft.Controls.Add(grpFit);
            panelLeft.Controls.Add(grpScan);
            panelLeft.Controls.Add(grpInfo);
            panelLeft.Controls.Add(panelRun);
            panelLeft.AutoScroll = true;
            panelLeft.Dock = DockStyle.Left;
            panelLeft.Name = "panelLeft";
            panelLeft.Padding = new Padding(6, 6, 4, 6);
            panelLeft.Size = new Size(300, 760);
            // 
            // grpFit
            // 
            grpFit.Controls.Add(tlpFit);
            grpFit.Dock = DockStyle.Top;
            grpFit.Font = new Font("Malgun Gothic", 9.5F, FontStyle.Bold);
            grpFit.Name = "grpFit";
            grpFit.Padding = new Padding(6, 4, 6, 6);
            grpFit.Size = new Size(288, 226);
            grpFit.Text = "사인 피팅 옵션 (측정 완료 후 1회 피팅)";
            // 
            // tlpFit
            // 
            tlpFit.Controls.Add(lblXSource, 0, 0);
            tlpFit.Controls.Add(cboXSource, 1, 0);
            tlpFit.Controls.Add(lblSelection, 0, 1);
            tlpFit.Controls.Add(cboSelection, 1, 1);
            tlpFit.Controls.Add(chkExclude, 0, 2);
            tlpFit.Controls.Add(chkFixY0, 0, 3);
            tlpFit.Controls.Add(nudFixY0, 1, 3);
            tlpFit.Controls.Add(chkFixA, 0, 4);
            tlpFit.Controls.Add(nudFixA, 1, 4);
            tlpFit.Controls.Add(btnRefit, 0, 5);
            tlpFit.ColumnCount = 2;
            tlpFit.Dock = DockStyle.Fill;
            tlpFit.Name = "tlpFit";
            tlpFit.RowCount = 7;
            tlpFit.SetColumnSpan(chkExclude, 2);
            tlpFit.SetColumnSpan(btnRefit, 2);
            tlpFit.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52F));
            tlpFit.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48F));
            tlpFit.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tlpFit.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tlpFit.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tlpFit.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tlpFit.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tlpFit.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            tlpFit.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            // 
            // lblXSource
            // 
            lblXSource.Dock = DockStyle.Fill;
            lblXSource.Name = "lblXSource";
            lblXSource.Text = "x축 전압";
            lblXSource.TextAlign = ContentAlignment.MiddleLeft;
            lblXSource.AutoSize = false;
            lblXSource.Font = new Font("Malgun Gothic", 9F);
            // 
            // cboXSource
            // 
            cboXSource.Dock = DockStyle.Fill;
            cboXSource.DropDownStyle = ComboBoxStyle.DropDownList;
            cboXSource.Font = new Font("Malgun Gothic", 9F);
            cboXSource.Name = "cboXSource";
            cboXSource.Items.AddRange(new object[] { "설정 전압", "모니터 (CH2)" });
            cboXSource.SelectedIndexChanged += FitOption_Changed;
            // 
            // lblSelection
            // 
            lblSelection.Dock = DockStyle.Fill;
            lblSelection.Name = "lblSelection";
            lblSelection.Text = "피팅 데이터";
            lblSelection.TextAlign = ContentAlignment.MiddleLeft;
            lblSelection.AutoSize = false;
            lblSelection.Font = new Font("Malgun Gothic", 9F);
            // 
            // cboSelection
            // 
            cboSelection.Dock = DockStyle.Fill;
            cboSelection.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSelection.Font = new Font("Malgun Gothic", 9F);
            cboSelection.Name = "cboSelection";
            cboSelection.Items.AddRange(new object[] { "전체", "정방향만", "역방향만" });
            cboSelection.SelectedIndexChanged += FitOption_Changed;
            // 
            // chkExclude
            // 
            chkExclude.Dock = DockStyle.Fill;
            chkExclude.Font = new Font("Malgun Gothic", 9F);
            chkExclude.Name = "chkExclude";
            chkExclude.Text = "레인지 초과·포화 의심 점 제외";
            chkExclude.CheckedChanged += FitOption_Changed;
            // 
            // chkFixY0
            // 
            chkFixY0.Dock = DockStyle.Fill;
            chkFixY0.Font = new Font("Malgun Gothic", 9F);
            chkFixY0.Name = "chkFixY0";
            chkFixY0.Text = "y0 고정 (V)";
            chkFixY0.CheckedChanged += FitOption_Changed;
            // 
            // nudFixY0
            // 
            nudFixY0.DecimalPlaces = 4;
            nudFixY0.Dock = DockStyle.Fill;
            nudFixY0.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            nudFixY0.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            nudFixY0.Minimum = new decimal(new int[] { 20, 0, 0, -2147483648 });
            nudFixY0.Name = "nudFixY0";
            nudFixY0.TextAlign = HorizontalAlignment.Right;
            nudFixY0.Value = new decimal(new int[] { 15, 0, 0, 65536 });
            nudFixY0.Font = new Font("Malgun Gothic", 9F);
            nudFixY0.ValueChanged += FitOption_Changed;
            // 
            // chkFixA
            // 
            chkFixA.Dock = DockStyle.Fill;
            chkFixA.Font = new Font("Malgun Gothic", 9F);
            chkFixA.Name = "chkFixA";
            chkFixA.Text = "A 고정 (V)";
            chkFixA.CheckedChanged += FitOption_Changed;
            // 
            // nudFixA
            // 
            nudFixA.DecimalPlaces = 4;
            nudFixA.Dock = DockStyle.Fill;
            nudFixA.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            nudFixA.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            nudFixA.Minimum = new decimal(new int[] { 1, 0, 0, 262144 });
            nudFixA.Name = "nudFixA";
            nudFixA.TextAlign = HorizontalAlignment.Right;
            nudFixA.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudFixA.Font = new Font("Malgun Gothic", 9F);
            nudFixA.ValueChanged += FitOption_Changed;
            // 
            // btnRefit
            // 
            btnRefit.BackColor = Color.FromArgb(98, 102, 242);
            btnRefit.Cursor = Cursors.Hand;
            btnRefit.FlatStyle = FlatStyle.Flat;
            btnRefit.Font = new Font("Malgun Gothic", 9.5F, FontStyle.Bold);
            btnRefit.ForeColor = Color.White;
            btnRefit.IconChar = FontAwesome.Sharp.IconChar.ArrowsRotate;
            btnRefit.IconColor = Color.White;
            btnRefit.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnRefit.IconSize = 16;
            btnRefit.Name = "btnRefit";
            btnRefit.Size = new Size(130, 36);
            btnRefit.Text = "현재 데이터로 다시 피팅";
            btnRefit.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRefit.UseVisualStyleBackColor = false;
            btnRefit.Dock = DockStyle.Fill;
            btnRefit.Margin = new Padding(2);
            btnRefit.FlatAppearance.BorderSize = 0;
            btnRefit.Click += btnRefit_Click;
            // 
            // grpScan
            // 
            grpScan.Controls.Add(tlpScan);
            grpScan.Dock = DockStyle.Top;
            grpScan.Font = new Font("Malgun Gothic", 9.5F, FontStyle.Bold);
            grpScan.Name = "grpScan";
            grpScan.Padding = new Padding(6, 4, 6, 6);
            grpScan.Size = new Size(288, 266);
            grpScan.Text = "전압 스캔";
            // 
            // tlpScan
            // 
            tlpScan.Controls.Add(lblStart, 0, 0);
            tlpScan.Controls.Add(nudStart, 1, 0);
            tlpScan.Controls.Add(lblEnd, 0, 1);
            tlpScan.Controls.Add(nudEnd, 1, 1);
            tlpScan.Controls.Add(lblStep, 0, 2);
            tlpScan.Controls.Add(nudStep, 1, 2);
            tlpScan.Controls.Add(lblSettle, 0, 3);
            tlpScan.Controls.Add(nudSettle, 1, 3);
            tlpScan.Controls.Add(lblAverage, 0, 4);
            tlpScan.Controls.Add(nudAverage, 1, 4);
            tlpScan.Controls.Add(lblMode, 0, 5);
            tlpScan.Controls.Add(cboMode, 1, 5);
            tlpScan.Controls.Add(lblRepeats, 0, 6);
            tlpScan.Controls.Add(nudRepeats, 1, 6);
            tlpScan.Controls.Add(lblPlan, 0, 7);
            tlpScan.ColumnCount = 2;
            tlpScan.Dock = DockStyle.Fill;
            tlpScan.Name = "tlpScan";
            tlpScan.RowCount = 9;
            tlpScan.SetColumnSpan(lblPlan, 2);
            tlpScan.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52F));
            tlpScan.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48F));
            tlpScan.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tlpScan.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tlpScan.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tlpScan.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tlpScan.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tlpScan.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tlpScan.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tlpScan.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tlpScan.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            // 
            // lblStart
            // 
            lblStart.Dock = DockStyle.Fill;
            lblStart.Name = "lblStart";
            lblStart.Text = "시작 전압 (kV)";
            lblStart.TextAlign = ContentAlignment.MiddleLeft;
            lblStart.AutoSize = false;
            lblStart.Font = new Font("Malgun Gothic", 9F);
            // 
            // nudStart
            // 
            nudStart.DecimalPlaces = 3;
            nudStart.Dock = DockStyle.Fill;
            nudStart.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudStart.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            nudStart.Minimum = new decimal(new int[] { 1, 0, 0, -2147483648 });
            nudStart.Name = "nudStart";
            nudStart.TextAlign = HorizontalAlignment.Right;
            nudStart.Value = new decimal(new int[] { 1, 0, 0, -2147483648 });
            nudStart.Font = new Font("Malgun Gothic", 9F);
            nudStart.ValueChanged += ScanInput_Changed;
            // 
            // lblEnd
            // 
            lblEnd.Dock = DockStyle.Fill;
            lblEnd.Name = "lblEnd";
            lblEnd.Text = "끝 전압 (kV)";
            lblEnd.TextAlign = ContentAlignment.MiddleLeft;
            lblEnd.AutoSize = false;
            lblEnd.Font = new Font("Malgun Gothic", 9F);
            // 
            // nudEnd
            // 
            nudEnd.DecimalPlaces = 3;
            nudEnd.Dock = DockStyle.Fill;
            nudEnd.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudEnd.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            nudEnd.Minimum = new decimal(new int[] { 1, 0, 0, -2147483648 });
            nudEnd.Name = "nudEnd";
            nudEnd.TextAlign = HorizontalAlignment.Right;
            nudEnd.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudEnd.Font = new Font("Malgun Gothic", 9F);
            nudEnd.ValueChanged += ScanInput_Changed;
            // 
            // lblStep
            // 
            lblStep.Dock = DockStyle.Fill;
            lblStep.Name = "lblStep";
            lblStep.Text = "전압 간격 (kV)";
            lblStep.TextAlign = ContentAlignment.MiddleLeft;
            lblStep.AutoSize = false;
            lblStep.Font = new Font("Malgun Gothic", 9F);
            // 
            // nudStep
            // 
            nudStep.DecimalPlaces = 3;
            nudStep.Dock = DockStyle.Fill;
            nudStep.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            nudStep.Maximum = new decimal(new int[] { 2, 0, 0, 0 });
            nudStep.Minimum = new decimal(new int[] { 1, 0, 0, 196608 });
            nudStep.Name = "nudStep";
            nudStep.TextAlign = HorizontalAlignment.Right;
            nudStep.Value = new decimal(new int[] { 1, 0, 0, 65536 });
            nudStep.Font = new Font("Malgun Gothic", 9F);
            nudStep.ValueChanged += ScanInput_Changed;
            // 
            // lblSettle
            // 
            lblSettle.Dock = DockStyle.Fill;
            lblSettle.Name = "lblSettle";
            lblSettle.Text = "안정화 시간 (ms)";
            lblSettle.TextAlign = ContentAlignment.MiddleLeft;
            lblSettle.AutoSize = false;
            lblSettle.Font = new Font("Malgun Gothic", 9F);
            // 
            // nudSettle
            // 
            nudSettle.DecimalPlaces = 0;
            nudSettle.Dock = DockStyle.Fill;
            nudSettle.Increment = new decimal(new int[] { 1, 0, 0, 0 });
            nudSettle.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudSettle.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            nudSettle.Name = "nudSettle";
            nudSettle.TextAlign = HorizontalAlignment.Right;
            nudSettle.Value = new decimal(new int[] { 50, 0, 0, 0 });
            nudSettle.Font = new Font("Malgun Gothic", 9F);
            nudSettle.ValueChanged += ScanInput_Changed;
            // 
            // lblAverage
            // 
            lblAverage.Dock = DockStyle.Fill;
            lblAverage.Name = "lblAverage";
            lblAverage.Text = "평균 시간 (ms)";
            lblAverage.TextAlign = ContentAlignment.MiddleLeft;
            lblAverage.AutoSize = false;
            lblAverage.Font = new Font("Malgun Gothic", 9F);
            // 
            // nudAverage
            // 
            nudAverage.DecimalPlaces = 1;
            nudAverage.Dock = DockStyle.Fill;
            nudAverage.Increment = new decimal(new int[] { 1, 0, 0, 0 });
            nudAverage.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
            nudAverage.Minimum = new decimal(new int[] { 1, 0, 0, 65536 });
            nudAverage.Name = "nudAverage";
            nudAverage.TextAlign = HorizontalAlignment.Right;
            nudAverage.Value = new decimal(new int[] { 10, 0, 0, 0 });
            nudAverage.Font = new Font("Malgun Gothic", 9F);
            nudAverage.ValueChanged += ScanInput_Changed;
            // 
            // lblMode
            // 
            lblMode.Dock = DockStyle.Fill;
            lblMode.Name = "lblMode";
            lblMode.Text = "스캔 방식";
            lblMode.TextAlign = ContentAlignment.MiddleLeft;
            lblMode.AutoSize = false;
            lblMode.Font = new Font("Malgun Gothic", 9F);
            // 
            // cboMode
            // 
            cboMode.Dock = DockStyle.Fill;
            cboMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMode.Font = new Font("Malgun Gothic", 9F);
            cboMode.Name = "cboMode";
            cboMode.Items.AddRange(new object[] { "단방향 (시작→끝)", "왕복 (시작→끝→시작)" });
            cboMode.SelectedIndexChanged += ScanInput_Changed;
            // 
            // lblRepeats
            // 
            lblRepeats.Dock = DockStyle.Fill;
            lblRepeats.Name = "lblRepeats";
            lblRepeats.Text = "반복 횟수";
            lblRepeats.TextAlign = ContentAlignment.MiddleLeft;
            lblRepeats.AutoSize = false;
            lblRepeats.Font = new Font("Malgun Gothic", 9F);
            // 
            // nudRepeats
            // 
            nudRepeats.DecimalPlaces = 0;
            nudRepeats.Dock = DockStyle.Fill;
            nudRepeats.Increment = new decimal(new int[] { 1, 0, 0, 0 });
            nudRepeats.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            nudRepeats.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudRepeats.Name = "nudRepeats";
            nudRepeats.TextAlign = HorizontalAlignment.Right;
            nudRepeats.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudRepeats.Font = new Font("Malgun Gothic", 9F);
            nudRepeats.ValueChanged += ScanInput_Changed;
            // 
            // lblPlan
            // 
            lblPlan.Dock = DockStyle.Fill;
            lblPlan.Name = "lblPlan";
            lblPlan.Text = "측정점 21개 · 예상 소요 00:01.6";
            lblPlan.TextAlign = ContentAlignment.MiddleLeft;
            lblPlan.AutoSize = false;
            lblPlan.ForeColor = Color.DarkBlue;
            lblPlan.Font = new Font("Malgun Gothic", 9F, FontStyle.Bold);
            // 
            // grpInfo
            // 
            grpInfo.Controls.Add(tlpInfo);
            grpInfo.Dock = DockStyle.Top;
            grpInfo.Font = new Font("Malgun Gothic", 9.5F, FontStyle.Bold);
            grpInfo.Name = "grpInfo";
            grpInfo.Padding = new Padding(6, 4, 6, 6);
            grpInfo.Size = new Size(288, 150);
            grpInfo.Text = "측정 조건 (결과에 함께 기록)";
            // 
            // tlpInfo
            // 
            tlpInfo.Controls.Add(lblSample, 0, 0);
            tlpInfo.Controls.Add(txtSample, 1, 0);
            tlpInfo.Controls.Add(lblWavelength, 0, 1);
            tlpInfo.Controls.Add(nudWavelength, 1, 1);
            tlpInfo.Controls.Add(lblTemperature, 0, 2);
            tlpInfo.Controls.Add(nudTemperature, 1, 2);
            tlpInfo.Controls.Add(lblMemo, 0, 3);
            tlpInfo.Controls.Add(txtMemo, 1, 3);
            tlpInfo.ColumnCount = 2;
            tlpInfo.Dock = DockStyle.Fill;
            tlpInfo.Name = "tlpInfo";
            tlpInfo.RowCount = 5;
            tlpInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
            tlpInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58F));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tlpInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            // 
            // lblSample
            // 
            lblSample.Dock = DockStyle.Fill;
            lblSample.Name = "lblSample";
            lblSample.Text = "시료 이름";
            lblSample.TextAlign = ContentAlignment.MiddleLeft;
            lblSample.AutoSize = false;
            lblSample.Font = new Font("Malgun Gothic", 9F);
            // 
            // txtSample
            // 
            txtSample.Dock = DockStyle.Fill;
            txtSample.Font = new Font("Malgun Gothic", 9F);
            txtSample.Name = "txtSample";
            // 
            // lblWavelength
            // 
            lblWavelength.Dock = DockStyle.Fill;
            lblWavelength.Name = "lblWavelength";
            lblWavelength.Text = "파장 λ (nm)";
            lblWavelength.TextAlign = ContentAlignment.MiddleLeft;
            lblWavelength.AutoSize = false;
            lblWavelength.Font = new Font("Malgun Gothic", 9F);
            // 
            // nudWavelength
            // 
            nudWavelength.DecimalPlaces = 1;
            nudWavelength.Dock = DockStyle.Fill;
            nudWavelength.Increment = new decimal(new int[] { 1, 0, 0, 0 });
            nudWavelength.Maximum = new decimal(new int[] { 5000, 0, 0, 0 });
            nudWavelength.Minimum = new decimal(new int[] { 100, 0, 0, 0 });
            nudWavelength.Name = "nudWavelength";
            nudWavelength.TextAlign = HorizontalAlignment.Right;
            nudWavelength.Value = new decimal(new int[] { 1310, 0, 0, 0 });
            nudWavelength.Font = new Font("Malgun Gothic", 9F);
            nudWavelength.ValueChanged += nudEo_ValueChanged;
            // 
            // lblTemperature
            // 
            lblTemperature.Dock = DockStyle.Fill;
            lblTemperature.Name = "lblTemperature";
            lblTemperature.Text = "온도 (℃)";
            lblTemperature.TextAlign = ContentAlignment.MiddleLeft;
            lblTemperature.AutoSize = false;
            lblTemperature.Font = new Font("Malgun Gothic", 9F);
            // 
            // nudTemperature
            // 
            nudTemperature.DecimalPlaces = 1;
            nudTemperature.Dock = DockStyle.Fill;
            nudTemperature.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            nudTemperature.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            nudTemperature.Minimum = new decimal(new int[] { 100, 0, 0, -2147483648 });
            nudTemperature.Name = "nudTemperature";
            nudTemperature.TextAlign = HorizontalAlignment.Right;
            nudTemperature.Value = new decimal(new int[] { 25, 0, 0, 0 });
            nudTemperature.Font = new Font("Malgun Gothic", 9F);
            // 
            // lblMemo
            // 
            lblMemo.Dock = DockStyle.Fill;
            lblMemo.Name = "lblMemo";
            lblMemo.Text = "메모";
            lblMemo.TextAlign = ContentAlignment.MiddleLeft;
            lblMemo.AutoSize = false;
            lblMemo.Font = new Font("Malgun Gothic", 9F);
            // 
            // txtMemo
            // 
            txtMemo.Dock = DockStyle.Fill;
            txtMemo.Font = new Font("Malgun Gothic", 9F);
            txtMemo.Name = "txtMemo";
            // 
            // panelRun
            // 
            panelRun.Controls.Add(btnStart);
            panelRun.Controls.Add(btnStop);
            panelRun.Dock = DockStyle.Top;
            panelRun.Name = "panelRun";
            panelRun.Size = new Size(288, 64);
            // 
            // btnStart
            // 
            btnStart.BackColor = Color.FromArgb(46, 160, 67);
            btnStart.Cursor = Cursors.Hand;
            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.Font = new Font("Malgun Gothic", 9.5F, FontStyle.Bold);
            btnStart.ForeColor = Color.White;
            btnStart.IconChar = FontAwesome.Sharp.IconChar.Play;
            btnStart.IconColor = Color.White;
            btnStart.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnStart.IconSize = 24;
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(138, 48);
            btnStart.Text = "측정 시작";
            btnStart.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnStart.UseVisualStyleBackColor = false;
            btnStart.Location = new Point(6, 8);
            btnStart.Font = new Font("Malgun Gothic", 11F, FontStyle.Bold);
            btnStart.FlatAppearance.BorderSize = 0;
            btnStart.Click += btnStart_Click;
            // 
            // btnStop
            // 
            btnStop.BackColor = Color.FromArgb(220, 53, 69);
            btnStop.Cursor = Cursors.Hand;
            btnStop.FlatStyle = FlatStyle.Flat;
            btnStop.Font = new Font("Malgun Gothic", 9.5F, FontStyle.Bold);
            btnStop.ForeColor = Color.White;
            btnStop.IconChar = FontAwesome.Sharp.IconChar.Stop;
            btnStop.IconColor = Color.White;
            btnStop.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnStop.IconSize = 24;
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(138, 48);
            btnStop.Text = "정지";
            btnStop.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnStop.UseVisualStyleBackColor = false;
            btnStop.Location = new Point(150, 8);
            btnStop.Font = new Font("Malgun Gothic", 11F, FontStyle.Bold);
            btnStop.Enabled = false;
            btnStop.FlatAppearance.BorderSize = 0;
            btnStop.Click += btnStop_Click;
            // 
            // panelTop
            // 
            panelTop.Controls.Add(flowTop);
            panelTop.BackColor = Color.White;
            panelTop.Dock = DockStyle.Top;
            panelTop.Name = "panelTop";
            panelTop.Padding = new Padding(4, 2, 4, 2);
            panelTop.Size = new Size(1400, 42);
            // 
            // flowTop
            // 
            flowTop.Controls.Add(lblDevKind);
            flowTop.Controls.Add(cboDevice);
            flowTop.Controls.Add(btnConnect);
            flowTop.Controls.Add(btnDisconnect);
            flowTop.Controls.Add(lblSep1);
            flowTop.Controls.Add(lblManual);
            flowTop.Controls.Add(nudManualKv);
            flowTop.Controls.Add(btnApplyOutput);
            flowTop.Controls.Add(btnZero);
            flowTop.Controls.Add(lblHv);
            flowTop.Controls.Add(lblSep2);
            flowTop.Controls.Add(chkLive);
            flowTop.Controls.Add(lblLive);
            flowTop.Dock = DockStyle.Fill;
            flowTop.Name = "flowTop";
            flowTop.WrapContents = false;
            // 
            // lblDevKind
            // 
            lblDevKind.Anchor = AnchorStyles.Left;
            lblDevKind.AutoSize = true;
            lblDevKind.Margin = new Padding(8, 11, 2, 0);
            lblDevKind.Name = "lblDevKind";
            lblDevKind.Text = "장비";
            // 
            // cboDevice
            // 
            cboDevice.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDevice.Margin = new Padding(2, 7, 2, 0);
            cboDevice.Name = "cboDevice";
            cboDevice.Size = new Size(200, 23);
            cboDevice.Items.AddRange(new object[] { "TiePie HS5 (실제 장비)", "데모 (가상 장비)" });
            cboDevice.SelectedIndexChanged += cboDevice_SelectedIndexChanged;
            // 
            // btnConnect
            // 
            btnConnect.BackColor = Color.FromArgb(98, 102, 242);
            btnConnect.Cursor = Cursors.Hand;
            btnConnect.FlatStyle = FlatStyle.Flat;
            btnConnect.Font = new Font("Malgun Gothic", 9.5F, FontStyle.Bold);
            btnConnect.ForeColor = Color.White;
            btnConnect.IconChar = FontAwesome.Sharp.IconChar.Plug;
            btnConnect.IconColor = Color.White;
            btnConnect.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnConnect.IconSize = 18;
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(90, 32);
            btnConnect.Text = "연결";
            btnConnect.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnConnect.UseVisualStyleBackColor = false;
            btnConnect.Margin = new Padding(6, 3, 2, 0);
            btnConnect.FlatAppearance.BorderSize = 0;
            btnConnect.Click += btnConnect_Click;
            // 
            // btnDisconnect
            // 
            btnDisconnect.BackColor = Color.FromArgb(108, 117, 125);
            btnDisconnect.Cursor = Cursors.Hand;
            btnDisconnect.FlatStyle = FlatStyle.Flat;
            btnDisconnect.Font = new Font("Malgun Gothic", 9.5F, FontStyle.Bold);
            btnDisconnect.ForeColor = Color.White;
            btnDisconnect.IconChar = FontAwesome.Sharp.IconChar.PowerOff;
            btnDisconnect.IconColor = Color.White;
            btnDisconnect.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnDisconnect.IconSize = 18;
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(90, 32);
            btnDisconnect.Text = "해제";
            btnDisconnect.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDisconnect.UseVisualStyleBackColor = false;
            btnDisconnect.Margin = new Padding(2, 3, 2, 0);
            btnDisconnect.FlatAppearance.BorderSize = 0;
            btnDisconnect.Click += btnDisconnect_Click;
            // 
            // lblSep1
            // 
            lblSep1.Anchor = AnchorStyles.Left;
            lblSep1.AutoSize = true;
            lblSep1.Margin = new Padding(8, 11, 2, 0);
            lblSep1.Name = "lblSep1";
            lblSep1.Text = "|";
            lblSep1.ForeColor = Color.Silver;
            // 
            // lblManual
            // 
            lblManual.Anchor = AnchorStyles.Left;
            lblManual.AutoSize = true;
            lblManual.Margin = new Padding(8, 11, 2, 0);
            lblManual.Name = "lblManual";
            lblManual.Text = "수동 출력 (kV)";
            // 
            // nudManualKv
            // 
            nudManualKv.DecimalPlaces = 3;
            nudManualKv.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudManualKv.Margin = new Padding(2, 8, 2, 0);
            nudManualKv.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            nudManualKv.Minimum = new decimal(new int[] { 1, 0, 0, -2147483648 });
            nudManualKv.Name = "nudManualKv";
            nudManualKv.Size = new Size(80, 23);
            nudManualKv.TextAlign = HorizontalAlignment.Right;
            // 
            // btnApplyOutput
            // 
            btnApplyOutput.BackColor = Color.FromArgb(255, 140, 0);
            btnApplyOutput.Cursor = Cursors.Hand;
            btnApplyOutput.FlatStyle = FlatStyle.Flat;
            btnApplyOutput.Font = new Font("Malgun Gothic", 9.5F, FontStyle.Bold);
            btnApplyOutput.ForeColor = Color.White;
            btnApplyOutput.IconChar = FontAwesome.Sharp.IconChar.Bolt;
            btnApplyOutput.IconColor = Color.White;
            btnApplyOutput.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnApplyOutput.IconSize = 16;
            btnApplyOutput.Name = "btnApplyOutput";
            btnApplyOutput.Size = new Size(76, 32);
            btnApplyOutput.Text = "출력";
            btnApplyOutput.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnApplyOutput.UseVisualStyleBackColor = false;
            btnApplyOutput.Margin = new Padding(4, 3, 2, 0);
            btnApplyOutput.FlatAppearance.BorderSize = 0;
            btnApplyOutput.Click += btnApplyOutput_Click;
            // 
            // btnZero
            // 
            btnZero.BackColor = Color.FromArgb(108, 117, 125);
            btnZero.Cursor = Cursors.Hand;
            btnZero.FlatStyle = FlatStyle.Flat;
            btnZero.Font = new Font("Malgun Gothic", 9.5F, FontStyle.Bold);
            btnZero.ForeColor = Color.White;
            btnZero.IconChar = FontAwesome.Sharp.IconChar.PowerOff;
            btnZero.IconColor = Color.White;
            btnZero.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnZero.IconSize = 16;
            btnZero.Name = "btnZero";
            btnZero.Size = new Size(70, 32);
            btnZero.Text = "0 V";
            btnZero.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnZero.UseVisualStyleBackColor = false;
            btnZero.Margin = new Padding(2, 3, 2, 0);
            btnZero.FlatAppearance.BorderSize = 0;
            btnZero.Click += btnZero_Click;
            // 
            // lblHv
            // 
            lblHv.Anchor = AnchorStyles.Left;
            lblHv.AutoSize = true;
            lblHv.Margin = new Padding(8, 11, 2, 0);
            lblHv.Name = "lblHv";
            lblHv.Text = "출력 0.000 kV";
            lblHv.Font = new Font("Malgun Gothic", 9.5F, FontStyle.Bold);
            lblHv.ForeColor = Color.DimGray;
            // 
            // lblSep2
            // 
            lblSep2.Anchor = AnchorStyles.Left;
            lblSep2.AutoSize = true;
            lblSep2.Margin = new Padding(8, 11, 2, 0);
            lblSep2.Name = "lblSep2";
            lblSep2.Text = "|";
            lblSep2.ForeColor = Color.Silver;
            // 
            // chkLive
            // 
            chkLive.AutoSize = true;
            chkLive.Margin = new Padding(8, 10, 2, 0);
            chkLive.Name = "chkLive";
            chkLive.Text = "실시간 광신호 보기";
            chkLive.CheckedChanged += chkLive_CheckedChanged;
            // 
            // lblLive
            // 
            lblLive.Anchor = AnchorStyles.Left;
            lblLive.AutoSize = true;
            lblLive.Margin = new Padding(8, 11, 2, 0);
            lblLive.Name = "lblLive";
            lblLive.Text = "-";
            lblLive.Font = new Font("Malgun Gothic", 10F, FontStyle.Bold);
            lblLive.ForeColor = Color.DarkBlue;
            // 
            // uiTimer
            // 
            uiTimer.Interval = 100;
            uiTimer.Tick += uiTimer_Tick;
            // 
            // liveTimer
            // 
            liveTimer.Interval = 300;
            liveTimer.Tick += liveTimer_Tick;
            // 
            // toolTip
            // 
            toolTip.AutoPopDelay = 15000;
            toolTip.InitialDelay = 300;
            toolTip.ReshowDelay = 100;
            // 
            // ucMeasure
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 255);
            Font = new Font("Malgun Gothic", 9F);
            Name = "ucMeasure";
            Size = new Size(1400, 800);
            Controls.Add(panelCenter);
            Controls.Add(panelRight);
            Controls.Add(panelLeft);
            Controls.Add(panelTop);
            toolTip.SetToolTip(nudSettle, "전압을 바꾼 뒤 측정 전까지 기다리는 시간입니다. 증폭기와 검출기 출력이 안정되도록 충분히 주세요.");
            toolTip.SetToolTip(nudAverage, "측정점 하나에서 오실로스코프로 기록해 평균하는 시간입니다. 길수록 잡음이 줄고 측정이 느려집니다.");
            toolTip.SetToolTip(cboXSource, "설정 전압: 신호발생기 명령값 × 증폭기 이득\r\n모니터 (CH2): AMJ-1B20 전압 모니터 출력으로 측정한 실제 인가 전압");
            toolTip.SetToolTip(chkFixY0, "측정 범위가 반주기보다 좁으면 w 가 불안정합니다.\r\n편광자 회전으로 얻은 최대/최소 광신호로 y0 = (최대+최소)/2 를 고정할 수 있습니다.");
            toolTip.SetToolTip(chkFixA, "A = (최대−최소)/2 를 고정하면 반주기보다 좁은 범위에서도 w 를 안정적으로 구할 수 있습니다.");
            toolTip.SetToolTip(chkExclude, "CH1 신호가 입력 레인지 끝에 닿은 점(레인지 초과·검출기 포화 의심)을 피팅에서 뺍니다.");
            toolTip.SetToolTip(nudManualKv, "광학 정렬 등을 위해 원하는 전압을 직접 출력합니다 (측정 중에는 사용할 수 없음).");
            toolTip.SetToolTip(chkLive, "측정 중이 아닐 때 광검출기 신호를 0.3초마다 표시합니다 (편광자 정렬용).");
            toolTip.SetToolTip(btnNHelp, "굴절률 선택 도움말 (n_e / n_o, 파장별 값, 전기광학 계수별 관계식)");
            ((System.ComponentModel.ISupportInitialize)nudN).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudD).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudL).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudFixY0).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudFixA).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStart).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudEnd).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStep).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSettle).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudAverage).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudRepeats).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudWavelength).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudTemperature).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudManualKv).EndInit();
            panelCenter.ResumeLayout(false);
            panelStatus.ResumeLayout(false);
            tlpTime.ResumeLayout(false);
            tlpTime.PerformLayout();
            panelRight.ResumeLayout(false);
            panelSave.ResumeLayout(false);
            grpEo.ResumeLayout(false);
            grpEo.PerformLayout();
            tlpEo.ResumeLayout(false);
            tlpEo.PerformLayout();
            grpResult.ResumeLayout(false);
            grpResult.PerformLayout();
            tlpResult.ResumeLayout(false);
            tlpResult.PerformLayout();
            panelLeft.ResumeLayout(false);
            grpFit.ResumeLayout(false);
            grpFit.PerformLayout();
            tlpFit.ResumeLayout(false);
            tlpFit.PerformLayout();
            grpScan.ResumeLayout(false);
            grpScan.PerformLayout();
            tlpScan.ResumeLayout(false);
            tlpScan.PerformLayout();
            grpInfo.ResumeLayout(false);
            grpInfo.PerformLayout();
            tlpInfo.ResumeLayout(false);
            tlpInfo.PerformLayout();
            panelRun.ResumeLayout(false);
            panelTop.ResumeLayout(false);
            flowTop.ResumeLayout(false);
            flowTop.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelCenter;
        private ScottPlot.WinForms.FormsPlot formsPlot;
        private Panel panelStatus;
        private TextBox txtLog;
        private Label lblGap;
        private ProgressBar progressBar;
        private TableLayoutPanel tlpTime;
        private Label lblCapEstimate;
        private Label lblEstimate;
        private Label lblCapElapsed;
        private Label lblElapsed;
        private Label lblCapRemaining;
        private Label lblRemaining;
        private Label lblCapTotal;
        private Label lblTotal;
        private Panel panelRight;
        private Panel panelSave;
        private Label lblSaved;
        private FontAwesome.Sharp.IconButton btnSaveCsv;
        private FontAwesome.Sharp.IconButton btnAddHistory;
        private GroupBox grpEo;
        private Label lblR;
        private TableLayoutPanel tlpEo;
        private Label lblEoLambdaName;
        private Label lblEoLambda;
        private Label lblEoN;
        private NumericUpDown nudN;
        private Label lblEoD;
        private NumericUpDown nudD;
        private Label lblEoL;
        private NumericUpDown nudL;
        private Label lblEoVpiName;
        private Label lblEoVpi;
        private FontAwesome.Sharp.IconButton btnNHelp;
        private Label lblEoFormula;
        private GroupBox grpResult;
        private Label lblFitMsg;
        private TableLayoutPanel tlpResult;
        private Label lblHdrName;
        private Label lblHdrValue;
        private Label lblHdrErr;
        private Label lblNameY0;
        private Label lblY0;
        private Label lblY0Err;
        private Label lblNameXc;
        private Label lblXc;
        private Label lblXcErr;
        private Label lblNameW;
        private Label lblW;
        private Label lblWErr;
        private Label lblNameA;
        private Label lblA;
        private Label lblAErr;
        private Label lblNameChi;
        private Label lblChi;
        private Label lblNameR2;
        private Label lblR2;
        private Label lblNameN;
        private Label lblN;
        private Label lblVpi;
        private Panel panelLeft;
        private GroupBox grpFit;
        private TableLayoutPanel tlpFit;
        private Label lblXSource;
        private ComboBox cboXSource;
        private Label lblSelection;
        private ComboBox cboSelection;
        private CheckBox chkExclude;
        private CheckBox chkFixY0;
        private NumericUpDown nudFixY0;
        private CheckBox chkFixA;
        private NumericUpDown nudFixA;
        private FontAwesome.Sharp.IconButton btnRefit;
        private GroupBox grpScan;
        private TableLayoutPanel tlpScan;
        private Label lblStart;
        private NumericUpDown nudStart;
        private Label lblEnd;
        private NumericUpDown nudEnd;
        private Label lblStep;
        private NumericUpDown nudStep;
        private Label lblSettle;
        private NumericUpDown nudSettle;
        private Label lblAverage;
        private NumericUpDown nudAverage;
        private Label lblMode;
        private ComboBox cboMode;
        private Label lblRepeats;
        private NumericUpDown nudRepeats;
        private Label lblPlan;
        private GroupBox grpInfo;
        private TableLayoutPanel tlpInfo;
        private Label lblSample;
        private TextBox txtSample;
        private Label lblWavelength;
        private NumericUpDown nudWavelength;
        private Label lblTemperature;
        private NumericUpDown nudTemperature;
        private Label lblMemo;
        private TextBox txtMemo;
        private Panel panelRun;
        private FontAwesome.Sharp.IconButton btnStart;
        private FontAwesome.Sharp.IconButton btnStop;
        private Panel panelTop;
        private FlowLayoutPanel flowTop;
        private Label lblDevKind;
        private ComboBox cboDevice;
        private FontAwesome.Sharp.IconButton btnConnect;
        private FontAwesome.Sharp.IconButton btnDisconnect;
        private Label lblSep1;
        private Label lblManual;
        private NumericUpDown nudManualKv;
        private FontAwesome.Sharp.IconButton btnApplyOutput;
        private FontAwesome.Sharp.IconButton btnZero;
        private Label lblHv;
        private Label lblSep2;
        private CheckBox chkLive;
        private Label lblLive;
        private System.Windows.Forms.Timer uiTimer;
        private System.Windows.Forms.Timer liveTimer;
        private ToolTip toolTip;
    }
}
