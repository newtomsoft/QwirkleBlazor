namespace Qwirkle.WebApi.Client.Blazor.Pages;

public partial class InstantGameComponent
{
    [Inject] private IInstantGameApi InstantGameApi { get; set; }
    [Inject] private INotification Notification { get; set; }

    [Inject] private NavigationManager NavigationManager { get; set; }


    [Parameter] public string? PlayersNumber { get; set; }

    //private HubConnection? _hubConnection;
    private string _userName = string.Empty;
    private List<string> _playersNames = new();


    [CascadingParameter]
    private Task<AuthenticationState> authenticationStateTask { get; set; }

    private async Task JoinInstantGame(int playersNumber)
    {
        var result = await InstantGameApi.JoinInstantGame(playersNumber);
        if (result.GameId != 0)
            NavigationManager.NavigateTo($"{Page.Game}/{result.GameId}");
        else
        {
            _playersNames = result.UsersNames.ToList();
            await Notification.SendUserWaitingInstantGame(playersNumber, _userName);
        }
    }

    protected override async Task OnInitializedAsync()
    {
        _userName = authenticationStateTask.Result.User.Identity!.Name!;
        Notification.CreateHub();
        await Notification.Subscribe(ReceiveInstantGameStarted, ReceiveInstantGameExpected); ///todo refactor signature
    }
    
    private void ReceiveInstantGameStarted(int playersNumberForStartGame, int gameId) => NavigationManager.NavigateTo($"{Page.Game}/{gameId}");

    private void ReceiveInstantGameExpected(string userName)
    {
        _playersNames.Add(userName);
        StateHasChanged();
    }
    
    public async ValueTask DisposeAsync()
    {
        //if (_hubConnection is not null)
        //{
        //    await _hubConnection.DisposeAsync();
        //}
    }
}
