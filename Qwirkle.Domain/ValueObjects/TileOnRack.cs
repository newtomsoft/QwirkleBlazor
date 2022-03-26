namespace Qwirkle.Domain.ValueObjects;

public record TileOnRack(RackPosition RackPosition, TileColor Color, TileShape Shape) : Tile(Color, Shape)
{
    public TileOnRack(RackPosition rackPosition, Tile tile) : this(rackPosition, tile.Color, tile.Shape) { }

    public TileOnRack() : this(0, TileColor.Blue, TileShape.Circle) { }


    public TileOnBoard ToTileOnBoard(Coordinate coordinate) => TileOnBoard.From(this, coordinate);
    public Tile ToTile() => new(Color, Shape);
}