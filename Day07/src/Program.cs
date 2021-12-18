using AoC.Runner;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AoC.Day07;

public record Crabs(int[] Positions);

public static class Program
{
    public static async Task Main(string[] args) =>
        await Driver.RunSolver(7, Parse, SolvePart1, SolvePart2);

    public static Crabs Parse(string[] lines)
        => new Crabs(lines[0].Split(',').Select(int.Parse).ToArray());

    public static long? SolvePart1(Crabs input)
    {
        var pos = input.Positions.OrderBy(_ => _).ElementAt(input.Positions.Length/2);
        var fuel = input.Positions.Select(_ => Math.Abs(_ - pos)).Sum();
        return fuel;
    }

    public static long? SolvePart2(Crabs input)
    {
        var min = input.Positions.Min();
        var max = input.Positions.Max();

        return Enumerable.Range(min, max-min+1).Select(p => FuelConsumption(input, p)).Min();
    }



    public static long FuelConsumption(Crabs input, int targetPos)
        => input.Positions.Select(_ => FuelConsumption(Math.Abs(_ - targetPos))).Sum();

    public static long FuelConsumption(int distance) => (distance*distance+distance)/2;
}
