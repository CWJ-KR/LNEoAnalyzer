namespace LnEoAnalyzer;

internal static class Program
{
    /// <summary>
    /// LN 전기-광학 특성 분석 프로그램 (LnEoAnalyzer)
    /// 실행 인수 --demo : 데모(가상 장비) 모드로 시작
    /// </summary>
    [STAThread]
    private static void Main(string[] args)
    {
        ApplicationConfiguration.Initialize();
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += (s, e) =>
            MessageBox.Show(e.Exception.Message + Environment.NewLine + Environment.NewLine + e.Exception.GetType().Name,
                "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);

        bool demo = args.Any(a => string.Equals(a, "--demo", StringComparison.OrdinalIgnoreCase));
        Application.Run(new MainForm(demo));
    }
}
