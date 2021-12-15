using AoC.Runner;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AoC.Day06;

public record Fishes(long[] Histogram)
{
    public long Count { get => Histogram.Sum(); }
}

public static class Program
{
    public static async Task Main(string[] args) =>
        await Driver.RunSolver(6, Parse, SolvePart1, SolvePart2);

    public static Fishes Parse(string[] lines)
        => new Fishes(lines[0].Split(",").Select(int.Parse)
            .Aggregate(new long[9], (hist, n) => { hist[n]++; return hist; }));

    public static Fishes StepSimulation(Fishes f) {
        var spawners = f.Histogram[0];
        var hist = f.Histogram.Skip(1).Append(spawners).ToArray();
        hist[6] += spawners;
        return new Fishes(hist);
    }

    public static Fishes RunSimulation(int n, Fishes f)
        => n == 0 ? f : RunSimulation(n-1, StepSimulation(f));

    public static long? SolvePart1(Fishes input)
        => RunSimulation(80, input).Count;

    public static long? SolvePart2(Fishes input)
        => RunSimulation(256, input).Count;
}
