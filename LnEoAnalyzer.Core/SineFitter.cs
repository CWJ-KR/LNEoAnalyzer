using System;
using System.Collections.Generic;
using System.Linq;

namespace LnEoAnalyzer.Core;

public sealed class SineFitOptions
{
    /// <summary>y0 를 고정값으로 둘지 여부</summary>
    public bool FixY0 { get; set; }
    public double Y0 { get; set; }
    /// <summary>A 를 고정값으로 둘지 여부</summary>
    public bool FixA { get; set; }
    public double A { get; set; }
    public int MaxIterations { get; set; } = 500;
}

/// <summary>사인 피팅 결과. 각 오차는 Origin 과 같은 방식(공분산 × Reduced χ²)의 표준오차입니다.</summary>
public sealed class SineFitResult
{
    public bool Success { get; set; }
    public string Message { get; set; } = "";
    public List<string> Warnings { get; } = new List<string>();

    public double Y0 { get; set; } = double.NaN;
    public double Xc { get; set; } = double.NaN;
    /// <summary>반파장 전압 (x 와 같은 단위, kV)</summary>
    public double W { get; set; } = double.NaN;
    public double A { get; set; } = double.NaN;
    public double Y0Err { get; set; } = double.NaN;
    public double XcErr { get; set; } = double.NaN;
    public double WErr { get; set; } = double.NaN;
    public double AErr { get; set; } = double.NaN;

    public int N { get; set; }
    public int Dof { get; set; }
    public double Ssr { get; set; } = double.NaN;
    public double ReducedChiSq { get; set; } = double.NaN;
    public double RSquare { get; set; } = double.NaN;
    public double AdjRSquare { get; set; } = double.NaN;
    public int Iterations { get; set; }

    public double Evaluate(double x) => SineFitter.Model(x, Y0, Xc, W, A);
}

/// <summary>
/// y = y0 + A·sin(π(x − xc)/w) 비선형 최소제곱 피팅 (Origin 의 "Sine" 함수와 동일한 형태).
/// 1) w 를 로그 격자로 훑으며 (w 고정 시 선형인) y0, A, xc 를 구해 초기값을 찾고
/// 2) Levenberg–Marquardt 로 4개 파라미터를 동시에 최적화합니다.
/// 결과는 A &gt; 0, 0 ≤ xc &lt; 2w 로 정규화합니다 (Origin 결과표와 같은 표현).
/// </summary>
public static class SineFitter
{
    private const int IY0 = 0, IXC = 1, IW = 2, IA = 3;

    public static double Model(double x, double y0, double xc, double w, double a)
        => y0 + a * Math.Sin(Math.PI * (x - xc) / w);

