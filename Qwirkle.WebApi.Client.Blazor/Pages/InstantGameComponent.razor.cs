namespace Qwirkle.WebApi.Client.Blazor.Pages;

public partial class InstantGameComponent : IAsyncDisposable
{
    [Inject] private IInstantGameApi InstantGameApi { get; set; }
    [Inject] private INotificationService NotificationService { get; set; }

    [Inject] private NavigationManager NavigationManager { get; set; }


    [Parameter] public string? PlayersNumber { get; set; }

    private string _userName = string.Empty;
    private List<string> _playersNames = new();


    [CascadingParameter]
    private Task<AuthenticationState> authenticationStateTask { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _userName = authenticationStateTask.Result.User.Identity!.Name!;

        NotificationService.Initialize(NavigationManager.ToAbsoluteUri("/hubGame"));
        NotificationService.SubscribeInstantGameStarted(InstantGameStarted);
        NotificationService.SubscribeInstantGameJoined(InstantGameJoinedBy);
        await NotificationService.Start();
    }

    private async Task JoinInstantGame(int playersNumber)
    {
        var result = await InstantGameApi.JoinInstantGame(playersNumber);
        if (result.GameId != 0)
            NavigationManager.NavigateTo($"{Page.Game}/{result.GameId}");
        else
        {
            _playersNames = result.UsersNames.ToList();
            await NotificationService.SendUserWaitingInstantGame(playersNumber, _userName);
        }
    }

    private void InstantGameStarted(int playersNumberForStartGame, int gameId) => NavigationManager.NavigateTo($"{Page.Game}/{gameId}");

    private void InstantGameJoinedBy(string userName)
    {
        _playersNames.Add(userName);
        StateHasChanged();
    }
    
    public async ValueTask DisposeAsync() => await NotificationService.DisposeAsync();
}
