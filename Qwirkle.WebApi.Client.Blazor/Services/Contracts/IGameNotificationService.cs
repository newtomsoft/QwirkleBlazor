namespace Qwirkle.WebApi.Client.Blazor.Services.Contracts;

public interface IGameNotificationService : IAsyncDisposable
{
    public void Initialize(Uri hubUri);
    Task Start();
    Task Stop();

    Task SendPlayerInGame(int gameId, int playerId);

    void SubscribePlayerIdTurn(Action<int> action);
    void SubscribeTurnSkipped(Action<int> action);
    void SubscribeTilesPlayed(Action<int, Move> action);
    void SubscribeTilesSwapped(Action<int> action);
    void SubscribeGameOver(Action<int> action);
    void SubscribePlayersInGame(Action<HashSet<int>> action);
}