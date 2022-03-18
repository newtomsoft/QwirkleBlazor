namespace Qwirkle.WebApi.Client.Blazor.Models;

public class BoardLimit
{
    public int MinX { get; }
    public int MaxX { get; }
    public int MinY { get; }
    public int MaxY { get; }

    public BoardLimit(IReadOnlyCollection<TileOnBoard>? boardTiles)
    {
        if (boardTiles is not null)
        {
            MinX = boardTiles.Min(t => t.Coordinates.X) - 2;
            MaxX = boardTiles.Max(t => t.Coordinates.X) + 2;
            MinY = boardTiles.Min(t => t.Coordinates.Y) - 2;
            MaxY = boardTiles.Max(t => t.Coordinates.Y) + 2;
        }
        else
        {
            MinX = MinY = -2;
            MaxX = MaxY = 2;
        }
    }
}