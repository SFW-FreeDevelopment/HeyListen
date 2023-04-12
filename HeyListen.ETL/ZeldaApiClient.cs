using System.Net;
using System.Text.Json;
using HeyListen.ETL.ZeldaApiModels;
using RestSharp;

namespace HeyListen.ETL;

public static class ZeldaApiClient
{
    private const string BaseUrl = "https://zelda.fanapis.com/api/";
    private const string Games = "games";

    private static readonly RestClient Client = new (BaseUrl);

    private static readonly JsonSerializerOptions SerializeOptions = new ()
    {
        PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance
    };

    public static async Task<List<Game>> GetGames()
    {
        var request = new RestRequest(Games);
        var response = await Client.ExecuteAsync(request);

        if (response.StatusCode != HttpStatusCode.OK || string.IsNullOrEmpty(response.Content))
        {
            throw new Exception("Error getting games!");
        }

        var data = JsonSerializer.Deserialize<ApiResponse<Game>>(response.Content, SerializeOptions);
        return data.Data;
    }
}