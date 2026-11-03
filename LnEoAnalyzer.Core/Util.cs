using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading;

namespace LnEoAnalyzer.Core;

/// <summary>수치 계산 보조 함수 모음</summary>
public static class Num
{
    public static readonly CultureInfo Inv = CultureInfo.InvariantCulture;

    public static bool IsFinite(double x) => !double.IsNaN(x) && !double.IsInfinity(x);

    public static double Clamp(double x, double min, double max) => x < min ? min : (x > max ? max : x);

    public static int Clamp(int x, int min, int max) => x < min ? min : (x > max ? max : x);

    /// <summary>mm:ss.f 형식 (1시간 이상이면 h:mm:ss)</summary>
    public static string FormatDuration(TimeSpan t)
    {
        if (t < TimeSpan.Zero) t = TimeSpan.Zero;
        if (t.TotalHours >= 1)
            return string.Format(Inv, "{0}:{1:00}:{2:00}", (int)t.TotalHours, t.Minutes, t.Seconds);
        return string.Format(Inv, "{0:00}:{1:00}.{2}", (int)t.TotalMinutes, t.Seconds, t.Milliseconds / 100);
    }

    public static string F(double x, string fmt = "G6") => IsFinite(x) ? x.ToString(fmt, Inv) : "";

    /// <summary>단순 선형회귀 y = slope*x + intercept</summary>
    public static bool LinearFit(IReadOnlyList<double> xs, IReadOnlyList<double> ys, out double slope, out double intercept, out double r2)
    {
        slope = double.NaN; intercept = double.NaN; r2 = double.NaN;
        int n = 0; double sx = 0, sy = 0;
        for (int i = 0; i < xs.Count; i++)
        {
            if (!IsFinite(xs[i]) || !IsFinite(ys[i])) continue;
            n++; sx += xs[i]; sy += ys[i];
        }
        if (n < 2) return false;
        double mx = sx / n, my = sy / n, sxx = 0, sxy = 0, syy = 0;
        for (int i = 0; i < xs.Count; i++)
        {
            if (!IsFinite(xs[i]) || !IsFinite(ys[i])) continue;
            double dx = xs[i] - mx, dy = ys[i] - my;
            sxx += dx * dx; sxy += dx * dy; syy += dy * dy;
        }
        if (sxx <= 0) return false;
        slope = sxy / sxx;
        intercept = my - slope * mx;
        r2 = syy > 0 ? (sxy * sxy) / (sxx * syy) : 1.0;
        return true;
    }
}

/// <summary>
/// 밀리초 단위의 정밀 대기. Thread.Sleep 은 Windows 기본 타이머 해상도(15.6 ms)에 묶여 있으므로
/// 남은 시간이 짧아지면 SpinWait 로 전환합니다.
/// </summary>
public static class PreciseTimer
{
    public static void Delay(double milliseconds, CancellationToken ct)
    {
        if (milliseconds <= 0) { ct.ThrowIfCancellationRequested(); return; }
        var sw = Stopwatch.StartNew();
        while (true)
        {
            ct.ThrowIfCancellationRequested();
            double remain = milliseconds - sw.Elapsed.TotalMilliseconds;
            if (remain <= 0) break;
            if (remain > 3.0)
                Thread.Sleep((int)Math.Min(remain - 2.0, 20.0));
            else
                Thread.SpinWait(200);
        }
    }
}

/// <summary>작은 선형계(2~4 변수)용 가우스 소거</summary>
public static class LinAlg
{
    /// <summary>A x = b 를 풉니다. 특이행렬이면 null.</summary>
    public static double[]? Solve(double[,] a, double[] b)
    {
        int n = b.Length;
        var m = new double[n, n + 1];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++) m[i, j] = a[i, j];
            m[i, n] = b[i];
        }
        for (int col = 0; col < n; col++)
        {
            int piv = col; double best = Math.Abs(m[col, col]);
            for (int r = col + 1; r < n; r++)
                if (Math.Abs(m[r, col]) > best) { best = Math.Abs(m[r, col]); piv = r; }
            if (best < 1e-300) return null;
            if (piv != col)
                for (int j = 0; j <= n; j++) { double t = m[col, j]; m[col, j] = m[piv, j]; m[piv, j] = t; }
            for (int r = 0; r < n; r++)
            {
                if (r == col) continue;
                double f = m[r, col] / m[col, col];
                if (f == 0) continue;
                for (int j = col; j <= n; j++) m[r, j] -= f * m[col, j];
            }
        }
        var x = new double[n];
        for (int i = 0; i < n; i++) x[i] = m[i, n] / m[i, i];
        return x;
    }

    /// <summary>역행렬. 특이행렬이면 null.</summary>
    public static double[,]? Inverse(double[,] a)
    {
        int n = a.GetLength(0);
        var inv = new double[n, n];
        for (int c = 0; c < n; c++)
        {
            var e = new double[n]; e[c] = 1;
            var col = Solve(a, e);
            if (col == null) return null;
            for (int r = 0; r < n; r++) inv[r, c] = col[r];
        }
        return inv;
    }
}
