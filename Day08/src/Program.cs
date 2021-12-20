using AoC.Runner;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace AoC.Day08;

public record Display(string[] Observations, string[] Reading);

public record Panel(Display[] Displays);

public static class Program
{
    public static async Task Main(string[] args) =>
        await Driver.RunSolver(8, Parse, SolvePart1, SolvePart2);

    public static Panel Parse(string[] lines)
        => new Panel(lines.Select(ParseDisplay).ToArray());

    public static Display ParseDisplay(string line)
    {
        var split = line.Split(" | ");
        return new Display(split[0].Split(" "), split[1].Split(" "));
    }

    private static readonly int[] UniqueLengths = new int[] {2, 3, 4, 7};

    public static long? SolvePart1(Panel panel)
        => panel.Displays.Select(_ => _.Reading.Count(d => UniqueLengths.Contains(d.Length))).Sum();

    public static long? SolvePart2(Panel panel)
        => panel.Displays.Select(DeriveValue).Sum();

    public static long DeriveValue(Display d)
    {
        var map = DeriveDigitMapping(d);
        var value = new string(d.Reading.Select(_ => map[_]).ToArray());
        return long.Parse(value);
    }

    public class DigitComparer : IEqualityComparer<string>
    {
        public bool Equals(string? x, string? y) =>
            ReferenceEquals(x, y) ||
            (x != null && y != null) &&
            (x.Equals(y) || Sorted(x).Equals(Sorted(y)));

        public int GetHashCode([DisallowNull] string obj) => Sorted(obj).GetHashCode();
        private static string Sorted(string s) => new string(s.OrderBy(_ => _).ToArray());
    }

    public static Dictionary<string,char> DeriveDigitMapping(Display d)
    {
        // unique lengths
        var s1 = d.Observations.Single(_ => _.Length == 2);
        var s4 = d.Observations.Single(_ => _.Length == 4);        
        var s7 = d.Observations.Single(_ => _.Length == 3);
        var s8 = d.Observations.Single(_ => _.Length == 7);

        // 6 is the only 6 segment digit which does not contain all segments of 1
        var s6 = d.Observations.Single(_ => _.Length == 6 && s1.Any(s => !_.Contains(s)));

        // 9 is the only 6 segment digit which contains all segments of 4
        var s9 = d.Observations.Single(_ => _.Length == 6 && s4.All(s => _.Contains(s)));

        // 0 is the last 6 segment digit
        var s0 = d.Observations.Single(_ => _.Length == 6 && _ != s6 && _ != s9);

        // 3 is the only 5 segment digit which contains all segments of 1
        var s3 = d.Observations.Single(_ => _.Length == 5 && s1.All(s => _.Contains(s)));

        // 5 is the only 5 segment digit of which all segments is contained int 9
        var s2 = d.Observations.Single(_ => _.Length == 5 && _.Any(s => !s9.Contains(s)));

        // 2 is the last 5 segment digit
        var s5 = d.Observations.Single(_ => _.Length == 5 && _ != s3 && _ != s2);

        return new Dictionary<string, char>(new DigitComparer())
        {
            {s1, '1'}, {s2, '2'}, {s3, '3'}, {s4, '4'}, {s5, '5'}, {s6, '6'}, {s7, '7'}, {s8, '8'}, {s9, '9'}, {s0, '0'},
        };
    }
}
