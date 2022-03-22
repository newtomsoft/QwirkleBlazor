namespace Qwirkle.WebApi.Client.Blazor.Services.Implementations.DragNDrop;

public class DragNDropManager : IDragNDropManager
{
    private readonly IAreaManager _areaManager;
    public HashSet<DropItem> AllTilesInGame { get; } = new();
    public HashSet<DropItem> TilesDroppedInBoard { get; } = new();
    public HashSet<DropItem> TilesDroppedInBag { get; } = new();

    private readonly List<DropItem> _tilesDroppedInRack = new();
    private readonly List<string> _dropZoneIdentifiersTaken = new();


    private Action _stateHasChanged;

    private Board _board;
    private Rack _rack;

    private const string BagIdentifierPrefix = "bag";
    private const string RackIdentifierPrefix = "rack";
    private const string BoardIdentifierPrefix = "board";
    private const char Separator = '_';

    public DragNDropManager(IAreaManager areaManager)
    {
        _areaManager = areaManager;
        TileInBoardDropped += (source, eventArgs) => _areaManager.OnTileInBoardDropped(source, eventArgs);
    }


    public async Task OnBoardChanged(object source, BoardChangedEventArgs eventArgs)
    {
        Console.WriteLine("DragNDrop: board changed");
        foreach (var tile in eventArgs.Board.Tiles)
        {
            Console.WriteLine($"{tile.Shape} {tile.Color} {tile.Coordinates}");
        }
        _board = eventArgs.Board;
        UpdateDropZones();
    }

    public event EventHandler<TileInBoardDroppedEventArgs> TileInBoardDropped = default!;
    protected virtual void OnTileInBoardDropped(Coordinates coordinates)
    {
        TileInBoardDropped.Invoke(this, new TileInBoardDroppedEventArgs() { Coordinates = coordinates });
    }

    public void OnRackChanged(object source, RackChangedEventArgs eventArgs)
    {
        Console.WriteLine("DragNDrop: rack changed");
        _rack = eventArgs.Rack;
        UpdateDropZones();
    }

    public void Initialize(Action stateHasChanged)
    {
        _stateHasChanged = stateHasChanged;
        UpdateDropZones();
    }
    
    public bool IsDisabled(DropItem item)
    {
        var result = _dropZoneIdentifiersTaken.Contains(item.Identifier);
        return result;
    }

    public bool IsDroppable(string identifier) => _dropZoneIdentifiersTaken.All(x => x != identifier) && AllTilesInGame.All(x => x.Identifier != identifier);

    public void ItemDropped(MudItemDropInfo<DropItem> mudItemDropInfo)
    {
        var (dropItem, dropZoneIdentifier) = mudItemDropInfo;
        var fromDropZone = dropItem.DropZone;
        dropItem.Identifier = dropZoneIdentifier;
        if (fromDropZone == DropZone.Board && _areaManager.BoardLimit.Reduce(dropItem.Coordinates, _board.Tiles.Select(t => t.Coordinates), TilesDroppedInBoard.Select(t => t.Coordinates)))
            _stateHasChanged();

        dropItem.DropZone = DropInZone(dropZoneIdentifier);
        switch (dropItem.DropZone)
        {
            case DropZone.Board:
                TilesDroppedInBoard.Add(dropItem);
                _tilesDroppedInRack.Remove(dropItem);
                TilesDroppedInBag.Remove(dropItem);
                dropItem.Coordinates = ToCoordinate(dropItem.Identifier);
                _stateHasChanged();
                OnTileInBoardDropped(dropItem.Coordinates);
                return;
            case DropZone.Rack:
                _tilesDroppedInRack.Add(dropItem);
                TilesDroppedInBoard.Remove(dropItem);
                TilesDroppedInBag.Remove(dropItem);
                return;
            case DropZone.Bag:
                TilesDroppedInBag.Add(dropItem);
                _tilesDroppedInRack.Remove(dropItem);
                TilesDroppedInBoard.Remove(dropItem);
                return;
            case DropZone.Undefined:
            default:
                throw new ArgumentOutOfRangeException(nameof(mudItemDropInfo));
        }
    }

    public string ToBoardIdentifier(Coordinates coordinates) => $"{BoardIdentifierPrefix}{Separator}{coordinates.X}{Separator}{coordinates.Y}";
    public string ToRackIdentifier(int rackIndex) => $"{RackIdentifierPrefix}{Separator}{rackIndex}";
    public string ToBagIdentifier(int bagIndex) => $"{BagIdentifierPrefix}{Separator}{bagIndex}";

    public void UpdateDropZones()
    {
        AllTilesInGame.Clear();
        TilesDroppedInBag.Clear();
        TilesDroppedInBoard.Clear();
        _tilesDroppedInRack.Clear();
        _dropZoneIdentifiersTaken.Clear();
        var rackIndex = 0;
        foreach (var tile in _rack.Tiles.OrderBy(t => t.RackPosition))
        {
            var droppedTile = new DropItem { Tile = tile, Identifier = ToRackIdentifier(rackIndex), DropZone = DropZone.Rack };
            AllTilesInGame.Add(droppedTile);
            _tilesDroppedInRack.Add(droppedTile);
            //todo remove tempI and use RackPosition when RackPosition ok
            rackIndex++;
        }
        foreach (var tile in _board.Tiles)
        {
            var identifier = ToBoardIdentifier(tile.Coordinates);
            AllTilesInGame.Add(new DropItem { Tile = tile, Identifier = identifier, Coordinates = tile.Coordinates, DropZone = DropZone.Board });
            _dropZoneIdentifiersTaken.Add(identifier);
        }
    }

    private static Coordinates ToCoordinate(string dropItemIdentifier)
    {
        var spited = dropItemIdentifier.Split(Separator);
        var x = int.Parse(spited[1]);
        var y = int.Parse(spited[2]);
        return Coordinates.From(x, y);
    }

    private static DropZone DropInZone(string dropZoneIdentifier)
    {
        if (dropZoneIdentifier.StartsWith(BoardIdentifierPrefix)) return DropZone.Board;
        if (dropZoneIdentifier.StartsWith(RackIdentifierPrefix)) return DropZone.Rack;
        if (dropZoneIdentifier.StartsWith(BagIdentifierPrefix)) return DropZone.Bag;
        return DropZone.Undefined;
    }
}

public class TileInBoardDroppedEventArgs : EventArgs
{
    public Coordinates Coordinates { get; set; }
}