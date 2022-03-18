namespace Qwirkle.WebApi.Client.Blazor.Services.Contracts;

public class NoGameNotification : IGameNotificationService
{
    public ValueTask DisposeAsync() => default;
    public void Initialize(Uri hubUri) { }
    public Task SendPlayerInGame(int gameId, int playerId) => default!;
    public void SubscribePlayerIdTurn(Action<int> action) { }
    public void SubscribeTurnSkipped(Action<int> action) { }
    public void SubscribeTilesPlayed(Action<int, Move> action) { }
    public void SubscribeTilesSwapped(Action<int> action) { }
    public void SubscribeGameOver(Action<int> action) { }
    public void SubscribePlayersInGame(Action<HashSet<int>> action) { }


    public Task Start() => new(() => Console.WriteLine("Nothing"));
    public Task Stop() => default!;
}