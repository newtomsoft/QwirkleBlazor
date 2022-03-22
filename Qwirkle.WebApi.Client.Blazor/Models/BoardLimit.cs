namespace Qwirkle.WebApi.Client.Blazor.Models;

public record BoardLimit
{
    public int MinX { get; private set; }
    public int MaxX { get; private set; }
    public int MinY { get; private set; }
    public int MaxY { get; private set; }

    public int LinesNumber => MaxY - MinY + 1;
    public int ColumnNumber => MaxX - MinX + 1;

    public BoardLimit(IEnumerable<Coordinates> coordinates)
    {
        var coordinatesList = coordinates.ToList();
        if (coordinatesList.Any())
        {
            MinX = coordinatesList.Min(c => c.X) - 1;
            MaxX = coordinatesList.Max(c => c.X) + 1;
            MinY = coordinatesList.Min(c => c.Y) - 1;
            MaxY = coordinatesList.Max(c => c.Y) + 1;
        }
        else
        {
            MinX = MinY = 0;
            MaxX = MaxY = 0;
        }
    }

    private void Reset(IEnumerable<Coordinates> coordinates)
    {
        var coordinatesList = coordinates.ToList();
        if (coordinatesList.Any())
        {
            MinX = coordinatesList.Min(c => c.X) - 1;
            MaxX = coordinatesList.Max(c => c.X) + 1;
            MinY = coordinatesList.Min(c => c.Y) - 1;
            MaxY = coordinatesList.Max(c => c.Y) + 1;
        }
        else
        {
            MinX = MinY = 0;
            MaxX = MaxY = 0;
        }
    }

    public bool Update(IEnumerable<Coordinates> coordinates)
    {
        var oldLimit = this;
        Reset(coordinates);
        return oldLimit != this;
    }

    public bool Enlarge(Coordinates coordinates)
    {
        var result = false;
        var (x, y) = coordinates;
        if (x <= MinX) { MinX -= MinX - x + 1; result = true; }
        if (x >= MaxX) { MaxX += x - MaxX + 1; result = true; }
        if (y <= MinY) { MinY -= MinY - y + 1; result = true; }
        if (y >= MaxY) { MaxY += y - MaxY + 1; result = true; }
        return result;
    }

    public bool Reduce(Coordinates coordinates, IEnumerable<Coordinates> tilesPlayedCoordinates, IEnumerable<Coordinates> tilesDroppedCoordinates)
    {
        var tilesDroppedCoordinatesList = tilesDroppedCoordinates.ToList();
        tilesDroppedCoordinatesList.Remove(coordinates);
        var tilesInBoardCoordinates = tilesPlayedCoordinates.ToList();
        tilesInBoardCoordinates.AddRange(tilesDroppedCoordinatesList);
        return Update(tilesInBoardCoordinates);
    }
}