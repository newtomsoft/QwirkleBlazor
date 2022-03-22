namespace Qwirkle.WebApi.Client.Blazor.Services.Implementations.DragNDrop;

public class DropItem
{
    public Tile Tile { get; init; } = default!;
    public string Identifier { get; set; } = default!;
    public DropZone DropZone { get; set; }
    public Coordinates Coordinates { get; set; } = default!;
}