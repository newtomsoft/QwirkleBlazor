namespace Qwirkle.WebApi.Client.Blazor.Components;

public partial class PlayerComponent
{
    [Parameter] public PlayerInfo PlayerInfo { get; set; } = default!;
}