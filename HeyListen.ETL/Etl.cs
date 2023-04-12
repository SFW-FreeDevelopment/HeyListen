using HeyListen.Database;
using HeyListen.Database.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace HeyListen.ETL;

public static class Etl
{
    private const string SelectionMessage = "Select a resource to import:\n\n1 - Games\n0 - Quit";

    private static MongoClient _client;
    private static GameRepository _gameRepository = new (_client);
    
    public static async Task Run()
    {
        _client = CreateClient();
        _gameRepository = new GameRepository(_client);
        while (true)
        {
            Console.WriteLine(SelectionMessage);
            var input = Console.ReadKey();
            Console.WriteLine(Environment.NewLine);

            switch (input.Key)
            {
                case ConsoleKey.D0:
                    Console.WriteLine("Exiting...");
                    return;
                
                case ConsoleKey.D1:
                    Console.WriteLine("Importing games\n\n");
                    await DoGamesImport();
                    Console.WriteLine("Games import finished\n\n");
                    break;
                
                default:
                    Console.WriteLine("Not a valid input!\n\n");
                    break;
            }
        }
    }

    private static MongoClient CreateClient()
    {
        var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", false, true)
        .Build();
        
        return new MongoClient(MongoClientSettings.FromConnectionString(configuration["MongoDatabaseConnectionString"]));
    }

    #region Games

    private static async Task DoGamesImport()
    {
        var apiGames = await ZeldaApiClient.GetGames();

        if (apiGames is null || apiGames.Count == 0)
        {
            Console.WriteLine("Error getting games from API!\n\n");
            return;
        }

        foreach (var gameToCreate in apiGames.Select(CreateGameResourceFromApiModel))
        {
            var game = await _gameRepository.Create(gameToCreate);

            if (game is null)
            {
                Console.WriteLine("Game not imported.\n");
                continue;
            }
            
            Console.WriteLine("Game successfully imported.\n");
        }
    }

    private static Game CreateGameResourceFromApiModel(ZeldaApiModels.Game apiGame)
    {
        if (apiGame is null)
        {
            return null;
        }

        return new Game
        {
            Name = apiGame.Name,
            Description = apiGame.Description,
            Developer = apiGame.Developer,
            Publisher = apiGame.Publisher,
            ReleaseDates = new List<ReleaseDate>
            {
                new()
                {
                    Date = GetDateTimeFromReleaseDate(apiGame.ReleasedDate),
                    Region = "Japan"
                }
            }
        };
    }

    private static DateTime GetDateTimeFromReleaseDate(string releaseDate)
    {
        if (string.IsNullOrEmpty(releaseDate))
        {
            return new DateTime();
        }
        
        if (releaseDate[0] == ' ')
        {
            releaseDate = releaseDate.Remove(0, 1);
        }

        var dateTimeParsed = DateTime.TryParse(releaseDate, out var parsedDateTime);

        return dateTimeParsed ? parsedDateTime : new DateTime();
    }
    
    #endregion
}