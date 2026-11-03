using System.Diagnostics;
using LnEoAnalyzer.Core;

namespace LnEoAnalyzer.Views
{
    /// <summary>결과 기록 화면: 측정 조건별 피팅 결과 + 전기-광학 계수 (PPT 4번 슬라이드 표와 같은 항목)</summary>
    public partial class ucResults : UserControl
    {
        private AppState? _state;

        private static readonly string[] Headers =
        {
            "측정 시각", "시료", "파장 (nm)", "온도 (℃)", "메모",
            "y0", "y0 오차", "xc (kV)", "xc 오차", "w = Vπ (kV)", "w 오차", "A", "A 오차",
            "Reduced χ²", "Adj. R²", "N", "n", "d (mm)", "L (mm)", "r (pm/V)", "r 오차",
            "소요 (s)", "비고", "파일",
        };

        public ucResults()
        {
            InitializeComponent();
            foreach (var h in Headers)
            {
                var col = new DataGridViewTextBoxColumn { HeaderText = h, Name = "col" + dgvResults.Columns.Count, SortMode = DataGridViewColumnSortMode.NotSortable };
                dgvResults.Columns.Add(col);
            }
            // w(Vπ) 와 r 열 강조
            dgvResults.Columns[9].DefaultCellStyle.Font = new Font("Malgun Gothic", 9F, FontStyle.Bold);
            dgvResults.Columns[9].DefaultCellStyle.ForeColor = Color.FromArgb(200, 30, 30);
            dgvResults.Columns[19].DefaultCellStyle.Font = new Font("Malgun Gothic", 9F, FontStyle.Bold);
            dgvResults.Columns[19].DefaultCellStyle.ForeColor = Color.FromArgb(30, 60, 180);
        }

        public void Bind(AppState state)
        {
            _state = state;
            _state.HistoryChanged += (s, e) => RefreshGrid();
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            if (_state == null) return;
            dgvResults.SuspendLayout();
            dgvResults.Rows.Clear();
            foreach (var r in _state.History)
                dgvResults.Rows.Add(CsvExport.HistoryRow(r).Cast<object>().ToArray());
            dgvResults.ResumeLayout();
            if (dgvResults.Rows.Count > 0)
                dgvResults.FirstDisplayedScrollingRowIndex = dgvResults.Rows.Count - 1;
        }

        private void btnExport_Click(object? sender, EventArgs e)
        {
            if (_state == null || _state.History.Count == 0)
            {
                MessageBox.Show("내보낼 결과가 없습니다.", "결과 기록", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var folder = _state.Settings.General.DataFolder;
            using var dlg = new SaveFileDialog
            {
                Filter = "CSV 파일 (*.csv)|*.csv",
                FileName = $"LN_결과표_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
                InitialDirectory = Directory.Exists(folder) ? folder : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            };
            if (dlg.ShowDialog(this) != DialogResult.OK) return;
            try
            {
                CsvExport.WriteHistory(dlg.FileName, _state.History);
                MessageBox.Show("저장했습니다.\n" + dlg.FileName, "결과 기록", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "저장 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object? sender, EventArgs e)
        {
            if (_state == null || dgvResults.SelectedRows.Count == 0) return;
            var idx = dgvResults.SelectedRows.Cast<DataGridViewRow>().Select(r => r.Index).OrderByDescending(i => i).ToList();
            foreach (var i in idx)
                if (i >= 0 && i < _state.History.Count) _state.History.RemoveAt(i);
            _state.RaiseHistoryChanged();
            _state.SaveHistory();
        }

        private void btnClear_Click(object? sender, EventArgs e)
        {
            if (_state == null || _state.History.Count == 0) return;
            var r = MessageBox.Show("결과표를 모두 지울까요? (저장된 측정 CSV 파일은 지워지지 않습니다)",
                "결과 기록", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (r != DialogResult.Yes) return;
            _state.History.Clear();
            _state.RaiseHistoryChanged();
            _state.SaveHistory();
        }

        private void btnOpenFolder_Click(object? sender, EventArgs e)
        {
            if (_state == null) return;
            var folder = _state.Settings.General.DataFolder;
            try
            {
                Directory.CreateDirectory(folder);
                Process.Start(new ProcessStartInfo { FileName = folder, UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "폴더 열기", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
