using System.Text;

namespace LnEoAnalyzer.Views
{
    /// <summary>
    /// 굴절률 선택 도움말 본문.
    /// 내용을 고치려면 아래 Markup 문자열만 수정하면 됩니다.
    ///   "# 제목"   : 굵은 소제목
    ///   "- 항목"   : 글머리표 항목
    ///   "> 내용"   : 들여쓴 줄 (수식, 부연 설명)
    ///   **굵게**, x_{아래첨자}, x^{위첨자}
    /// </summary>
    internal static class IndexHelpContent
    {
        public const string Markup = @"# 1. 기본 원리
- LiNbO_{3} 는 음의 일축성 결정(n_{e} < n_{o})입니다. 빛의 편광(전기장)이 결정의 **광학축(z축)과 나란하면 n_{e}**, **수직(x 또는 y축)이면 n_{o}** 를 씁니다.
- 파장이 길수록 두 굴절률 모두 작아집니다(분산). 위 표의 값은 Zelmon 셀마이어 식으로 계산한 값입니다.
- 측정 온도, MgO 도핑 여부, 결정 조성에 따라 소수 셋째~넷째 자리가 달라질 수 있습니다.

# 2. 구하려는 전기-광학 계수에 맞는 굴절률
- **r_{33}** : 전계 z방향, 편광 z방향 → **n_{e}**  (LiNbO_{3} 에서 가장 큰 계수)
- **r_{13}** : 전계 z방향, 편광 x 또는 y방향 → **n_{o}**
- **r_{22}** : 전계 x 또는 y방향, 빛이 z축 방향으로 진행(편광은 z에 수직) → **n_{o}**
> 참고 문헌값 (633 nm, pm/V): r_{33} ≈ 31~32, r_{13} ≈ 8.6~10, r_{22} ≈ 3.4~6.8, r_{51} ≈ 28~32
> (앞 값은 고주파(클램프), 뒤 값은 저주파·DC 조건. 이 프로그램처럼 DC 전압을 거는 측정은 뒤 값에 가깝습니다.)

# 3. 변조 방식에 따른 관계식
- **위상 변조** (한 편광만 사용): 해당 편광 방향의 굴절률만 들어갑니다.
> Δn_{z} = −½ n_{e}^{3} r_{33} E_{z}     → n_{e} 사용
> Δn_{x} = −½ n_{o}^{3} r_{13} E_{z}     → n_{o} 사용
- **세기(편광) 변조** — 이 프로그램의 측정 구조 (편광자 1 → LN → 편광자 2)
> Γ = (2πL/λ) [ (n_{o} − n_{e}) − ½ (n_{o}^{3} r_{13} − n_{e}^{3} r_{33}) E_{z} ]
> E_{z} = V/d 를 넣고, Γ 가 π 만큼 바뀌는 전압이 반파장 전압 V_{π} 이므로
> **V_{π} = λd / [ L (n_{e}^{3} r_{33} − n_{o}^{3} r_{13}) ] = λd / (n_{e}^{3} L r_{c})**,   r_{c} = r_{33} − (n_{o}/n_{e})^{3} r_{13}
- 전계 z방향, 빛이 z축에 수직으로 진행, 편광자 45° 배치라면: n 칸에 **n_{e}** 를 넣고, 계산된 r 은 **유효 계수 r_{c}** 입니다.
> 문헌값으로 어림하면 r_{c} ≈ 32 − 1.11 × 10 ≈ 21 pm/V
- 빛이 z축 방향으로 진행하고 전계가 x 또는 y방향이라면: V_{π} = λd / (2 n_{o}^{3} r_{22} L) 이므로 n 칸에 **n_{o}** 를 넣고, 계산된 r 은 **2·r_{22}** 입니다.
> 식의 첫 항 (n_{o} − n_{e}) 는 전압과 무관한 고유 복굴절입니다. 사인 피팅에서는 위상 오프셋 xc 로 나타나며, 온도가 바뀌면 xc 도 달라질 수 있습니다.

# 4. 요약
- 빛의 편광이 z축(광학축)과 나란 → **n_{e}**  (1310 nm: 2.145, 1550 nm: 2.138)
- 빛의 편광이 x 또는 y축과 나란 → **n_{o}**  (1310 nm: 2.220, 1550 nm: 2.211)
- 편광자를 45° 로 두는 세기 변조 측정(전계 z방향)에서는 **n_{e}** 를 넣고 결과를 **r_{c}** 로 해석합니다.

# 참고
> D. E. Zelmon, D. L. Small, D. Jundt, J. Opt. Soc. Am. B 14, 3319 (1997) — refractiveindex.info (LiNbO3, Zelmon o / e)
";

        /// <summary>위 마크업을 RichTextBox 용 RTF 로 변환</summary>
        public static string ToRtf(string markup)
        {
            var sb = new StringBuilder();
            sb.Append(@"{\rtf1\ansi\ansicpg949\deff0{\fonttbl{\f0\fnil\fcharset129 Malgun Gothic;}}");
            sb.Append(@"{\colortbl;\red98\green102\blue242;\red40\green40\blue40;\red90\green90\blue90;}");
            sb.Append(@"\viewkind4\uc1\f0\fs20\cf2 ");
            foreach (var raw in markup.Replace("\r", "").Split('\n'))
            {
                string line = raw.TrimEnd();
                if (line.Length == 0) { sb.Append(@"\pard\sa0\fs10\par\fs20 "); continue; }
                if (line.StartsWith("# "))
                {
                    sb.Append(@"\pard\sb160\sa80\b\fs24\cf1 ");
                    Inline(sb, line.Substring(2));
                    sb.Append(@"\b0\fs20\cf2\par ");
                }
                else if (line.StartsWith("- "))
                {
                    sb.Append(@"\pard\li360\fi-240\sa60\bullet\tab ");
                    Inline(sb, line.Substring(2));
                    sb.Append(@"\par ");
                }
                else if (line.StartsWith("> "))
                {
                    sb.Append(@"\pard\li720\sa40\cf3 ");
                    Inline(sb, line.Substring(2));
                    sb.Append(@"\cf2\par ");
                }
                else
                {
                    sb.Append(@"\pard\sa60 ");
                    Inline(sb, line);
                    sb.Append(@"\par ");
                }
            }
            sb.Append('}');
            return sb.ToString();
        }

        private static void Inline(StringBuilder sb, string s)
        {
            bool bold = false;
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (c == '*' && i + 1 < s.Length && s[i + 1] == '*')
                {
                    bold = !bold;
                    sb.Append(bold ? @"\b " : @"\b0 ");
                    i++;
                    continue;
                }
                if ((c == '_' || c == '^') && i + 1 < s.Length && s[i + 1] == '{')
                {
                    int end = s.IndexOf('}', i + 2);
                    if (end > 0)
                    {
                        sb.Append(c == '_' ? @"{\sub " : @"{\super ");
                        Escape(sb, s.Substring(i + 2, end - i - 2));
                        sb.Append('}');
                        i = end;
                        continue;
                    }
                }
                Escape(sb, c.ToString());
            }
            if (bold) sb.Append(@"\b0 ");
        }

        private static void Escape(StringBuilder sb, string s)
        {
            foreach (char c in s)
            {
                if (c == '\\' || c == '{' || c == '}') sb.Append('\\').Append(c);
                else if (c < 128) sb.Append(c);
                else sb.Append(@"\u").Append(((short)c).ToString(System.Globalization.CultureInfo.InvariantCulture)).Append('?');
            }
        }
    }
}
