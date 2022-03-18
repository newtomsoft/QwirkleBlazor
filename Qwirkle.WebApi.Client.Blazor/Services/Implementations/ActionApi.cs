namespace Qwirkle.WebApi.Client.Blazor.Services.Implementations;

public class ActionApi : BaseApi, IActionApi
{
    protected override string ControllerName => "Action";

    public ActionApi(HttpClient httpClient) : base(httpClient) { }

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