namespace Qwirkle.WebApi.Client.Blazor.Pages;

public partial class GameComponent
{
    [Inject] private IActionApi ActionApi { get; set; }
    [Inject] private IGameApi GameApi { get; set; }
    [Inject] private IPlayerApi PlayerApi { get; set; }
    [Inject] private IGameNotificationService GameNotificationService { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Parameter] public string GameIdString { get; set; }
    private int GameId => int.Parse(GameIdString); //Todo revoir conversion automatique param string en int
    private Game? Game { get; set; }

    [CascadingParameter]
    private AuthenticationState context { get; set; }

    private string ActionResultString { get; set; } = string.Empty;
    private Player Player { get; set; }


    protected override async Task OnInitializedAsync()
    {
        if (int.TryParse(GameIdString, out var gameId) && gameId != 0)
            Game = await GetGame(gameId);

        GameNotificationService.Initialize(NavigationManager.ToAbsoluteUri("/hubGame"));
        GameNotificationService.SubscribeTilesPlayed(TilesPlayed);
        GameNotificationService.SubscribeTilesSwapped(TilesSwapped);
        GameNotificationService.SubscribeTurnSkipped(TurnSkipped);
        GameNotificationService.SubscribePlayerIdTurn(PlayerIdTurn);
        GameNotificationService.SubscribeGameOver(GameOver);
        await GameNotificationService.Start();

        Player = await PlayerApi.GetByGameId(gameId);
        await GameNotificationService.SendPlayerInGame(GameId, Player.Id);
    }


    private void TilesPlayed(int playerId, Move move)
    {
        Console.WriteLine($"playerId {playerId} has play {move}");
    }

    private void GameOver(int playerId)
    {
        Console.WriteLine($"game is over. {playerId} win");
    }

    private void PlayerIdTurn(int playerId)
    {
        Console.WriteLine($"it's playerId {playerId} to play");
    }

    private void TurnSkipped(int playerId)
    {
        Console.WriteLine($"playerId {playerId} has skipped turn");
    }

    private void TilesSwapped(int playerId)
    {
        Console.WriteLine($"playerId {playerId} has swapped tiles");
    }


    private async Task PlayTiles()
    {
        var tileModel = new TileModel { GameId = GameId, Shape = TileShape.Circle, Color = TileColor.Green, X = 0, Y = 0 };
        var tiles = new List<TileModel> { tileModel };
        var playReturn = await ActionApi.PlayTiles(tiles);
    }

    private async Task SkipTurn(SkipTurnModel skipTurnModel)
    {
        var skipTurnReturn = await ActionApi.SkipTurn(skipTurnModel);
        ActionResultString = skipTurnReturn.Code.ToString();
    }

    private async Task<Game> GetGame(int gameId) => await GameApi.GetUserGame(gameId);
}
