using AoC.Runner;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AoC.Day03;

public record Reading(int[] Numbers, int BitCount);

public static class Program
{
    public static async Task Main(string[] args) =>
        await Driver.RunSolver(3, Parse, SolvePart1, SolvePart2);

    public static Reading Parse(string[] lines)
        => new Reading(lines.Select(ParseBinary).ToArray(), lines.Select(_ => _.Length).Max());

    public static int ParseBinary(string l) => l.Aggregate(0, (acc, c) => acc*2 + (int)(c - '0'));

    public static int CommonBit(Reading r, int bit)
    {
        var mask = 1 << bit;

        var bits = r.Numbers.Count(n => (n & mask) != 0);

        return bits*2 >= r.Numbers.Length ? mask : 0;
    }

    public static int Mask(Reading r, int n) => n & ((1 << r.BitCount) - 1);

    public static int GammaRate(Reading r) =>
        Enumerable
            .Range(0, r.BitCount)
            .Select(bit => CommonBit(r, bit))
            .Aggregate((x,y) => x | y);

    public static int EpsilonRate(Reading r) => Mask(r, ~GammaRate(r));

    public static long? SolvePart1(Reading r)
        => (long)GammaRate(r)*EpsilonRate(r);


    public static int FindRating(Reading r, Func<int,int,bool> pred)
    {
        int Rating(Reading r, int bit)
        {
            if (bit < 0) throw new ArgumentOutOfRangeException("bit is negative.");
            var m = CommonBit(r, bit);
            var r_ = r with { Numbers = r.Numbers.Where(_ => pred(_ & (1 << bit), m)).ToArray() };
            return r_.Numbers.Length == 1 ? r_.Numbers[0] :  Rating(r_, bit-1);
        }
        return Rating(r, r.BitCount-1);
    }


    public static int OxygenRating(Reading r) => FindRating(r, (a,b) => a == b);
    public static int ScrubberRating(Reading r) => FindRating(r, (a,b) => a != b);

    public static long? SolvePart2(Reading r)
        => (long)OxygenRating(r)*ScrubberRating(r);
}
