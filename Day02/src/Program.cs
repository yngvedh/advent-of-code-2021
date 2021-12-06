using AoC.Runner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoC.Day02;

public enum Direction { Forward, Down, Up }
public record Command(Direction Dir, int Distance);

public interface IMovable<T>
{
    public long Answer();
    
    public T Move(Command c);
    public T Move(IEnumerable<Command> cs);
}

public static class Movable
{
    public static S Move<S>(S s, IEnumerable<Command> cs) where S : IMovable<S>
        => cs.Aggregate(s, (s,c) => s.Move(c));
}

public record Submarine(int Depth, int X) : IMovable<Submarine>
{
    public static Submarine Start => new Submarine(0, 0);

    public Submarine Move(Command c) => c.Dir switch {
        Direction.Forward => this with {X = X + c.Distance},
        Direction.Down => this with {Depth = Depth + c.Distance},
        Direction.Up => this with {Depth = Depth - c.Distance},
        _ => throw new ArgumentOutOfRangeException($"Command contains unrecognized direction '{c.Dir}'.")
    };

    public Submarine Move(IEnumerable<Command> cs) => Movable.Move(this, cs);

    public long Answer() => (long)Depth*X;
}

public record AimSub(int Depth, int X, int Aim) : IMovable<AimSub>
{
    public static AimSub Start => new AimSub(0, 0, 0);

    public AimSub Move(IEnumerable<Command> cs) => Movable.Move(this, cs);
    public AimSub Move(Command c) => c switch
    {
        (Direction.Forward, var n) => this with { X = X+n, Depth = Depth + Aim*n },
        (Direction.Up, var n) => this with { Aim = Aim - n },
        (Direction.Down, var n) => this with { Aim = Aim + n},
        _ => throw new ArgumentOutOfRangeException($"Command contains unrecognized direction '{c.Dir}'.")
    };

    public long Answer() => (long)Depth*X;
}

public static class Program
{
    public static async Task Main(string[] args) =>
        await Driver.RunSolver(2, Parse, SolvePart1, SolvePart2);

    public static Command[] Parse(string[] lines)
        => lines.Select(ParseCommand).ToArray();

    public static Command ParseCommand(string line)
    {
        Regex r = new(@"^(forward|up|down) (\d+)$");

        var match = r.Match(line);
        if (!match.Success)
            throw new ArgumentOutOfRangeException($"'{line}' is not a recognized command");

        return new Command(
            ParseDirection(match.Groups[1].Value),
            int.Parse(match.Groups[2].Value));
    }

    public static Direction ParseDirection(string t) => t switch
    {
        "forward" => Direction.Forward,
        "up" => Direction.Up,
        "down" => Direction.Down,
        _ => throw new ArgumentOutOfRangeException($"'{t}' is not a recognized direction")
    };

    public static long? SolvePart1(Command[] input)
        => Submarine.Start.Move(input).Answer();

    public static long? SolvePart2(Command[] input)
        => AimSub.Start.Move(input).Answer();
}
