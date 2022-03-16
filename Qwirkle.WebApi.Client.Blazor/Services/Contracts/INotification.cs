namespace Qwirkle.WebApi.Client.Blazor.Services.Contracts;

public interface INotification
{
    public void CreateHub();
    Task SendUserWaitingInstantGame(int playersNumber, string userName);
    Task Subscribe(Action<int, int> receiveInstantGameStarted, Action<string> receiveInstantGameExpected);
}