    public static SineFitResult Fit(IReadOnlyList<double> xsIn, IReadOnlyList<double> ysIn, SineFitOptions? options = null)
    {
        var opt = options ?? new SineFitOptions();
        var res = new SineFitResult();

        var xs = new List<double>(); var ys = new List<double>();
        for (int i = 0; i < Math.Min(xsIn.Count, ysIn.Count); i++)
        {
            if (Num.IsFinite(xsIn[i]) && Num.IsFinite(ysIn[i])) { xs.Add(xsIn[i]); ys.Add(ysIn[i]); }
        }
        int n = xs.Count;
        var free = new bool[4] { !opt.FixY0, true, true, !opt.FixA };
        int k = free.Count(f => f);
        res.N = n;
        if (n < k + 1)
        {
            res.Message = $"피팅에 필요한 점이 부족합니다 (현재 {n}개, 최소 {k + 1}개).";
            return res;
        }

        double xmin = xs.Min(), xmax = xs.Max(), span = xmax - xmin;
        if (span <= 0) { res.Message = "x 값의 범위가 0 입니다."; return res; }
        double xmid = 0.5 * (xmin + xmax);

        // 최소 x 간격 (나이퀴스트 한계: 반주기 w 는 점 간격보다 커야 의미가 있음)
        var sorted = xs.OrderBy(v => v).ToList();
        double minGap = double.MaxValue;
        for (int i = 1; i < sorted.Count; i++)
        {
            double d = sorted[i] - sorted[i - 1];
            if (d > span * 1e-6 && d < minGap) minGap = d;
        }
        if (minGap == double.MaxValue) minGap = span / n;

        double wLo = Math.Max(minGap * 1.2, span / 40.0);
        double wHi = span * 20.0;

        // ---- 1단계: w 격자 탐색 ----
        const int G = 900;
        var grid = new List<(double w, double ssr, double[] p)>();
        for (int g = 0; g < G; g++)
        {
            double w = wLo * Math.Pow(wHi / wLo, g / (double)(G - 1));
            var p = LinearForW(xs, ys, w, xmid, opt);
            if (p == null) continue;
            grid.Add((w, Ssr(xs, ys, p), p));
        }
        if (grid.Count == 0) { res.Message = "초기값을 찾지 못했습니다."; return res; }

        // 국소 최소값 후보 (최대 4개) → 각각 LM 으로 다듬어 가장 좋은 결과 선택
        var cand = new List<double[]>();
        for (int i = 0; i < grid.Count; i++)
        {
            bool left = i == 0 || grid[i].ssr <= grid[i - 1].ssr;
            bool right = i == grid.Count - 1 || grid[i].ssr <= grid[i + 1].ssr;
            if (left && right) cand.Add(grid[i].p);
        }
        cand = cand.OrderBy(p => Ssr(xs, ys, p)).Take(4).ToList();
        if (cand.Count == 0) cand.Add(grid.OrderBy(g => g.ssr).First().p);

        double[]? best = null; double bestSsr = double.MaxValue; int bestIter = 0;
        foreach (var c in cand)
        {
            var p = (double[])c.Clone();
            int it = LevenbergMarquardt(xs, ys, p, free, opt.MaxIterations, wHi * 2.0);
            double s = Ssr(xs, ys, p);
            if (Num.IsFinite(s) && s < bestSsr) { bestSsr = s; best = p; bestIter = it; }
        }
        if (best == null) { res.Message = "피팅이 수렴하지 않았습니다."; return res; }

        // ---- 정규화: A > 0, 0 ≤ xc < 2w ----
        if (best[IW] < 0) { best[IW] = -best[IW]; best[IA] = -best[IA]; best[IXC] = -best[IXC]; }
        if (best[IA] < 0 && free[IA]) { best[IA] = -best[IA]; best[IXC] += best[IW]; }
        double period = 2.0 * best[IW];
        best[IXC] = best[IXC] - period * Math.Floor(best[IXC] / period);

        res.Y0 = best[IY0]; res.Xc = best[IXC]; res.W = best[IW]; res.A = best[IA];
        res.Iterations = bestIter;
        res.Ssr = Ssr(xs, ys, best);
        res.Dof = n - k;
        res.ReducedChiSq = res.Dof > 0 ? res.Ssr / res.Dof : double.NaN;

        double mean = ys.Average();
        double sst = ys.Sum(y => (y - mean) * (y - mean));
        res.RSquare = sst > 0 ? 1.0 - res.Ssr / sst : double.NaN;
        res.AdjRSquare = (sst > 0 && res.Dof > 0) ? 1.0 - (res.Ssr / res.Dof) / (sst / (n - 1)) : double.NaN;

        // ---- 표준오차: sqrt(diag((JᵀJ)⁻¹) · Reduced χ²) ----
        var err = new double[4];
        for (int j = 0; j < 4; j++) err[j] = free[j] ? double.NaN : 0.0;
        var jtj = BuildJtJ(xs, best, free, out var map);
        var inv = LinAlg.Inverse(jtj);
        if (inv != null && res.Dof > 0)
        {
            for (int a = 0; a < map.Length; a++)
            {
                double v = inv[a, a] * res.ReducedChiSq;
                err[map[a]] = v >= 0 ? Math.Sqrt(v) : double.NaN;
            }
        }
        res.Y0Err = err[IY0]; res.XcErr = err[IXC]; res.WErr = err[IW]; res.AErr = err[IA];
        res.Success = Num.IsFinite(res.W) && res.W > 0;
        res.Message = res.Success ? "피팅 완료" : "피팅 실패";

        // ---- 경고 ----
        if (res.Success)
        {
            if (span < 0.75 * res.W)
                res.Warnings.Add(string.Format(Num.Inv,
                    "측정 전압 폭({0:0.###} kV)이 반파장 전압 w({1:0.###} kV)에 비해 좁아 사인 곡선의 일부만 측정되었습니다. " +
                    "w 의 신뢰도가 낮으므로 y0·A 고정 피팅(편광자 회전으로 구한 최대/최소 광신호 이용)을 고려하세요.",
                    span, res.W));
            if (Num.IsFinite(res.WErr) && res.WErr / res.W > 0.1)
                res.Warnings.Add(string.Format(Num.Inv, "w 의 상대 오차가 {0:0.#}% 로 큽니다.", 100 * res.WErr / res.W));
            if (res.W > wHi * 0.9 * 2.0)
                res.Warnings.Add("w 가 탐색 범위의 상한에 가깝습니다 (신호가 거의 직선입니다).");
            if (inv == null)
                res.Warnings.Add("공분산 행렬을 계산할 수 없어 표준오차를 구하지 못했습니다.");
        }
        return res;
    }

