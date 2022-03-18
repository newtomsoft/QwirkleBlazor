namespace Qwirkle.WebApi.Client.Blazor.Services.Contracts;

public interface IGameApi
{
    Task<List<int>> GetUserGamesIds();
    Task<Game> GetGame(int gameId);
}