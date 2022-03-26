namespace Qwirkle.Domain.ValueObjects;

public record Rack(List<TileOnRack> Tiles)
{
    public static Rack From(List<TileOnRack> tiles) => new(tiles);
    public static Rack Empty => From(new List<TileOnRack>());

    public int TilesNumber => Tiles?.Count ?? 0;

    public Rack WithoutDuplicatesTiles()
    {
        var tiles = Tiles.Select(t => t.ToTile()).Distinct();
        return new(tiles.Select((t, index) => t.ToTileOnRack((RackPosition)index)).ToList());
    }
}