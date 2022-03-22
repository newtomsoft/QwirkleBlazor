namespace Qwirkle.WebApi.Client.Blazor.Services.Events;

public class TileOutBoardDroppedEventArgs : EventArgs
{
    public Coordinates Coordinates { get; set; }
}