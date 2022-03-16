namespace Qwirkle.Domain.ValueObjects;

public record TileOnPlayer(RackPosition RackPosition, TileColor Color, TileShape Shape) : Tile(Color, Shape)
{
    public TileOnPlayer(RackPosition rackPosition, Tile tile) : this(rackPosition, tile.Color, tile.Shape) { }

    public TileOnPlayer() : this(0, TileColor.Blue, TileShape.Circle) { }



    public TileOnBoard ToTileOnBoard(Coordinates coordinates) => TileOnBoard.From(this, coordinates);
    public Tile ToTile() => new(Color, Shape);
}