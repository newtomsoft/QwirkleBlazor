namespace Qwirkle.WebApi.Client.Blazor.Tests.Arrange;

public class NoNotificationGame : INotificationGame
{
    public ValueTask DisposeAsync() => default;
    public void Initialize(Uri hubUri) { }
    public Task SendPlayerInGame(int gameId, int playerId) => Task.CompletedTask;
    public void SubscribePlayerIdTurn(Action<int> action) { }
    public void SubscribeTurnSkipped(Action<int> action) { }
    public void SubscribeTilesPlayed(Action<int, Move> action) { }
    public void SubscribeTilesSwapped(Action<int> action) { }
    public void SubscribeGameOver(Action<int> action) { }
    public void SubscribePlayersInGame(Action<HashSet<int>> action) { }
    public Task Start() => Task.CompletedTask;
    public Task Stop() => Task.CompletedTask;
}