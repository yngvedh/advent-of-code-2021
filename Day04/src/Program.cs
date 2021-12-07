using AoC.Runner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AoC.Day04;

public record Board(int[][] Rows);

public record BingoGame(int[] Numbers, Board[] Boards);

public static class Program
{
    public static async Task Main(string[] args) =>
        await Driver.RunSolver(4, Parse, SolvePart1, SolvePart2);

    public static BingoGame Parse(string[] lines)
        => new BingoGame(ParseNumbers(lines[0]), ParseBoards(lines.Skip(1)));

    public static long? SolvePart1(BingoGame input)
    {
        var (winningBoard, drawnNumbers) = FindWinningBoard(input);
        return Answer(winningBoard, drawnNumbers);
    }

    public static long? SolvePart2(BingoGame input)
    {
        var (losingBoard, drawnNumbers) = FindLosingBoard(input);
        return Answer(losingBoard, drawnNumbers);
    }


    public static int[] ParseNumbers(string line) =>
        line.Split(',').Select(int.Parse).ToArray();

    public static Board[] ParseBoards(IEnumerable<string> lines) =>
        lines.Chunk(5).Select(ParseBoard).ToArray();

    public static Board ParseBoard(IEnumerable<string> lines) =>
        new Board(lines.Select(ParseRow).ToArray());
    
    public static int[] ParseRow(string line) =>
        line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();


    public static (Board WinningBoard, List<int> DrawnNumbers) FindWinningBoard(BingoGame game)
    {
        var referees = game.Boards.Select(_ => new BoardReferee(_)).ToArray();
        var drawn = new List<int>();

        foreach(var n in game.Numbers)
        {
            drawn.Add(n);
            foreach(var r in referees)
            {
                if (r.RegisterNumber(n))
                    return (r.Board, drawn);
            }
        }

        throw new ArgumentException("No winners in this game of bingo sadly.");
    }

    public static (Board WinningBoard, List<int> DrawnNumbers) FindLosingBoard(BingoGame game)
    {
        var referees = game.Boards.Select(_ => new BoardReferee(_)).ToArray();
        var drawn = new List<int>();

        foreach(var n in game.Numbers)
        {
            drawn.Add(n);
            foreach(var r in referees)
                r.RegisterNumber(n);

            if (referees.All(_ => _.HasBingo))
                return (referees.First().Board, drawn);

            referees = referees.Where(_ => !_.HasBingo).ToArray();
        }

        throw new ArgumentException("No single worst loser in this game of bingo sadly.");
    }

    public static long Answer(Board board, List<int> drawnNumbers) => drawnNumbers.Last()
            * board.Rows.SelectMany(_ => _)
                .Where(_ => !drawnNumbers.Contains(_))
                .Sum();

}

public class BoardReferee
{
    private readonly List<int>[] Counters;

    public readonly Board Board;
    public bool HasBingo { get => Counters.Any(_ => _.Count == 0); }

    public BoardReferee(Board board)
    {
        Board = board;
        Counters = GetRowCounters(board).Concat(GetColumnCounters(board)).ToArray()
            ?? throw new NullReferenceException("Counters initialization resultet in null!");;
    }

    public bool RegisterNumber(int n)
    {
        foreach(var c in Counters)
            c.Remove(n);

        return HasBingo;
    }

    private IEnumerable<List<int>> GetRowCounters(Board b) => b.Rows.Select(_ => _.ToList());
    private IEnumerable<List<int>> GetColumnCounters(Board b) => Enumerable.Range(0, 5)
        .Select(i => b.Rows.Select(_ => _[i]).ToList());
}
