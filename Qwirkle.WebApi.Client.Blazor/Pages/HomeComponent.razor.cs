namespace Qwirkle.WebApi.Client.Blazor.Pages;

public partial class HomeComponent
{
    [Inject] private IGameApi GameApi { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }

    private List<Game> Games { get; } = new();
    private Game? Game { get; set; }



    private async Task GetGames()
    {
        var gamesIds = await GameApi.GetUserGamesIds();
        foreach (var gameId in gamesIds)
        {
            Games.Add(await GameApi.GetGame(gameId));
        }
    }

    private void GetGame() => NavigationManager.NavigateTo($"{Page.Game}/161");
}
