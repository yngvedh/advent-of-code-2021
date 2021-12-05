using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AoC.Runner;
 
public record AoCClientConfig(string AoCSession);


public class AoCClient
{
    private readonly ILogger _logger;
    private readonly HttpClient _client;

    public AoCClient(HttpClient client, ILogger logger)
    {
        _logger = logger;
        _client = client;
    }

    public async Task<string[]> GetPuzzleInput(int day)
    {
        var path = $"./input_day{day}.txt";
        if (File.Exists(path))
        {
            _logger.LogInformation("Fetching from file.");
            return await File.ReadAllLinesAsync(path);
        }
        else
        {
            _logger.LogInformation("Fetching from adventofcode.com");
            var lines = await DoGetPuzzleInput(day);
            File.WriteAllLines(path, lines);
            return lines;
        }
    }

    public async Task<string[]> DoGetPuzzleInput(int day)
    {
        var inputUrl = $"https://adventofcode.com/2020/day/{day}/input";
        var response = await _client.GetAsync(inputUrl);
        var content = await response.Content.ReadAsStringAsync();
        return content.Split(new string[] {"\r\n", "\n"}, StringSplitOptions.None);
    }
}

public class AoCHttpClientHandler : HttpClientHandler
{
    public AoCHttpClientHandler(AoCClientConfig config, ILogger logger)
    {
        logger.LogInformation("Using adventofcode.com session: {Session}", config.AoCSession);

        CookieContainer = new CookieContainer().WithSession(config.AoCSession);
    }
}

public static class AoCClientConfigurationExtensions
{
    public static IServiceCollection AddAoCClientConfig(
        this IServiceCollection services,
        IConfigurationRoot config)
    {
        services.AddSingleton(new AoCClientConfig(config["SESSION"]));

        return services;
    }

    public static CookieContainer WithSession(this CookieContainer c, string session)
    {
        c.Add(new Uri(@"https://adventofcode.com"), new Cookie("session", session));
        return c;
    }

    public static IServiceCollection AddAoCClient(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton<AoCHttpClientHandler>()
            .AddHttpClient<AoCClient>()
                .ConfigurePrimaryHttpMessageHandler<AoCHttpClientHandler>();

        return serviceCollection;
    }
}
