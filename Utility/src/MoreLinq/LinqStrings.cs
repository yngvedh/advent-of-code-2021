using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC.MoreLinq
{

    public static class LinqStrings
    {
        public static IEnumerable<string> Paragraphs(this IEnumerable<string> lines, string separator = "\n")
        {
            string Joiner(IEnumerable<string> s) => string.Join(separator, s);
            return lines.ParagraphsAsLines().Select(Joiner);
        }

        public static IEnumerable<IEnumerable<string>> ParagraphsAsLines(this IEnumerable<string> lines)
        {
            bool IsParagraphLine(string s) => !string.IsNullOrEmpty(s);
            Func<string,bool> IsSeparatorLine = string.IsNullOrEmpty;
            var remaining = lines;

            while (remaining.Any())
            {
                var paragraphLines = remaining.TakeWhile(IsParagraphLine);
                remaining = remaining.SkipWhile(IsParagraphLine).SkipWhile(IsSeparatorLine);
                yield return paragraphLines;
            }
        }
    }

}
