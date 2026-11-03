// LnEoAnalyzer 자체 검증 프로그램
//  1) 수동 측정 엑셀 데이터(LN 반파장 전압 측정_20260909.xlsx)를 피팅해 PPT 4번 슬라이드의 Origin 결과와 비교
//  2) 데모(가상) 장비로 -1 ~ +1 kV 스캔 → 소요 시간 / 피팅 결과 확인
//  3) 스캔 계획(왕복/반복), 출력 제한, 전기-광학 계수 계산 확인
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using LnEoAnalyzer.Core;

int fails = 0;
void Check(bool ok, string what)
{
    Console.WriteLine((ok ? "  [PASS] " : "  [FAIL] ") + what);
    if (!ok) fails++;
}

var kv = Enumerable.Range(0, 21).Select(i => Math.Round(-1.0 + 0.1 * i, 10)).ToArray();

// 엑셀 수동 측정 데이터
var data = new (string name, double[] y)[]
{
    ("1310 nm @25℃", new double[] { 2.0839,2.1747,2.2544,2.3082,2.3403,2.3659,2.3624,2.3323,2.2843,2.2169,2.1382,2.0534,1.9436,1.8391,1.7225,1.5803,1.4729,1.3451,1.2045,1.0793,0.9701 }),
    ("1310 nm @28℃", new double[] { 2.0521,2.1558,2.2369,2.3112,2.3614,2.3832,2.3865,2.3662,2.3337,2.2748,2.1985,2.1094,2.0179,1.9096,1.7931,1.6634,1.5485,1.4245,1.3093,1.1796,1.0634 }),
    ("1310 nm @31℃", new double[] { 2.3363,2.2341,2.1305,2.0059,1.8721,1.7178,1.5702,1.4017,1.2625,1.1198,0.9791,0.8603,0.7656,0.6681,0.606,0.546,0.5203,0.4971,0.4889,0.5027,0.5265 }),
    ("1310 nm @34℃", new double[] { 0.9137,0.7732,0.7317,0.7202,0.7468,0.7912,0.8689,0.9695,1.1014,1.152,1.4124,1.606,1.7725,1.9246,2.0627,2.1987,2.3283,2.4102,2.5145,2.6026,2.6682 }),
    ("1310 nm @37℃", new double[] { 2.6589,2.7166,2.7104,2.6715,2.5847,2.4933,2.3541,2.2111,2.0572,1.8934,1.7276,1.5375,1.3475,1.1677,0.9996,0.8509,0.7232,0.6102,0.541,0.4942,0.4653 }),
    ("1550 nm @25℃", new double[] { 1.2697,1.1296,1.0095,0.9074,0.8122,0.7262,0.6541,0.5871,0.5306,0.4874,0.4538,0.4272,0.4246,0.4325,0.4586,0.479,0.5228,0.5852,0.6533,0.7458,0.8392 }),
    ("1550 nm @28℃", new double[] { 0.4909,0.4466,0.4235,0.4143,0.4243,0.4481,0.4778,0.515,0.5824,0.6463,0.7146,0.8122,0.9191,1.012,1.1066,1.2324,1.3434,1.4673,1.5575,1.6511,1.7241 }),
    ("1550 nm @31℃", new double[] { 1.3916,1.2083,1.0683,0.9342,0.8233,0.7209,0.6271,0.5511,0.4849,0.442,0.4086,0.398,0.3966,0.4084,0.4321,0.4735,0.5282,0.5958,0.684,0.7721,0.8713 }),
    ("1550 nm @34℃", new double[] { 2.4654,2.4643,2.4225,2.3501,2.2622,2.1609,2.0349,1.9011,1.7644,1.6233,1.4797,1.331,1.192,1.0561,0.9226,0.8132,0.7119,0.6269,0.5468,0.4801,0.4438 }),
    ("1550 nm @37℃", new double[] { 2.5381,2.5215,2.4703,2.3955,2.3126,2.2092,2.091,1.9765,1.8486,1.7042,1.5567,1.411,1.2483,1.102,0.9727,0.8447,0.7289,0.6314,0.5486,0.4786,0.4263 }),
};
// PPT 4번 슬라이드 Origin 결과: y0, xc, w, A, w 표준오차, Reduced Chi-Sqr
var origin = new double[][]
{
    new[] { 1.49137, 2.66175, 2.08675, 0.86449, 0.03721, 8.07294E-5 },
    new[] { 1.5856 , 2.53442, 1.96981, 0.7953 , 0.0339 , 8.33344E-5 },
    new[] { 1.54942, 1.87309, 2.26477, 1.06949, 0.04745, 1.76454E-4 },
    new[] { 1.68116, 0.16541, 1.74783, 0.94506, 0.03464, 7.04399E-4 },
    new[] { 1.58616, 1.96424, 1.89585, 1.11849, 0.01695, 1.33895E-4 },
    new[] { 1.47532, 1.46252, 2.57086, 1.09538, 0.35918, 3.63668E-4 },
    new[] { 1.23475, 0.50254, 2.36006, 0.81872, 0.05412, 8.20155E-5 },
    new[] { 0.99869, 1.22984, 2.07121, 0.56593, 0.50299, 0.0018     },
    new[] { 1.46881, 2.21203, 2.20188, 1.0276 , 0.02842, 9.85565E-5 },
    new[] { 1.4685 , 2.33651, 2.28153, 1.08012, 0.02133, 4.05555E-5 },
};

