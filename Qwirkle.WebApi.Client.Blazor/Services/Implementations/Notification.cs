namespace Qwirkle.WebApi.Client.Blazor.Services.Implementations;

public class Notification : INotification
{
    private HubConnection? _hubConnection;

    public void CreateHub()
    {
        _hubConnection ??= new HubConnectionBuilder().WithUrl("https://localhost:5001/hubGame").Build(); //TODO address with NavigationManager
    }

    public async Task SendUserWaitingInstantGame(int playersNumber, string userName)
    {
        await _hubConnection!.SendAsync("UserWaitingInstantGame", playersNumber, userName);
    }

    public async Task Subscribe(Action<int, int> receiveInstantGameStarted, Action<string> receiveInstantGameExpected)
    {
        _hubConnection!.On<string>("ReceiveInstantGameExpected", receiveInstantGameExpected);
        _hubConnection!.On<int, int>("ReceiveInstantGameStarted", receiveInstantGameStarted);
        await _hubConnection!.StartAsync();
    }
}