namespace Qwirkle.WebApi.Client.Blazor.Services.Implementations;

class ActionApi : IActionApi
{
    private readonly HttpClient _httpClient;
    private const string Controller = "Action";
    public ActionApi(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    public async Task PlayTiles(List<TileModel> tiles)
    {
        var result = await _httpClient.PostAsJsonAsync($"{Controller}/PlayTiles", tiles);
        if (result.StatusCode == HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
        result.EnsureSuccessStatusCode();
    }

    public Task PlayTilesSimulation(List<TileModel> tiles)
    {
        throw new NotImplementedException();
    }

    public Task SwapTiles(List<TileModel> tiles)
    {
        throw new NotImplementedException();
    }

    public Task SkipTurn(SkipTurnModel skipTurnViewModel)
    {
        throw new NotImplementedException();
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