Console.WriteLine("== 1. 엑셀 수동 측정 데이터 피팅 vs Origin (PPT 4번 슬라이드) ==");
Console.WriteLine("  조건             | w(본 프로그램)        | w(Origin)            | Red.Chi2(본)  Red.Chi2(Origin)");
for (int i = 0; i < data.Length; i++)
{
    var f = SineFitter.Fit(kv, data[i].y);
    var o = origin[i];
    Console.WriteLine($"  {data[i].name,-16} | {f.W,8:0.00000} ± {f.WErr,7:0.00000} | {o[2],8:0.00000} ± {o[4],7:0.00000} | {f.ReducedChiSq,10:0.000E+0}  {o[5],10:0.000E+0}");
    // 같은 최소값을 찾았으면 w 가 일치해야 하고, 다르면 같은 데이터에서 Origin 파라미터보다 잔차가 작아야 합니다.
    double ssrOrigin = data[i].y.Select((yy, j) => Math.Pow(yy - SineFitter.Model(kv[j], o[0], o[1], o[2], o[3]), 2)).Sum() / (kv.Length - 4);
    bool same = Math.Abs(f.W - o[2]) < 2e-3;
    bool better = f.ReducedChiSq <= ssrOrigin * 1.0005;
    if (!same) Console.WriteLine($"         (엑셀 데이터에 Origin 파라미터를 넣은 Reduced χ² = {ssrOrigin:0.000E+0})");
    Check(f.Success && (same || better), $"{data[i].name}: " + (same ? "Origin 과 같은 w" : "Origin 보다 잔차가 작거나 같음 (다른 국소해)"));
    foreach (var wmsg in f.Warnings) Console.WriteLine("         경고: " + wmsg);
}
{
    var f = SineFitter.Fit(kv, data[0].y);
    var o = origin[0];
    Check(Math.Abs(f.Y0 - o[0]) < 1e-3 && Math.Abs(f.Xc - o[1]) < 2e-3 && Math.Abs(f.A - o[3]) < 1e-3,
        $"1310 nm @25℃ 파라미터 일치: y0={f.Y0:0.#####} xc={f.Xc:0.#####} A={f.A:0.#####} (Origin {o[0]}, {o[1]}, {o[3]})");
    Check(Math.Abs(f.Y0Err - 0.02021) < 5e-4 && Math.Abs(f.XcErr - 0.05426) < 1e-3 && Math.Abs(f.AErr - 0.01858) < 5e-4,
        $"표준오차 일치: y0 ±{f.Y0Err:0.#####}, xc ±{f.XcErr:0.#####}, A ±{f.AErr:0.#####} (Origin ±0.02021, ±0.05426, ±0.01858)");
    Check(Math.Abs(f.AdjRSquare - 0.99961) < 5e-5, $"Adj. R-Square = {f.AdjRSquare:0.#####} (Origin 0.99961)");
}

