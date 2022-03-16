namespace Qwirkle.WebApi.Client.Blazor.Pages;

public partial class Home
{
    [Inject] private IGameApi GameApi { get; set; }

    private List<Game> Games { get; } = new();
    private Game? Game { get; set; }

    private async Task GetGames()
    {
        var gamesIds = await GameApi.GetUserGamesIds();
        foreach (var gameId in gamesIds)
        {
            Games.Add(await GameApi.GetUserGame(gameId));
        }
    }

    private async Task GetGame()
    {
        Game = await GameApi.GetUserGame(161);
    }
}
