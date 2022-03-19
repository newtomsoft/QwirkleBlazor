namespace Qwirkle.WebApi.Client.Blazor.Services.Implementations;

public class GameApi : BaseApi, IGameApi
{
    private const string ControllerName = "Game";

    public GameApi(HttpClient httpClient) : base(httpClient) { }

    public async Task<List<int>> GetUserGamesIds()
    {
        var response = await _httpClient.GetAsync($"api/{ControllerName}/UserGamesIds");
        if (response.StatusCode == HttpStatusCode.BadRequest) throw new Exception(await response.Content.ReadAsStringAsync());
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<int>>();
    }

    public async Task<Game> GetGame(int gameId)
    {
        var response = await _httpClient.GetAsync($"api/{ControllerName}/{gameId}");
        if (response.StatusCode == HttpStatusCode.BadRequest) throw new Exception(await response.Content.ReadAsStringAsync());
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Game>();
    }
}