namespace Qwirkle.WebApi.Client.Blazor.Services.Implementations;

class GameNotificationService : IGameNotificationService
{
    private HubConnection? _hubConnection;

    public void Initialize(Uri hubUri)
    {
        _hubConnection ??= new HubConnectionBuilder().WithUrl(hubUri).Build();
    }


    public async Task Start() => await _hubConnection!.StartAsync();

    public async Task SendPlayerInGame(int gameId, int playerId) => await _hubConnection!.SendAsync("PlayerInGame", gameId, playerId);

    public void SubscribeTilesPlayed(Action<int, Move> action) => _hubConnection!.On(INotification.ReceiveTilesPlayed, action);
    public void SubscribeTilesSwapped(Action<int> action) => _hubConnection!.On(INotification.ReceiveTilesSwapped, action);
    public void SubscribeTurnSkipped(Action<int> action) => _hubConnection!.On(INotification.ReceiveTurnSkipped, action);
    public void SubscribePlayerIdTurn(Action<int> action) => _hubConnection!.On(INotification.ReceivePlayerIdTurn, action);
    public void SubscribeGameOver(Action<int> action) => _hubConnection!.On(INotification.ReceiveGameOver, action);

    public async ValueTask DisposeAsync()
    {
        if (_hubConnection is not null) await _hubConnection.DisposeAsync();
    }

}