using LnEoAnalyzer.Core;

namespace LnEoAnalyzer.Views
{
    /// <summary>
    /// 전기광학 계수 계산용 굴절률(n_e / n_o) 선택 도움말.
    /// 현재 파장의 n_e, n_o 를 셀마이어 식으로 계산해 보여주고, [적용] 을 누르면 그 값을 굴절률 칸에 넣습니다.
    /// 본문은 IndexHelpContent.Markup 에서 수정할 수 있습니다.
    /// </summary>
    public partial class frmIndexHelp : Form
    {
        private readonly double _nm, _ne, _no;

        /// <summary>[적용] 으로 선택한 굴절률 (닫기만 하면 null)</summary>
        public double? SelectedIndex { get; private set; }
        /// <summary>"n_e" 또는 "n_o"</summary>
        public string SelectedName { get; private set; } = "";

        public frmIndexHelp() : this(1310) { }

        public frmIndexHelp(double wavelengthNm)
        {
            InitializeComponent();
            _nm = wavelengthNm;
            _ne = LnRefractiveIndex.Ne(wavelengthNm);
            _no = LnRefractiveIndex.No(wavelengthNm);
            FillTop();
            FillTable();
            try { rtbHelp.Rtf = IndexHelpContent.ToRtf(IndexHelpContent.Markup); }
            catch { rtbHelp.Text = IndexHelpContent.Markup; }
        }

        private void FillTop()
        {
            string nm = _nm.ToString("0.#", Num.Inv);
            if (Num.IsFinite(_ne) && Num.IsFinite(_no))
            {
                lblHeader.Text = $"현재 파장 λ = {nm} nm 에서의 LiNbO3 굴절률";
                btnApplyNe.Text = string.Format(Num.Inv, "n_e = {0:0.0000} 적용", _ne);
                btnApplyNo.Text = string.Format(Num.Inv, "n_o = {0:0.0000} 적용", _no);
                lblDn.Text = string.Format(Num.Inv, "Δn = n_e − n_o = {0:0.0000}", _ne - _no);
            }
            else
            {
                lblHeader.Text = $"파장 {nm} nm 는 셀마이어 식 적용 범위(400 ~ 5000 nm) 밖입니다.";
                btnApplyNe.Enabled = btnApplyNo.Enabled = false;
                btnApplyNe.Text = "n_e 적용";
                btnApplyNo.Text = "n_o 적용";
                lblDn.Text = "";
            }
        }

        private void FillTable()
        {
            dgvIndex.Columns.Clear();
            foreach (var h in new[] { "파장 (nm)", "n_e  (이상광 · 편광이 z축과 나란)", "n_o  (정상광 · 편광이 z축에 수직)", "Δn = n_e − n_o" })
                dgvIndex.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = h, SortMode = DataGridViewColumnSortMode.NotSortable });
            dgvIndex.Columns[0].FillWeight = 60;

            var list = new List<double> { 1310, 1550 };
            if (LnRefractiveIndex.InRange(_nm) && !list.Any(v => Math.Abs(v - _nm) < 0.05)) list.Add(_nm);
            list.Sort();
            foreach (var w in list)
            {
                double no = LnRefractiveIndex.No(w), ne = LnRefractiveIndex.Ne(w);
                int r = dgvIndex.Rows.Add(
                    w.ToString("0.#", Num.Inv) + (Math.Abs(w - _nm) < 0.05 ? "  (현재)" : ""),
                    ne.ToString("0.0000", Num.Inv),
                    no.ToString("0.0000", Num.Inv),
                    (ne - no).ToString("0.0000", Num.Inv));
                if (Math.Abs(w - _nm) < 0.05)
                {
                    var st = dgvIndex.Rows[r].DefaultCellStyle;
                    st.Font = new Font(dgvIndex.Font, FontStyle.Bold);
                    st.BackColor = Color.FromArgb(232, 234, 255);
                }
            }
            dgvIndex.ClearSelection();
        }

        private void Apply(double n, string name)
        {
            SelectedIndex = Math.Round(n, 4);
            SelectedName = name;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnApplyNe_Click(object? sender, EventArgs e) => Apply(_ne, "n_e");
        private void btnApplyNo_Click(object? sender, EventArgs e) => Apply(_no, "n_o");
        private void btnClose_Click(object? sender, EventArgs e) => Close();
    }
}
