namespace AoC.Runner
{
    public interface ISolver<TInput,TResult> where TResult: struct
    {
        int Day { get; }
        string Name { get => $"Day {Day}"; }
        string DescribePart1(TResult i) => i.ToString()!;
        string DescribePart2(TResult i) => i.ToString()!;
        TInput Parse(string[] input);
        TResult? SolvePart1(TInput input);
        TResult? SolvePart2(TInput input);
    }
}
