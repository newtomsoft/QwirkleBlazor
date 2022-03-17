﻿namespace Qwirkle.WebApi.Client.Blazor.Services.Contracts;

public interface INotificationService : IAsyncDisposable
{
    public void Initialize(Uri hubUri);
    Task SendUserWaitingInstantGame(int playersNumber, string userName);
    void SubscribeInstantGameJoined(Action<string> receiveInstantJoined);
    void SubscribeInstantGameStarted(Action<int, int> receiveInstantGameStarted);
    Task Start();
}