    /// <summary>w 고정 시 y = y0 + a·sin(πx'/w) + b·cos(πx'/w) (x' = x − xmid) 선형 최소제곱 → [y0, xc, w, A]</summary>
    private static double[]? LinearForW(List<double> xs, List<double> ys, double w, double xmid, SineFitOptions opt)
    {
        int n = xs.Count;
        bool fy = opt.FixY0;
        int m = fy ? 2 : 3;
        var ata = new double[m, m]; var atb = new double[m];
        var row = new double[m];
        for (int i = 0; i < n; i++)
        {
            double t = Math.PI * (xs[i] - xmid) / w;
            double s = Math.Sin(t), c = Math.Cos(t);
            double y = ys[i];
            if (fy) { row[0] = s; row[1] = c; y -= opt.Y0; }
            else { row[0] = 1; row[1] = s; row[2] = c; }
            for (int a = 0; a < m; a++)
            {
                atb[a] += row[a] * y;
                for (int b = 0; b < m; b++) ata[a, b] += row[a] * row[b];
            }
        }
        var sol = LinAlg.Solve(ata, atb);
        if (sol == null) return null;
        double y0 = fy ? opt.Y0 : sol[0];
        double ca = fy ? sol[0] : sol[1], cb = fy ? sol[1] : sol[2];
        // a·sin t + b·cos t = A·sin(t − φ),  a = A cos φ, b = −A sin φ
        double amp = Math.Sqrt(ca * ca + cb * cb);
        double phi = Math.Atan2(-cb, ca);
        if (opt.FixA) amp = opt.A;
        double xc = xmid + phi * w / Math.PI;
        return new[] { y0, xc, w, amp };
    }

    private static double Ssr(List<double> xs, List<double> ys, double[] p)
    {
        double s = 0;
        for (int i = 0; i < xs.Count; i++)
        {
            double r = ys[i] - Model(xs[i], p[IY0], p[IXC], p[IW], p[IA]);
            s += r * r;
        }
        return s;
    }

    private static void Jacobian(double x, double[] p, double[] d)
    {
        double w = p[IW], a = p[IA], u = x - p[IXC];
        double t = Math.PI * u / w;
        double c = Math.Cos(t);
        d[IY0] = 1.0;
        d[IXC] = -a * c * Math.PI / w;
        d[IW] = -a * c * Math.PI * u / (w * w);
        d[IA] = Math.Sin(t);
    }

    private static double[,] BuildJtJ(List<double> xs, double[] p, bool[] free, out int[] map)
    {
        map = Enumerable.Range(0, 4).Where(j => free[j]).ToArray();
        int k = map.Length;
        var jtj = new double[k, k];
        var d = new double[4];
        foreach (var x in xs)
        {
            Jacobian(x, p, d);
            for (int a = 0; a < k; a++)
                for (int b = 0; b < k; b++)
                    jtj[a, b] += d[map[a]] * d[map[b]];
        }
        return jtj;
    }

