namespace LnEoAnalyzer.Views
{
    partial class frmIndexHelp
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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            panelBody = new Panel();
            rtbHelp = new RichTextBox();
            panelBottom = new Panel();
            btnClose = new Button();
            panelTop = new Panel();
            dgvIndex = new DataGridView();
            lblSource = new Label();
            lblHint = new Label();
            flowApply = new FlowLayoutPanel();
            btnApplyNe = new FontAwesome.Sharp.IconButton();
            btnApplyNo = new FontAwesome.Sharp.IconButton();
            lblDn = new Label();
            lblHeader = new Label();
            panelBody.SuspendLayout();
            panelBottom.SuspendLayout();
            panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvIndex).BeginInit();
            flowApply.SuspendLayout();
            SuspendLayout();
            // 
            // panelBody
            // 
            panelBody.Controls.Add(rtbHelp);
            panelBody.BackColor = Color.White;
            panelBody.Dock = DockStyle.Fill;
            panelBody.Name = "panelBody";
            panelBody.Padding = new Padding(16, 8, 12, 8);
            // 
            // rtbHelp
            // 
            rtbHelp.BackColor = Color.White;
            rtbHelp.BorderStyle = BorderStyle.None;
            rtbHelp.Dock = DockStyle.Fill;
            rtbHelp.Name = "rtbHelp";
            rtbHelp.ReadOnly = true;
            rtbHelp.Text = "";
            // 
            // panelBottom
            // 
            panelBottom.Controls.Add(btnClose);
            panelBottom.BackColor = Color.FromArgb(245, 245, 255);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(860, 50);
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.Location = new Point(740, 8);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(108, 34);
            btnClose.Text = "닫기";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // panelTop
            // 
            panelTop.Controls.Add(dgvIndex);
            panelTop.Controls.Add(lblSource);
            panelTop.Controls.Add(lblHint);
            panelTop.Controls.Add(flowApply);
            panelTop.Controls.Add(lblHeader);
            panelTop.BackColor = Color.FromArgb(245, 245, 255);
            panelTop.Dock = DockStyle.Top;
            panelTop.Name = "panelTop";
            panelTop.Padding = new Padding(12, 8, 12, 6);
            panelTop.Size = new Size(860, 218);
            // 
            // dgvIndex
            // 
            dgvIndex.AllowUserToAddRows = false;
            dgvIndex.AllowUserToDeleteRows = false;
            dgvIndex.AllowUserToResizeRows = false;
            dgvIndex.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvIndex.BackgroundColor = Color.White;
            dgvIndex.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvIndex.Dock = DockStyle.Fill;
            dgvIndex.Name = "dgvIndex";
            dgvIndex.ReadOnly = true;
            dgvIndex.RowHeadersVisible = false;
            dgvIndex.ScrollBars = ScrollBars.None;
            dgvIndex.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            // 
            // lblSource
            // 
            lblSource.Dock = DockStyle.Bottom;
            lblSource.Font = new Font("Malgun Gothic", 8.5F);
            lblSource.ForeColor = Color.DimGray;
            lblSource.Name = "lblSource";
            lblSource.Size = new Size(836, 22);
            lblSource.Text = "셀마이어 식 계산값 · Zelmon et al., J. Opt. Soc. Am. B 14, 3319 (1997), congruent LiNbO3, 21 ℃";
            lblSource.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblHint
            // 
            lblHint.Dock = DockStyle.Top;
            lblHint.ForeColor = Color.FromArgb(180, 90, 0);
            lblHint.Name = "lblHint";
            lblHint.Size = new Size(836, 22);
            lblHint.Text = "편광이 결정의 z축(광학축)과 나란하면 n_e, 수직이면 n_o 를 넣으세요. 편광자 45° 세기 변조(전계 z방향)는 n_e → 결과는 유효 계수 r_c.";
            lblHint.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // flowApply
            // 
            flowApply.Controls.Add(btnApplyNe);
            flowApply.Controls.Add(btnApplyNo);
            flowApply.Controls.Add(lblDn);
            flowApply.Dock = DockStyle.Top;
            flowApply.Name = "flowApply";
            flowApply.Size = new Size(836, 44);
            flowApply.WrapContents = false;
            // 
            // btnApplyNe
            // 
            btnApplyNe.BackColor = Color.FromArgb(46, 160, 67);
            btnApplyNe.Cursor = Cursors.Hand;
            btnApplyNe.FlatStyle = FlatStyle.Flat;
            btnApplyNe.Font = new Font("Malgun Gothic", 9.5F, FontStyle.Bold);
            btnApplyNe.ForeColor = Color.White;
            btnApplyNe.IconChar = FontAwesome.Sharp.IconChar.Check;
            btnApplyNe.IconColor = Color.White;
            btnApplyNe.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnApplyNe.IconSize = 18;
            btnApplyNe.Name = "btnApplyNe";
            btnApplyNe.Size = new Size(210, 36);
            btnApplyNe.Text = "n_e = 2.1451 적용";
            btnApplyNe.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnApplyNe.UseVisualStyleBackColor = false;
            btnApplyNe.Margin = new Padding(0, 3, 8, 3);
            btnApplyNe.FlatAppearance.BorderSize = 0;
            btnApplyNe.Click += btnApplyNe_Click;
            // 
            // btnApplyNo
            // 
            btnApplyNo.BackColor = Color.FromArgb(98, 102, 242);
            btnApplyNo.Cursor = Cursors.Hand;
            btnApplyNo.FlatStyle = FlatStyle.Flat;
            btnApplyNo.Font = new Font("Malgun Gothic", 9.5F, FontStyle.Bold);
            btnApplyNo.ForeColor = Color.White;
            btnApplyNo.IconChar = FontAwesome.Sharp.IconChar.Check;
            btnApplyNo.IconColor = Color.White;
            btnApplyNo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnApplyNo.IconSize = 18;
            btnApplyNo.Name = "btnApplyNo";
            btnApplyNo.Size = new Size(210, 36);
            btnApplyNo.Text = "n_o = 2.2200 적용";
            btnApplyNo.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnApplyNo.UseVisualStyleBackColor = false;
            btnApplyNo.Margin = new Padding(0, 3, 8, 3);
            btnApplyNo.FlatAppearance.BorderSize = 0;
            btnApplyNo.Click += btnApplyNo_Click;
            // 
            // lblDn
            // 
            lblDn.AutoSize = true;
            lblDn.ForeColor = Color.DimGray;
            lblDn.Margin = new Padding(8, 13, 0, 0);
            lblDn.Name = "lblDn";
            lblDn.Text = "Δn = n_e − n_o = −0.0748";
            // 
            // lblHeader
            // 
            lblHeader.Dock = DockStyle.Top;
            lblHeader.Font = new Font("Malgun Gothic", 11F, FontStyle.Bold);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(836, 30);
            lblHeader.Text = "현재 파장 λ = 1310 nm 에서의 LiNbO3 굴절률";
            lblHeader.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // frmIndexHelp
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            CancelButton = btnClose;
            ClientSize = new Size(860, 700);
            Font = new Font("Malgun Gothic", 9F);
            MinimizeBox = false;
            MinimumSize = new Size(640, 480);
            Name = "frmIndexHelp";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "굴절률(n) 선택 도움말 — LiNbO3";
            Controls.Add(panelBody);
            Controls.Add(panelBottom);
            Controls.Add(panelTop);
            ((System.ComponentModel.ISupportInitialize)dgvIndex).EndInit();
            panelBody.ResumeLayout(false);
            panelBottom.ResumeLayout(false);
            panelTop.ResumeLayout(false);
            flowApply.ResumeLayout(false);
            flowApply.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelBody;
        private RichTextBox rtbHelp;
        private Panel panelBottom;
        private Button btnClose;
        private Panel panelTop;
        private DataGridView dgvIndex;
        private Label lblSource;
        private Label lblHint;
        private FlowLayoutPanel flowApply;
        private FontAwesome.Sharp.IconButton btnApplyNe;
        private FontAwesome.Sharp.IconButton btnApplyNo;
        private Label lblDn;
        private Label lblHeader;
    }
}
