namespace Qwirkle.WebApi.Client.Blazor.Services.Contracts.DragNDrop;

public interface IDragNDropManager
{
    Task OnBoardChanged(object source, BoardChangedEventArgs eventArgs);
    void OnRackChanged(object source, RackChangedEventArgs eventArgs);

    void Initialize(Action stateHasChanged);

    public string ToBoardIdentifier(Coordinates coordinates);
    public string ToRackIdentifier(int rackIndex);
    public string ToBagIdentifier(int bagIndex);
    HashSet<DropItem> AllTilesInGame { get; }
    HashSet<DropItem> TilesDroppedInBoard { get; }
    HashSet<DropItem> TilesDroppedInBag { get; }
    bool IsDisabled(DropItem item);
    bool IsDroppable(string identifier);
    void ItemDropped(MudItemDropInfo<DropItem> mudItemDropInfo);
    void UpdateDropZones();
}