Console.WriteLine();
Console.WriteLine("== 2. y0, A 고정 옵션 (1550 nm @25℃, 반주기 미만 데이터) ==");
{
    // 데모 장비 모델(1550 nm 예시 파라미터)로 만든 잡음 없는 데이터: y0, A 를 알면 w 를 정확히 복원해야 함
    var d = DemoParameters.Preset1550();
    var yy = kv.Select(x => d.Model(x)).ToArray();
    var opt = new SineFitOptions { FixY0 = true, Y0 = d.Y0V, FixA = true, A = d.AmplitudeV };
    var f = SineFitter.Fit(kv, yy, opt);
    Console.WriteLine($"  y0={d.Y0V}, A={d.AmplitudeV} 고정 → w = {f.W:0.00000} ± {f.WErr:0.00000} kV (참값 {d.VpiKv})");
    Check(f.Success && Math.Abs(f.W - d.VpiKv) < 1e-4, "y0·A 고정 피팅이 참값 w 를 복원");
    var f2 = SineFitter.Fit(kv, data[5].y, opt);
    Console.WriteLine($"  엑셀 1550 nm @25℃ 데이터에 같은 고정값 적용 → w = {f2.W:0.00000} ± {f2.WErr:0.00000} kV (자유 피팅 {SineFitter.Fit(kv, data[5].y).W:0.000} kV)");
}

Console.WriteLine();
Console.WriteLine("== 3. 스캔 계획 ==");
{
    var s = new ScanSettings { StartKv = -1, EndKv = 1, StepKv = 0.1 };
    var st = ScanPlanner.Build(s, 1.0);
    Check(st.Count == 21 && Math.Abs(st[0].SetKv + 1) < 1e-12 && Math.Abs(st[20].SetKv - 1) < 1e-12, $"단방향 -1→+1 kV, 0.1 kV 간격: {st.Count}점");
    Check(st[0].IsJump && !st[1].IsJump, "0 V → -1 kV 첫 점은 긴 안정화 적용");
    s.Mode = ScanMode.RoundTrip; s.Repeats = 2;
    st = ScanPlanner.Build(s, 1.0);
    Check(st.Count == 82 && st[21].Direction == SweepDirection.Backward && Math.Abs(st[40].SetKv + 1) < 1e-12, $"왕복 2회: {st.Count}점 (41점 × 2)");
    s.Mode = ScanMode.OneWay; s.Repeats = 2;
    st = ScanPlanner.Build(s, 1.0);
    Check(st.Count == 42 && st[21].IsJump, "단방향 2회: 두 번째 반복 시작점은 긴 안정화 적용");
    bool threw = false;
    try { ScanPlanner.Build(new ScanSettings { StartKv = -1.2, EndKv = 1 }, 1.0); } catch (ArgumentException) { threw = true; }
    Check(threw, "±1 kV 를 넘는 범위는 거부");
    var odd = ScanPlanner.SweepValues(0, 1, 0.3);
    Check(odd.Count == 5 && Math.Abs(odd[4] - 1) < 1e-12, "간격으로 나누어떨어지지 않으면 끝점 추가 (0, 0.3, 0.6, 0.9, 1.0)");
}

