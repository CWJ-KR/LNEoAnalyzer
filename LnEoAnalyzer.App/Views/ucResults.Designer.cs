namespace LnEoAnalyzer.Views
{
    partial class ucResults
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
            panelGrid = new Panel();
            dgvResults = new DataGridView();
            lblResHint = new Label();
            panelResTop = new Panel();
            lblResTitle = new Label();
            btnExport = new FontAwesome.Sharp.IconButton();
            btnDelete = new FontAwesome.Sharp.IconButton();
            btnClear = new FontAwesome.Sharp.IconButton();
            btnOpenFolder = new FontAwesome.Sharp.IconButton();
            panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResults).BeginInit();
            panelResTop.SuspendLayout();
            SuspendLayout();
            // 
            // panelGrid
            // 
            panelGrid.Controls.Add(dgvResults);
            panelGrid.Dock = DockStyle.Fill;
            panelGrid.Name = "panelGrid";
            panelGrid.Padding = new Padding(8);
            // 
            // dgvResults
            // 
            dgvResults.AllowUserToAddRows = false;
            dgvResults.AllowUserToDeleteRows = false;
            dgvResults.AllowUserToResizeRows = false;
            dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvResults.BackgroundColor = Color.White;
            dgvResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvResults.Dock = DockStyle.Fill;
            dgvResults.Name = "dgvResults";
            dgvResults.ReadOnly = true;
            dgvResults.RowHeadersVisible = false;
            dgvResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            // 
            // lblResHint
            // 
            lblResHint.Dock = DockStyle.Bottom;
            lblResHint.ForeColor = Color.DimGray;
            lblResHint.Name = "lblResHint";
            lblResHint.Padding = new Padding(8, 0, 0, 0);
            lblResHint.Size = new Size(1400, 28);
            lblResHint.Text = "측정이 끝나면 피팅 결과가 자동으로 추가됩니다 (장비 설정 화면에서 끌 수 있음). 표는 프로그램을 종료해도 유지됩니다.";
            lblResHint.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelResTop
            // 
            panelResTop.Controls.Add(lblResTitle);
            panelResTop.Controls.Add(btnExport);
            panelResTop.Controls.Add(btnDelete);
            panelResTop.Controls.Add(btnClear);
            panelResTop.Controls.Add(btnOpenFolder);
            panelResTop.BackColor = Color.White;
            panelResTop.Dock = DockStyle.Top;
            panelResTop.Name = "panelResTop";
            panelResTop.Size = new Size(1400, 52);
            // 
            // lblResTitle
            // 
            lblResTitle.AutoSize = true;
            lblResTitle.Font = new Font("Malgun Gothic", 12F, FontStyle.Bold);
            lblResTitle.Location = new Point(12, 14);
            lblResTitle.Name = "lblResTitle";
            lblResTitle.Text = "결과 기록";
            // 
            // btnExport
            // 
            btnExport.BackColor = Color.FromArgb(98, 102, 242);
            btnExport.Cursor = Cursors.Hand;
            btnExport.FlatStyle = FlatStyle.Flat;
            btnExport.Font = new Font("Malgun Gothic", 9.5F, FontStyle.Bold);
            btnExport.ForeColor = Color.White;
            btnExport.IconChar = FontAwesome.Sharp.IconChar.FileCsv;
            btnExport.IconColor = Color.White;
            btnExport.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnExport.IconSize = 18;
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(150, 36);
            btnExport.Text = "CSV로 내보내기";
            btnExport.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnExport.UseVisualStyleBackColor = false;
            btnExport.Location = new Point(130, 8);
            btnExport.FlatAppearance.BorderSize = 0;
            btnExport.Click += btnExport_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(108, 117, 125);
            btnDelete.Cursor = Cursors.Hand;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Malgun Gothic", 9.5F, FontStyle.Bold);
            btnDelete.ForeColor = Color.White;
            btnDelete.IconChar = FontAwesome.Sharp.IconChar.TrashCan;
            btnDelete.IconColor = Color.White;
            btnDelete.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnDelete.IconSize = 18;
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(120, 36);
            btnDelete.Text = "선택 삭제";
            btnDelete.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Location = new Point(286, 8);
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.FromArgb(108, 117, 125);
            btnClear.Cursor = Cursors.Hand;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Malgun Gothic", 9.5F, FontStyle.Bold);
            btnClear.ForeColor = Color.White;
            btnClear.IconChar = FontAwesome.Sharp.IconChar.Eraser;
            btnClear.IconColor = Color.White;
            btnClear.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnClear.IconSize = 18;
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(120, 36);
            btnClear.Text = "모두 지우기";
            btnClear.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Location = new Point(412, 8);
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.Click += btnClear_Click;
            // 
            // btnOpenFolder
            // 
            btnOpenFolder.BackColor = Color.FromArgb(98, 102, 242);
            btnOpenFolder.Cursor = Cursors.Hand;
            btnOpenFolder.FlatStyle = FlatStyle.Flat;
            btnOpenFolder.Font = new Font("Malgun Gothic", 9.5F, FontStyle.Bold);
            btnOpenFolder.ForeColor = Color.White;
            btnOpenFolder.IconChar = FontAwesome.Sharp.IconChar.FolderOpen;
            btnOpenFolder.IconColor = Color.White;
            btnOpenFolder.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnOpenFolder.IconSize = 18;
            btnOpenFolder.Name = "btnOpenFolder";
            btnOpenFolder.Size = new Size(160, 36);
            btnOpenFolder.Text = "데이터 폴더 열기";
            btnOpenFolder.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnOpenFolder.UseVisualStyleBackColor = false;
            btnOpenFolder.Location = new Point(538, 8);
            btnOpenFolder.FlatAppearance.BorderSize = 0;
            btnOpenFolder.Click += btnOpenFolder_Click;
            // 
            // ucResults
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 255);
            Font = new Font("Malgun Gothic", 9F);
            Name = "ucResults";
            Size = new Size(1400, 800);
            Controls.Add(panelGrid);
            Controls.Add(lblResHint);
            Controls.Add(panelResTop);
            ((System.ComponentModel.ISupportInitialize)dgvResults).EndInit();
            panelGrid.ResumeLayout(false);
            panelResTop.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelGrid;
        private DataGridView dgvResults;
        private Label lblResHint;
        private Panel panelResTop;
        private Label lblResTitle;
        private FontAwesome.Sharp.IconButton btnExport;
        private FontAwesome.Sharp.IconButton btnDelete;
        private FontAwesome.Sharp.IconButton btnClear;
        private FontAwesome.Sharp.IconButton btnOpenFolder;
    }
}
