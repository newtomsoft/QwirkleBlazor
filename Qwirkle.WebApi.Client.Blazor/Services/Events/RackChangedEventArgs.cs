namespace Qwirkle.WebApi.Client.Blazor.Services.Events;

public class RackChangedEventArgs : EventArgs
{
    public Rack Rack { get; set; }
}