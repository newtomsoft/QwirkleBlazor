namespace Qwirkle.WebApi.Client.Blazor.Services.Contracts.Apis;

public interface IApiAction
{
    Task<PlayReturn> PlayTiles(List<TileModel> tiles);
    Task PlayTilesSimulation(List<TileModel> tiles);
    Task<SwapTilesReturn> SwapTiles(List<TileModel> tiles);
    Task<SkipTurnReturn> SkipTurn(SkipTurnModel skipTurnModel);
    Task ArrangeRack(List<TileModel> tiles);
    Task<UserInfo?> GetUserInfo();
}