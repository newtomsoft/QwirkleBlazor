﻿namespace Qwirkle.Domain.ExtensionMethods;

public static class TilesExtensionsMethods
{
    public static List<TileOnBoard> FirstConsecutive(this List<TileOnBoard> tiles, Direction direction, sbyte reference)
    {
        var diff = direction is Direction.Right or Direction.Top ? -1 : 1;
        var result = new List<TileOnBoard>();
        if (tiles.Count == 0) return result;
        if (direction is Direction.Left or Direction.Right && reference != tiles[0].Coordinate.X + diff) return result;
        if (direction is Direction.Top or Direction.Bottom && reference != tiles[0].Coordinate.Y + diff) return result;

        result.Add(tiles[0]);
        for (var i = 1; i < tiles.Count; i++)
        {
            if (direction is Direction.Left or Direction.Right && tiles[i - 1].Coordinate.X == tiles[i].Coordinate.X + diff && tiles[i - 1].Coordinate.Y == tiles[i].Coordinate.Y
                || direction is Direction.Top or Direction.Bottom && tiles[i - 1].Coordinate.Y == tiles[i].Coordinate.Y + diff && tiles[i - 1].Coordinate.X == tiles[i].Coordinate.X)
                result.Add(tiles[i]);
            else
                break;
        }
        return result;
    }

    public static bool FormCompliantRow(this List<TileOnBoard> tiles)
    {
        for (var i = 0; i < tiles.Count; i++)
            for (var j = i + 1; j < tiles.Count; j++)
                if (!tiles[i].OnlyShapeOrColorEqual(tiles[j])) return false;

        return true;
    }

    public static string ToLog(this List<TileOnBoard> tiles)
    {
        var stringBuilder = new StringBuilder();
        foreach (var tile in tiles)
            stringBuilder.Append($"{tile.Shape}-{tile.Color} ({tile.Coordinate.X},{tile.Coordinate.Y}) ");

        return stringBuilder.ToString();
    }
}