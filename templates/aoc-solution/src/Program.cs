using AoC.Runner;
using System;
using System.Threading.Tasks;

namespace AoC.AoCTemplate;

public record Placeholder() {}

public static class Program
{
    public static async Task Main(string[] args) =>
        await Driver.RunSolver(0, Parse, SolvePart1, SolvePart2);

    public static Placeholder Parse(string[] lines)
        => throw new NotImplementedException();

    public static long? SolvePart1(Placeholder input)
        => throw new NotImplementedException();

    public static long? SolvePart2(Placeholder input)
        => throw new NotImplementedException();
}
