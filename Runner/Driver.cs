using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AoC.Runner;

public static class Driver
{
    public static async Task RunSolver<T>(
        int day,
        Func<string[], T> parser,
        Func<T, long?> part1Solver, Func<T, long?> part2Solver)
            where T : notnull
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("config.json")
            .AddEnvironmentVariables("AOC_")
            .Build()!;

        var logger = LoggerFactory.Create(_ => _.AddSimpleConsole(o => {
                o.IncludeScopes = false;
                o.SingleLine = true;
            }))
            .CreateLogger("AoC.Runner");

        var services = new ServiceCollection();

        services
            .AddSingleton<ILogger>(logger)
            .AddAoCClientConfig(config)
            .AddLogging()
            .AddAoCClient()
            .AddSingleton<Solver<T>>();

        var serviceProvider = services.BuildServiceProvider();
        
        var driver = serviceProvider.GetRequiredService<Solver<T>>();

        await driver.Run(day, parser, part1Solver, part2Solver);
    }
}

public class Solver<T> where T : notnull
{
    private readonly AoCClient _client;
    private readonly ILogger _logger;

    public Solver(AoCClient client, ILogger logger)
    {
        _logger = logger;
        _client = client;
    }

    public async Task Run(int day, Func<string[], T> parser, Func<T, long?> part1Solver, Func<T, long?> part2Solver)
    {
        _logger.LogInformation($"Running: Day {day}");

        var lines = await _client.GetPuzzleInput(day);
        var input = parser(lines);

        var solution1 = part1Solver(input);
        WriteSolution(1, solution1);

        var solution2 = part2Solver(input);
        WriteSolution(1, solution2);
    }

    private void WriteSolution(int part, long? res)
    {
        if (res.HasValue)
            _logger.LogInformation($"Part {part} - {res.Value}");
        else
            _logger.LogWarning($"Part {part} - no solution found.");

    }
}
