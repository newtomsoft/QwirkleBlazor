namespace Qwirkle.WebApi.Client.Blazor.Services.Implementations;

public class ActionApi : IActionApi
{
    private readonly HttpClient _httpClient;
    private const string ControllerName = "Action";
    public ActionApi(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PlayReturn> PlayTiles(List<TileModel> tiles)
    {
        var response = await _httpClient.PostAsJsonAsync($"{ControllerName}/PlayTiles", tiles);
        if (response.StatusCode == HttpStatusCode.BadRequest) throw new Exception(await response.Content.ReadAsStringAsync());
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<PlayReturn>();
    }

    public Task PlayTilesSimulation(List<TileModel> tiles)
    {
        throw new NotImplementedException();
    }

    public Task SwapTiles(List<TileModel> tiles)
    {
        throw new NotImplementedException();
    }

    public async Task<SkipTurnReturn> SkipTurn(SkipTurnModel skipTurnModel)
    {
        var response = await _httpClient.PostAsJsonAsync($"{ControllerName}/SkipTurn", skipTurnModel);
        if (response.StatusCode == HttpStatusCode.BadRequest) throw new Exception(await response.Content.ReadAsStringAsync());
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<SkipTurnReturn>();
    }

    public Task ArrangeRack(List<TileModel> tiles)
    {
        throw new NotImplementedException();
    }

    public Task<UserInfo?> GetUserInfo()
    {
        throw new NotImplementedException();
    }
}