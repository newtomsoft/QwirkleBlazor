namespace Qwirkle.WebApi.Client.Blazor.Services.Implementations.WebApis;

public class WebApiAction : WebApiBase, IApiAction
{
    private const string ControllerName = "Action";

    public WebApiAction(HttpClient httpClient) : base(httpClient) { }

    public async Task<PlayReturn> PlayTiles(List<TileModel> tiles)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/{ControllerName}/PlayTiles", tiles);
        if (response.StatusCode == HttpStatusCode.BadRequest) throw new Exception(await response.Content.ReadAsStringAsync());
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<PlayReturn>();
    }

    public Task PlayTilesSimulation(List<TileModel> tiles)
    {
        throw new NotImplementedException();
    }

    public async Task<SwapTilesReturn> SwapTiles(List<TileModel> tiles)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/{ControllerName}/SwapTiles", tiles);
        if (response.StatusCode == HttpStatusCode.BadRequest) throw new Exception(await response.Content.ReadAsStringAsync());
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<SwapTilesReturn>();
    }

    public async Task<SkipTurnReturn> SkipTurn(SkipTurnModel skipTurnModel)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/{ControllerName}/SkipTurn", skipTurnModel);
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