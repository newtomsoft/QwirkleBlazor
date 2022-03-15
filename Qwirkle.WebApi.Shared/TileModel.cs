namespace Qwirkle.WebApi.Shared;

public class TileModel
{
    public int GameId { get; init; }
    public TileColor Color { get; init; }
    public TileShape Shape { get; init; }
    public sbyte X { get; init; }
    public sbyte Y { get; init; }

    public TileOnBoard ToTileOnBoard() => new(Color, Shape, Coordinates.From(X, Y));
    public Tile ToTile() => new(Color, Shape);
}