Console.WriteLine();
Console.WriteLine("== 4. 출력 제한 ==");
{
    using var hw = new DemoHardware(new HardwareConfig(), DemoParameters.Preset1310(), 1);
    hw.Connect();
    bool threw = false;
    try { hw.SetOutputKv(1.05); } catch (ArgumentOutOfRangeException) { threw = true; }
    Check(threw, "1.05 kV 출력 요청 거부 (AMJ-1B20 정격 ±1 kV)");
    var cfg = new HardwareConfig { MaxOutputKv = 5.0 };
    Check(Math.Abs(cfg.EffectiveMaxKv - 1.0) < 1e-12, "설정값을 5 kV 로 바꿔도 실제 제한은 1 kV");
    Check(Math.Abs(new HardwareConfig().KvToGeneratorV(-1.0) + 10.0) < 1e-12, "-1 kV → 신호발생기 오프셋 -10 V (이득 100)");
}

Console.WriteLine();
Console.WriteLine("== 5. 데모 장비 스캔 (-1 ~ +1 kV, 0.1 kV, 안정화 50 ms, 평균 10 ms) ==");
{
    var demo = DemoParameters.Preset1310();
    using var hw = new DemoHardware(new HardwareConfig(), demo, 7);
    hw.Connect();
    var s = new ScanSettings();
    var steps = ScanPlanner.Build(s, 1.0);
    var est = ScanPlanner.Estimate(steps, s, demo.OverheadMs);
    int reports = 0;
    var prog = new SyncProgress<ScanProgress>(p => reports++);
    var r = MeasurementEngine.Run(hw, s, prog, CancellationToken.None);
    var an = new AnalysisSettings();
    var swFit = Stopwatch.StartNew();
    var f = an.Fit(r.Points);
    swFit.Stop();
    Console.WriteLine($"  예상 {est.TotalSeconds:0.000} s / 실제 {r.Elapsed.TotalSeconds:0.000} s, 점당 추가시간 {r.MeanOverheadMs:0.0} ms, 피팅 계산 {swFit.Elapsed.TotalMilliseconds:0.0} ms");
    Console.WriteLine($"  피팅 w = {f.W:0.0000} ± {f.WErr:0.0000} kV (가상 장비 설정 {demo.VpiKv}), Adj.R² = {f.AdjRSquare:0.#####}");
    Check(r.Completed && r.Points.Count == 21 && reports == 21, "21점 측정 완료, 진행 보고 21회");
    Check(Math.Abs(r.Elapsed.TotalSeconds - est.TotalSeconds) < 0.25, "실제 소요 시간이 예상과 비슷함");
    Check(Math.Abs(f.W - demo.VpiKv) < 4 * f.WErr + 0.01, "피팅 w 가 가상 장비의 Vπ 와 일치");
    Check(Math.Abs(hw.OutputKv) < 1e-12, "측정 후 출력 0 V 복귀");
    Check(r.Points.All(p => Math.Abs(p.MonitorKv - p.SetKv) < 0.01), "CH2 모니터 전압 ≈ 설정 전압");

    // 중단
    using var cts = new CancellationTokenSource(400);
    var r2 = MeasurementEngine.Run(hw, s, null, cts.Token);
    Check(r2.Cancelled && r2.Points.Count > 0 && r2.Points.Count < 21 && Math.Abs(hw.OutputKv) < 1e-12, $"측정 중단 시 {r2.Points.Count}점까지 저장, 출력 0 V 복귀");

    // 안정화 시간이 지나치게 짧으면 값이 덜 따라옴 (시정수 3 ms 흉내)
    var fast = new ScanSettings { SettleMs = 0, AverageMs = 1, JumpSettleMs = 0 };
    var r3 = MeasurementEngine.Run(hw, fast, null, CancellationToken.None);
    double lag = r3.Points.Skip(1).Average(p => Math.Abs(p.MonitorKv - p.SetKv));
    Console.WriteLine($"  안정화 0 ms 일 때 모니터-설정 평균 차이 {lag * 1000:0.0} V (안정화 50 ms: {r.Points.Average(p => Math.Abs(p.MonitorKv - p.SetKv)) * 1000:0.0} V)");
    Check(lag > 0.005, "안정화 시간 부족 효과가 데모에서 재현됨");
}

