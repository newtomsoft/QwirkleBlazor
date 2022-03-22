namespace Qwirkle.WebApi.Client.Blazor.Services.Implementations.WebApis;

public class WebApiGame : WebApiBase, IApiGame
{
    private const string ControllerName = "Game";

    public WebApiGame(HttpClient httpClient) : base(httpClient) { }

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

    public async Task<int> CreateGame(List<string> usersNames)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/{ControllerName}/New", usersNames);
        if (response.StatusCode == HttpStatusCode.BadRequest) throw new Exception(await response.Content.ReadAsStringAsync());
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<int>();
    }
}