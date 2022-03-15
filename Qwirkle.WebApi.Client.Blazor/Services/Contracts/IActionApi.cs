namespace Qwirkle.WebApi.Client.Blazor.Services.Contracts;

public interface IActionApi
{
    Task<PlayReturn> PlayTiles(List<TileModel> tiles);
    Task PlayTilesSimulation(List<TileModel> tiles);
    Task SwapTiles(List<TileModel> tiles);
    Task<SkipTurnReturn> SkipTurn(SkipTurnModel skipTurnModel);
    Task ArrangeRack(List<TileModel> tiles);
    Task<UserInfo?> GetUserInfo();
}