Console.WriteLine();
Console.WriteLine("== 6. 전기-광학 계수 r = λd/(n³LVπ) ==");
{
    var o = ElectroOptic.Compute(new ElectroOptic.Input { WavelengthNm = 1310, ElectrodeGapMm = 1, LengthMm = 10, RefractiveIndex = 2.2, VpiKv = 2.0, VpiErrKv = 0.02 });
    double expect = 1310e-9 * 1e-3 / (Math.Pow(2.2, 3) * 10e-3 * 2000) * 1e12;
    Console.WriteLine($"  λ=1310 nm, d=1 mm, L=10 mm, n=2.2, Vπ=2.0±0.02 kV → r = {o.RPmPerV:0.0000} ± {o.RErrPmPerV:0.0000} pm/V (SI 단위 직접 계산 {expect:0.0000})");
    Check(o.Valid && Math.Abs(o.RPmPerV - expect) < 1e-9 && Math.Abs(o.RErrPmPerV - expect * 0.01) < 1e-9, "단위 환산 및 오차 전파");
}

Console.WriteLine();
Console.WriteLine();
Console.WriteLine("== 6b. LiNbO3 굴절률 (Zelmon 1997 셀마이어) ==");
{
    double no13 = LnRefractiveIndex.No(1310), ne13 = LnRefractiveIndex.Ne(1310), no15 = LnRefractiveIndex.No(1550), ne15 = LnRefractiveIndex.Ne(1550);
    Console.WriteLine($"  1310 nm: n_o = {no13:0.0000}, n_e = {ne13:0.0000} / 1550 nm: n_o = {no15:0.0000}, n_e = {ne15:0.0000}");
    Check(Math.Abs(no13 - 2.220) < 5e-4 && Math.Abs(ne13 - 2.145) < 1e-3 && Math.Abs(no15 - 2.211) < 5e-4 && Math.Abs(ne15 - 2.138) < 5e-4,
        "1310/1550 nm 값이 문헌값(n_o 2.220/2.211, n_e 2.145/2.138)과 일치");
    Check(double.IsNaN(LnRefractiveIndex.No(300)), "적용 범위(400~5000 nm) 밖은 계산하지 않음");
}

Console.WriteLine("== 7. CSV 저장: 측정점 + 피팅값/잔차 + 피팅 곡선(라인) ==");
{
    using var hw = new DemoHardware(new HardwareConfig(), DemoParameters.Preset1310(), 11);
    hw.Connect();
    var s = new ScanSettings();
    var r = MeasurementEngine.Run(hw, s, null, CancellationToken.None);
    var an = new AnalysisSettings();
    var f = an.Fit(r.Points);
    var path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "lneo_selftest.csv");
    CsvExport.WriteMeasurement(path, new MeasurementInfo(), s, hw.Config, r, an, f, null, null, true, FitCurve.DefaultPoints);
    var lines = System.IO.File.ReadAllLines(path);
    int h = Array.FindIndex(lines, l => l.StartsWith("Index,"));
    var header = lines[h].Split(',');
    var rows = lines.Skip(h + 1).Select(l => l.Split(',')).ToList();
    Console.WriteLine("  표 머리글: " + lines[h]);
    Console.WriteLine("  " + lines.First(l => l.StartsWith("# Fit curve")));
    Check(header.Length == 15 && rows.All(c => c.Length == 15), $"모든 행이 {header.Length}열");
    Check(rows.Count == FitCurve.DefaultPoints, $"행 수 = 피팅 곡선 점 수 {rows.Count}");
    double P(string t) => double.Parse(t, System.Globalization.CultureInfo.InvariantCulture);
    Check(Math.Abs(P(rows[0][13]) + 3) < 1e-9 && Math.Abs(P(rows[rows.Count - 1][13]) - 3) < 1e-9, "곡선 범위 -3 ~ +3 kV (그래프와 동일)");
    bool fitOk = true;
    for (int i = 0; i < r.Points.Count; i++)
    {
        double yfit = P(rows[i][11]), res = P(rows[i][12]);
        fitOk &= Math.Abs(yfit - f.Evaluate(r.Points[i].SetKv)) < 1e-6 && Math.Abs(res - (r.Points[i].OpticalV - yfit)) < 1e-6;
    }
    Check(fitOk, "측정점별 Fit (V), Residual (V) 값이 피팅 함수와 일치");
    Check(rows.Skip(r.Points.Count).All(c => c.Take(13).All(string.IsNullOrEmpty)), "측정점 이후 행은 측정 열이 비어 있음");
    bool curveOk = rows.All(c => Math.Abs(P(c[14]) - f.Evaluate(P(c[13]))) < 1e-6);
    Check(curveOk, "피팅 곡선 y 값 = 피팅 함수");
}