    private static int LevenbergMarquardt(List<double> xs, List<double> ys, double[] p, bool[] free, int maxIter, double wMax)
    {
        var map = Enumerable.Range(0, 4).Where(j => free[j]).ToArray();
        int k = map.Length;
        double lambda = 1e-3;
        double ssr = Ssr(xs, ys, p);
        var d = new double[4];
        int iter;
        for (iter = 1; iter <= maxIter; iter++)
        {
            var jtj = new double[k, k];
            var jtr = new double[k];
            for (int i = 0; i < xs.Count; i++)
            {
                Jacobian(xs[i], p, d);
                double r = ys[i] - Model(xs[i], p[IY0], p[IXC], p[IW], p[IA]);
                for (int a = 0; a < k; a++)
                {
                    jtr[a] += d[map[a]] * r;
                    for (int b = 0; b < k; b++) jtj[a, b] += d[map[a]] * d[map[b]];
                }
            }

            bool improved = false;
            while (lambda < 1e16)
            {
                var m = (double[,])jtj.Clone();
                for (int a = 0; a < k; a++) m[a, a] += lambda * Math.Max(jtj[a, a], 1e-12);
                var delta = LinAlg.Solve(m, jtr);
                if (delta == null) { lambda *= 10; continue; }
                var trial = (double[])p.Clone();
                for (int a = 0; a < k; a++) trial[map[a]] += delta[a];
                if (trial[IW] <= 0 || trial[IW] > wMax || !Num.IsFinite(trial[IW])) { lambda *= 10; continue; }
                double s2 = Ssr(xs, ys, trial);
                if (Num.IsFinite(s2) && s2 < ssr)
                {
                    double rel = (ssr - s2) / Math.Max(ssr, 1e-300);
                    Array.Copy(trial, p, 4);
                    ssr = s2;
                    lambda = Math.Max(lambda / 10, 1e-15);
                    improved = true;
                    if (rel < 1e-13) return iter;
                    break;
                }
                lambda *= 10;
            }
            if (!improved) return iter; // 더 이상 개선 불가 → 수렴
        }
        return iter;
    }
}

/// <summary>
/// 피팅 곡선(라인) 데이터. 그래프와 CSV 가 같은 범위·같은 점을 쓰도록 한 곳에서 만듭니다.
/// 범위: 피팅에 사용한 점의 x 최소~최대. extend 이면 양쪽으로 측정 폭만큼 더 늘립니다 (±1 kV 측정 → −3 ~ +3 kV, PPT 그래프와 동일).
/// </summary>
public static class FitCurve
{
    public const int DefaultPoints = 601;

    public static bool Range(IReadOnlyList<double> usedX, bool extend, out double lo, out double hi)
    {
        lo = hi = double.NaN;
        var xs = usedX.Where(Num.IsFinite).ToList();
        if (xs.Count < 2) return false;
        double xmin = xs.Min(), xmax = xs.Max(), span = xmax - xmin;
        if (!(span > 0)) return false;
        lo = extend ? xmin - span : xmin;
        hi = extend ? xmax + span : xmax;
        return true;
    }

    /// <summary>피팅 곡선 (x, y). 만들 수 없으면 false</summary>
    public static bool Build(IReadOnlyList<MeasurementPoint> points, AnalysisSettings analysis, SineFitResult? fit,
        bool extend, int n, out double[] xs, out double[] ys)
    {
        xs = new double[0]; ys = new double[0];
        if (fit == null || !fit.Success || n < 2) return false;
        var used = points.Where(analysis.IsUsed).Select(p => p.X(analysis.UseMonitorX)).ToList();
        if (!Range(used, extend, out var lo, out var hi)) return false;
        xs = new double[n]; ys = new double[n];
        for (int i = 0; i < n; i++)
        {
            xs[i] = Math.Round(lo + (hi - lo) * i / (n - 1), 9);
            ys[i] = fit.Evaluate(xs[i]);
        }
        return true;
    }
}
