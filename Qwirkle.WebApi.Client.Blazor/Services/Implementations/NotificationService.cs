﻿namespace Qwirkle.WebApi.Client.Blazor.Services.Implementations;

public class NotificationService : INotificationService
{
    private HubConnection? _hubConnection;

    public void Initialize(Uri hubUri)
    {
        _hubConnection ??= new HubConnectionBuilder().WithUrl(hubUri).Build(); //TODO address with NavigationManager
    }

    public async Task SendUserWaitingInstantGame(int playersNumber, string userName) => await _hubConnection!.SendAsync("UserWaitingInstantGame", playersNumber, userName);

    public void SubscribeInstantGameStarted(Action<int, int> receiveInstantGameStarted) => _hubConnection!.On(INotification.ReceiveInstantGameStarted, receiveInstantGameStarted);

    public void SubscribeInstantGameJoined(Action<string> receiveInstantJoined) => _hubConnection!.On(INotification.ReceiveInstantGameJoined, receiveInstantJoined);

    public async Task Start() => await _hubConnection!.StartAsync();

    public async ValueTask DisposeAsync()
    {
        if (_hubConnection is not null) await _hubConnection.DisposeAsync();
    }
}