Console.WriteLine();
Console.WriteLine("== 8. 결과 기록 CSV (메모 열, 예전 형식 호환) ==");
{
    var fit = SineFitter.Fit(kv, data[0].y);
    var info = new MeasurementInfo { Sample = "LN, z-cut", WavelengthNm = 1310, TemperatureC = 25, Memo = "편광자 45°, \"재측정\"" };
    var rec = ResultRecord.Create(info, fit, null, null, TimeSpan.FromSeconds(1.5), "자동");
    var path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "lneo_history.csv");
    CsvExport.WriteHistory(path, new[] { rec });
    var head = System.IO.File.ReadAllLines(path)[0].TrimStart('\uFEFF');
    Console.WriteLine("  머리글: " + head);
    Check(head.StartsWith("Time,Sample,Wavelength (nm),Temperature (C),Memo,y0") && !head.Contains("Condition"), "조건 열 없음, 메모 열이 온도 오른쪽");
    var back = CsvExport.ReadHistory(path);
    Check(back.Count == 1 && back[0].Memo == info.Memo && back[0].Sample == info.Sample && Math.Abs(back[0].W - fit.W) < 1e-4,
        "저장 후 다시 읽기 (쉼표·따옴표가 든 메모 포함)");
    // 예전 형식 (Condition 열 있음, Memo 없음)
    var old = "Time,Sample,Condition,Wavelength (nm),Temperature (C),y0,y0 Err,xc (kV),xc Err,w = Vpi (kV),w Err,A,A Err,Reduced Chi-Sqr,Adj. R-Square,N,n,d (mm),L (mm),r (pm/V),r Err (pm/V),Duration (s),Note,File\n" +
              "2026-11-02 19:33:58,LN,1310 nm @25℃,1310,25,1.49952,0.0074,2.63941,0.02002,2.07264,0.01374,0.85708,0.00678,1.15874E-5,0.99994,21,2.14,1,10,6.4492,0.0428,1.57,자동,a.csv\n";
    System.IO.File.WriteAllText(path, old, new System.Text.UTF8Encoding(true));
    var o = CsvExport.ReadHistory(path);
    Check(o.Count == 1 && Math.Abs(o[0].W - 2.07264) < 1e-9 && Math.Abs(o[0].TemperatureC - 25) < 1e-9 && o[0].Memo == "" && o[0].Note == "자동",
        "예전 형식 결과 기록 파일도 읽힘");
}

Console.WriteLine();
Console.WriteLine(fails == 0 ? "모든 검사 통과" : $"실패 {fails}건");
return fails == 0 ? 0 : 1;

/// <summary>콘솔에서 즉시 호출되는 IProgress</summary>
sealed class SyncProgress<T> : IProgress<T>
{
    private readonly Action<T> _a;
    public SyncProgress(Action<T> a) { _a = a; }
    public void Report(T value) => _a(value);
}
