using System;

namespace LnEoAnalyzer.Core;

/// <summary>
/// 반파장 전압 → 전기-광학 계수:  r = λ·d / (n³·L·Vπ)
///   λ : 레이저 파장, d : 전극 사이 거리, n : 해당 파장에서 LN 굴절률,
///   L : 레이저 진행 방향 LN 길이, Vπ : 반파장 전압 (= 사인 피팅의 w)
/// 단위: λ[nm], d[mm], L[mm], Vπ[kV] 를 넣으면 r[pm/V] = λ·d / (n³·L·Vπ) 가 그대로 성립합니다.
///   (λ·1e-9 m × d·1e-3 m) / (n³ × L·1e-3 m × Vπ·1e3 V) = λ·d/(n³·L·Vπ) × 1e-12 m/V
/// </summary>
public static class ElectroOptic
{
    public sealed class Input
    {
        public double WavelengthNm { get; set; } = 1310;
        public double ElectrodeGapMm { get; set; } = 1.0;
        public double LengthMm { get; set; } = 10.0;
        public double RefractiveIndex { get; set; } = 2.14;
        public double VpiKv { get; set; } = double.NaN;
        public double VpiErrKv { get; set; } = double.NaN;
    }

    public sealed class Output
    {
        public bool Valid { get; set; }
        public string Message { get; set; } = "";
        /// <summary>전기-광학 계수 (pm/V)</summary>
        public double RPmPerV { get; set; } = double.NaN;
        /// <summary>Vπ 표준오차만 전파한 r 의 오차 (pm/V)</summary>
        public double RErrPmPerV { get; set; } = double.NaN;
    }

    public static Output Compute(Input i)
    {
        var o = new Output();
        if (!(i.WavelengthNm > 0)) { o.Message = "파장을 입력하세요."; return o; }
        if (!(i.ElectrodeGapMm > 0)) { o.Message = "전극 사이 거리 d 를 입력하세요."; return o; }
        if (!(i.LengthMm > 0)) { o.Message = "LN 길이 L 을 입력하세요."; return o; }
        if (!(i.RefractiveIndex > 0)) { o.Message = "굴절률 n 을 입력하세요."; return o; }
        if (!(Num.IsFinite(i.VpiKv) && i.VpiKv > 0)) { o.Message = "반파장 전압(피팅 w)이 필요합니다."; return o; }

        double n3 = i.RefractiveIndex * i.RefractiveIndex * i.RefractiveIndex;
        o.RPmPerV = i.WavelengthNm * i.ElectrodeGapMm / (n3 * i.LengthMm * i.VpiKv);
        if (Num.IsFinite(i.VpiErrKv)) o.RErrPmPerV = o.RPmPerV * i.VpiErrKv / i.VpiKv;
        o.Valid = true;
        o.Message = "계산 완료";
        return o;
    }
}

/// <summary>
/// 일치 용융(congruent) LiNbO3 굴절률 — Zelmon, Small, Jundt, J. Opt. Soc. Am. B 14, 3319 (1997), 21 ℃, 0.4–5.0 µm.
///   n² − 1 = Σ Bᵢλ² / (λ² − Cᵢ)   (λ: µm)
/// 1310 nm: n_o 2.2200, n_e 2.1451 / 1550 nm: n_o 2.2111, n_e 2.1376
/// </summary>
public static class LnRefractiveIndex
{
    public const double MinNm = 400, MaxNm = 5000;
    public const string Source = "Zelmon et al., J. Opt. Soc. Am. B 14, 3319 (1997), congruent LiNbO3, 21 ℃";

    private static double Sellmeier(double nm, double b1, double c1, double b2, double c2, double b3, double c3)
    {
        double l2 = (nm / 1000.0) * (nm / 1000.0);
        return Math.Sqrt(1 + b1 * l2 / (l2 - c1) + b2 * l2 / (l2 - c2) + b3 * l2 / (l2 - c3));
    }

    public static bool InRange(double nm) => nm >= MinNm && nm <= MaxNm;

    /// <summary>정상광선 굴절률 n_o (편광 ⊥ 광학축 z)</summary>
    public static double No(double nm) => InRange(nm) ? Sellmeier(nm, 2.6734, 0.01764, 1.2290, 0.05914, 12.614, 474.60) : double.NaN;

    /// <summary>이상광선 굴절률 n_e (편광 ∥ 광학축 z)</summary>
    public static double Ne(double nm) => InRange(nm) ? Sellmeier(nm, 2.9804, 0.02047, 0.5981, 0.0666, 8.9543, 416.08) : double.NaN;
}
