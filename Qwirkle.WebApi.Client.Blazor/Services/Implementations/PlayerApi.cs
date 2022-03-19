namespace Qwirkle.WebApi.Client.Blazor.Services.Implementations;

public class PlayerApi : BaseApi, IPlayerApi
{
    private const string ControllerName = "Player";

    public PlayerApi(HttpClient httpClient) : base(httpClient) { }

    public async Task<Player> GetByGameId(int gameId)
    {
        var response = await _httpClient.GetAsync($"api/{ControllerName}/ByGameId/{gameId}");
        if (response.StatusCode == HttpStatusCode.BadRequest) throw new Exception(await response.Content.ReadAsStringAsync());
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Player>();
    }
}