namespace Qwirkle.WebApi.Client.Blazor.Services.Events;

public class BoardChangedEventArgs : EventArgs
{
    public Board Board { get; set; }
}