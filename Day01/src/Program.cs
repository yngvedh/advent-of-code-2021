using AoC.MoreLinq;
using AoC.Runner;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AoC.Day01;

public static class Program
{
    public static async Task Main(string[] args) =>
        await Driver.RunSolver(1, Parse, SolvePart1, SolvePart2);

    public static int[] Parse(string[] args)
        => args.Select(int.Parse).ToArray();

    public static long? SolvePart1(int[] input)
        => input.Windows(2).Count(p => p.Last() > p.First());

    public static long? SolvePart2(int[] input)
        => input.Windows(3).Windows(2).Count(p => p.Last().Sum() > p.First().Sum());
}
