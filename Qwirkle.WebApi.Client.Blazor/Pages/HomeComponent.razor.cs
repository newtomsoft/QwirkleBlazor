namespace Qwirkle.WebApi.Client.Blazor.Pages;

public partial class HomeComponent
{
    [Inject] private IApiGame ApiGame { get; set; } = default!;
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    private List<Game> Games { get; } = new();
    private List<int> GamesIds { get; set; } = new();

    protected override async Task<Task> OnInitializedAsync()
    {
        GamesIds = await ApiGame.GetUserGamesIds();
        return base.OnInitializedAsync();
    }


    private async Task GetGames()
    {
        var gamesIds = await ApiGame.GetUserGamesIds();
        foreach (var gameId in gamesIds)
        {
            Games.Add(await ApiGame.GetGame(gameId));
        }
    }

    private void GetGame(int gameId) => NavigationManager.NavigateTo($"{PageName.Game}/{gameId}");

    private async Task CreateTestGame()
    {
        var gameId = await ApiGame.CreateGame(new List<string>());
        NavigationManager.NavigateTo($"{PageName.Game}/{gameId}");
    }
}
