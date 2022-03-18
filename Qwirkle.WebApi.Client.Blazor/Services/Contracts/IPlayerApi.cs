namespace Qwirkle.WebApi.Client.Blazor.Services.Contracts;

public interface IPlayerApi
{
    Task<Player> GetByGameId(int gameId);
}