namespace Qwirkle.WebApi.Client.Blazor.Services.Implementations;

public class GameApi : IGameApi
{
    private readonly HttpClient _httpClient;
    private const string ControllerName = "Game";

    public GameApi(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<int>> GetUserGamesIds()
    {
        var response = await _httpClient.GetAsync($"{ControllerName}/UserGamesIds");
        if (response.StatusCode == HttpStatusCode.BadRequest) throw new Exception(await response.Content.ReadAsStringAsync());
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<int>>();
    }

    public async Task<Game> GetUserGame(int gameId)
    {
        var response = await _httpClient.GetAsync($"{ControllerName}/{gameId}");
        if (response.StatusCode == HttpStatusCode.BadRequest) throw new Exception(await response.Content.ReadAsStringAsync());
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Game>();
    }
}