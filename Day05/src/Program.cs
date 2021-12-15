using AoC.MoreLinq;
using AoC.Runner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC.Day05;

public record Point(int X, int Y);
public record Line(Point A, Point B);
public record VentMap(Line[] Lines);

public static class Program
{
    public static async Task Main(string[] args) =>
        await Driver.RunSolver(5, Parse, SolvePart1, SolvePart2);

    public static VentMap Parse(string[] lines)
        => ParseMap(lines);

    public static long? SolvePart1(VentMap input)
        => input.Lines
            .Where(_ => IsVertical(_) || IsHorizontal(_))
            .SelectMany(ToPoints)
            .GroupBy(_ => _, _ => _, (p, ps) => (p, ps.Count()))
            .Where(_ => _.Item2 > 1)
            .Count();

    public static long? SolvePart2(VentMap input)
        => input.Lines
            .SelectMany(ToPoints)
            .GroupBy(_ => _, _ => _, (p, ps) => (p, ps.Count()))
            .Where(_ => _.Item2 > 1)
            .Count();


    private static VentMap ParseMap(string[] lines)
        => new VentMap(lines.Select(ParseLine).ToArray());

    public static T MapMatch<T>(this Regex r, string s, Func<string[], T> m)
    {
        var match = r.Match(s);

        if (!match.Success)
            throw new ArgumentOutOfRangeException($"Unable to match string from '{s}' with r:'{r.ToString()}'");

        return m(match.Groups.Values.Select(_ => _.Value).ToArray());
    }

    private static Regex LinePattern = new (@"(\d+),(\d+) -> (\d+),(\d+)");
    public static Line ParseLine(string line)
        => LinePattern.MapMatch(line, g =>
            new Line(
                new Point(int.Parse(g[1]), int.Parse(g[2])),
                new Point(int.Parse(g[3]), int.Parse(g[4]))));

    public static bool IsHorizontal(Line l) => l.A.X == l.B.X;
    public static bool IsVertical(Line l) => l.A.Y == l.B.Y;

    public static IEnumerable<Point> ToPoints(Line l)
    {
        if (Math.Abs(l.B.X - l.A.X) < Math.Abs(l.B.Y - l.A.Y))
        {
            // bx-ax < by-ay => angle > 45 => iterate over f-1(y),y
            
            // x = (y-y0)*dx/dy + x0
            int G(int y) => (y - l.A.Y)*(l.B.X-l.A.X)/(l.B.Y-l.A.Y) + l.A.X;
            return Enumerable
                .Range(Math.Min(l.A.Y, l.B.Y), Math.Abs(l.B.Y - l.A.Y) + 1)
                .Select(y => new Point(G(y), y));
        }
        else
        {
            // angle <= 45 => iterate over x,f(x)

            // y = (x-x0)*dy/dx + y0
            int F(int x) => (x - l.A.X)*(l.B.Y-l.A.Y)/(l.B.X-l.A.X) + l.A.Y;
            return Enumerable
                .Range(Math.Min(l.A.X, l.B.X), Math.Abs(l.B.X - l.A.X) + 1)
                .Select(x => new Point(x, F(x)));
        }
    }
}
