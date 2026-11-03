namespace LnEoAnalyzer
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelMenu = new Panel();
            btnSettingsView = new FontAwesome.Sharp.IconButton();
            btnResultsView = new FontAwesome.Sharp.IconButton();
            btnMeasureView = new FontAwesome.Sharp.IconButton();
            btnExit = new FontAwesome.Sharp.IconButton();
            panelLogo = new Panel();
            btnMenu = new FontAwesome.Sharp.IconButton();
            lblLogo = new Label();
            panelTitleBar = new Panel();
            lblDevice = new Label();
            lblTitle = new Label();
            btnMinimize = new FontAwesome.Sharp.IconButton();
            btnMaximize = new FontAwesome.Sharp.IconButton();
            btnClose = new FontAwesome.Sharp.IconButton();
            panelDesktop = new Panel();
            panelMenu.SuspendLayout();
            panelLogo.SuspendLayout();
            panelTitleBar.SuspendLayout();
            SuspendLayout();
            //
            // panelMenu
            //
            panelMenu.BackColor = Color.FromArgb(98, 102, 242);
            panelMenu.Controls.Add(btnSettingsView);
            panelMenu.Controls.Add(btnResultsView);
            panelMenu.Controls.Add(btnMeasureView);
            panelMenu.Controls.Add(btnExit);
            panelMenu.Controls.Add(panelLogo);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(2, 2);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(190, 796);
            panelMenu.TabIndex = 0;
            //
            // btnSettingsView
            //
            btnSettingsView.Dock = DockStyle.Top;
            btnSettingsView.FlatAppearance.BorderSize = 0;
            btnSettingsView.FlatStyle = FlatStyle.Flat;
            btnSettingsView.Font = new Font("Malgun Gothic", 11F, FontStyle.Bold);
            btnSettingsView.ForeColor = Color.White;
            btnSettingsView.IconChar = FontAwesome.Sharp.IconChar.Gear;
            btnSettingsView.IconColor = Color.White;
            btnSettingsView.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnSettingsView.IconSize = 28;
            btnSettingsView.ImageAlign = ContentAlignment.MiddleLeft;
            btnSettingsView.Location = new Point(0, 172);
            btnSettingsView.Name = "btnSettingsView";
            btnSettingsView.Padding = new Padding(10, 0, 0, 0);
            btnSettingsView.Size = new Size(190, 52);
            btnSettingsView.TabIndex = 3;
            btnSettingsView.Tag = "장비 설정";
            btnSettingsView.Text = "  장비 설정";
            btnSettingsView.TextAlign = ContentAlignment.MiddleLeft;
            btnSettingsView.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSettingsView.UseVisualStyleBackColor = true;
            btnSettingsView.Click += btnSettingsView_Click;
            //
            // btnResultsView
            //
            btnResultsView.Dock = DockStyle.Top;
            btnResultsView.FlatAppearance.BorderSize = 0;
            btnResultsView.FlatStyle = FlatStyle.Flat;
            btnResultsView.Font = new Font("Malgun Gothic", 11F, FontStyle.Bold);
            btnResultsView.ForeColor = Color.White;
            btnResultsView.IconChar = FontAwesome.Sharp.IconChar.Table;
            btnResultsView.IconColor = Color.White;
            btnResultsView.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnResultsView.IconSize = 28;
            btnResultsView.ImageAlign = ContentAlignment.MiddleLeft;
            btnResultsView.Location = new Point(0, 120);
            btnResultsView.Name = "btnResultsView";
            btnResultsView.Padding = new Padding(10, 0, 0, 0);
            btnResultsView.Size = new Size(190, 52);
            btnResultsView.TabIndex = 2;
            btnResultsView.Tag = "결과 기록";
            btnResultsView.Text = "  결과 기록";
            btnResultsView.TextAlign = ContentAlignment.MiddleLeft;
            btnResultsView.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnResultsView.UseVisualStyleBackColor = true;
            btnResultsView.Click += btnResultsView_Click;
            //
            // btnMeasureView
            //
            btnMeasureView.Dock = DockStyle.Top;
            btnMeasureView.FlatAppearance.BorderSize = 0;
            btnMeasureView.FlatStyle = FlatStyle.Flat;
            btnMeasureView.Font = new Font("Malgun Gothic", 11F, FontStyle.Bold);
            btnMeasureView.ForeColor = Color.White;
            btnMeasureView.IconChar = FontAwesome.Sharp.IconChar.ChartLine;
            btnMeasureView.IconColor = Color.White;
            btnMeasureView.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnMeasureView.IconSize = 28;
            btnMeasureView.ImageAlign = ContentAlignment.MiddleLeft;
            btnMeasureView.Location = new Point(0, 68);
            btnMeasureView.Name = "btnMeasureView";
            btnMeasureView.Padding = new Padding(10, 0, 0, 0);
            btnMeasureView.Size = new Size(190, 52);
            btnMeasureView.TabIndex = 1;
            btnMeasureView.Tag = "반파장 전압 측정";
            btnMeasureView.Text = "  반파장 전압 측정";
            btnMeasureView.TextAlign = ContentAlignment.MiddleLeft;
            btnMeasureView.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnMeasureView.UseVisualStyleBackColor = true;
            btnMeasureView.Click += btnMeasureView_Click;
            //
            // btnExit
            //
            btnExit.Dock = DockStyle.Bottom;
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Font = new Font("Malgun Gothic", 11F, FontStyle.Bold);
            btnExit.ForeColor = Color.White;
            btnExit.IconChar = FontAwesome.Sharp.IconChar.SignOutAlt;
            btnExit.IconColor = Color.White;
            btnExit.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnExit.IconSize = 28;
            btnExit.ImageAlign = ContentAlignment.MiddleLeft;
            btnExit.Location = new Point(0, 744);
            btnExit.Name = "btnExit";
            btnExit.Padding = new Padding(10, 0, 0, 0);
            btnExit.Size = new Size(190, 52);
            btnExit.TabIndex = 4;
            btnExit.Tag = "종료";
            btnExit.Text = "  종료";
            btnExit.TextAlign = ContentAlignment.MiddleLeft;
            btnExit.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            //
            // panelLogo
            //
            panelLogo.Controls.Add(btnMenu);
            panelLogo.Controls.Add(lblLogo);
            panelLogo.Dock = DockStyle.Top;
            panelLogo.Location = new Point(0, 0);
            panelLogo.Name = "panelLogo";
            panelLogo.Size = new Size(190, 68);
            panelLogo.TabIndex = 0;
            //
            // btnMenu
            //
            btnMenu.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMenu.FlatAppearance.BorderSize = 0;
            btnMenu.FlatStyle = FlatStyle.Flat;
            btnMenu.IconChar = FontAwesome.Sharp.IconChar.Bars;
            btnMenu.IconColor = Color.White;
            btnMenu.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnMenu.IconSize = 30;
            btnMenu.Location = new Point(146, 14);
            btnMenu.Name = "btnMenu";
            btnMenu.Size = new Size(40, 40);
            btnMenu.TabIndex = 1;
            btnMenu.UseVisualStyleBackColor = true;
            btnMenu.Click += btnMenu_Click;
            //
            // lblLogo
            //
            lblLogo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblLogo.ForeColor = Color.White;
            lblLogo.Location = new Point(8, 8);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(136, 52);
            lblLogo.TabIndex = 0;
            lblLogo.Text = "LiNbO3\r\nEO Analyzer";
            lblLogo.TextAlign = ContentAlignment.MiddleLeft;
            //
            // panelTitleBar
            //
            panelTitleBar.BackColor = Color.White;
            panelTitleBar.Controls.Add(lblDevice);
            panelTitleBar.Controls.Add(lblTitle);
            panelTitleBar.Controls.Add(btnMinimize);
            panelTitleBar.Controls.Add(btnMaximize);
            panelTitleBar.Controls.Add(btnClose);
            panelTitleBar.Dock = DockStyle.Top;
            panelTitleBar.Location = new Point(192, 2);
            panelTitleBar.Name = "panelTitleBar";
            panelTitleBar.Size = new Size(1406, 32);
            panelTitleBar.TabIndex = 1;
            panelTitleBar.MouseDoubleClick += panelTitleBar_MouseDoubleClick;
            panelTitleBar.MouseDown += panelTitleBar_MouseDown;
            //
            // lblDevice
            //
            lblDevice.Dock = DockStyle.Fill;
            lblDevice.Font = new Font("Malgun Gothic", 9.5F, FontStyle.Bold);
            lblDevice.ForeColor = Color.DimGray;
            lblDevice.Location = new Point(420, 0);
            lblDevice.Name = "lblDevice";
            lblDevice.Padding = new Padding(10, 0, 0, 0);
            lblDevice.Size = new Size(896, 32);
            lblDevice.TabIndex = 1;
            lblDevice.Text = "장비 연결 안 됨";
            lblDevice.TextAlign = ContentAlignment.MiddleLeft;
            lblDevice.MouseDown += panelTitleBar_MouseDown;
            //
            // lblTitle
            //
            lblTitle.Dock = DockStyle.Left;
            lblTitle.Font = new Font("Malgun Gothic", 10.5F, FontStyle.Bold);
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Padding = new Padding(8, 0, 0, 0);
            lblTitle.Size = new Size(420, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "LiNbO3 전기-광학 특성 분석 (반파장 전압 · 전기-광학 계수)";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            lblTitle.MouseDown += panelTitleBar_MouseDown;
            //
            // btnMinimize
            //
            btnMinimize.BackColor = Color.DarkTurquoise;
            btnMinimize.Dock = DockStyle.Right;
            btnMinimize.FlatAppearance.BorderSize = 0;
            btnMinimize.FlatStyle = FlatStyle.Flat;
            btnMinimize.IconChar = FontAwesome.Sharp.IconChar.WindowMinimize;
            btnMinimize.IconColor = Color.White;
            btnMinimize.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnMinimize.IconSize = 18;
            btnMinimize.Location = new Point(1316, 0);
            btnMinimize.Name = "btnMinimize";
            btnMinimize.Size = new Size(30, 32);
            btnMinimize.TabIndex = 2;
            btnMinimize.UseVisualStyleBackColor = false;
            btnMinimize.Click += btnMinimize_Click;
            //
            // btnMaximize
            //
            btnMaximize.BackColor = Color.RoyalBlue;
            btnMaximize.Dock = DockStyle.Right;
            btnMaximize.FlatAppearance.BorderSize = 0;
            btnMaximize.FlatStyle = FlatStyle.Flat;
            btnMaximize.IconChar = FontAwesome.Sharp.IconChar.Square;
            btnMaximize.IconColor = Color.White;
            btnMaximize.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnMaximize.IconSize = 18;
            btnMaximize.Location = new Point(1346, 0);
            btnMaximize.Name = "btnMaximize";
            btnMaximize.Size = new Size(30, 32);
            btnMaximize.TabIndex = 3;
            btnMaximize.UseVisualStyleBackColor = false;
            btnMaximize.Click += btnMaximize_Click;
            //
            // btnClose
            //
            btnClose.BackColor = Color.FromArgb(255, 74, 130);
            btnClose.Dock = DockStyle.Right;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.IconChar = FontAwesome.Sharp.IconChar.Xmark;
            btnClose.IconColor = Color.White;
            btnClose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnClose.IconSize = 20;
            btnClose.Location = new Point(1376, 0);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(30, 32);
            btnClose.TabIndex = 4;
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            //
            // panelDesktop
            //
            panelDesktop.BackColor = Color.FromArgb(245, 245, 255);
            panelDesktop.Dock = DockStyle.Fill;
            panelDesktop.Location = new Point(192, 34);
            panelDesktop.Name = "panelDesktop";
            panelDesktop.Size = new Size(1406, 764);
            panelDesktop.TabIndex = 2;
            //
            // MainForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(98, 102, 244);
            ClientSize = new Size(1600, 800);
            Controls.Add(panelDesktop);
            Controls.Add(panelTitleBar);
            Controls.Add(panelMenu);
            FormBorderStyle = FormBorderStyle.None;
            MinimumSize = new Size(1200, 700);
            Name = "MainForm";
            Padding = new Padding(2);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LN EO Analyzer";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            SizeChanged += MainForm_SizeChanged;
            panelMenu.ResumeLayout(false);
            panelLogo.ResumeLayout(false);
            panelTitleBar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMenu;
        private Panel panelLogo;
        private Label lblLogo;
        private FontAwesome.Sharp.IconButton btnMenu;
        private FontAwesome.Sharp.IconButton btnMeasureView;
        private FontAwesome.Sharp.IconButton btnResultsView;
        private FontAwesome.Sharp.IconButton btnSettingsView;
        private FontAwesome.Sharp.IconButton btnExit;
        private Panel panelTitleBar;
        private Label lblTitle;
        private Label lblDevice;
        private FontAwesome.Sharp.IconButton btnMinimize;
        private FontAwesome.Sharp.IconButton btnMaximize;
        private FontAwesome.Sharp.IconButton btnClose;
        private Panel panelDesktop;
    }
}
