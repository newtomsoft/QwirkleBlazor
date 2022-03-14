namespace Qwirkle.WebApi.Client.Blazor.Services.Contracts;

public interface IActionApi
{
    Task PlayTiles(List<TileModel> tiles);
    Task PlayTilesSimulation(List<TileModel> tiles);
    Task SwapTiles(List<TileModel> tiles);
    Task SkipTurn(SkipTurnModel skipTurnViewModel);
    Task ArrangeRack(List<TileModel> tiles);
    Task<UserInfo?> GetUserInfo();
}