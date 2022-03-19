namespace Qwirkle.WebApi.Client.Blazor.Pages;

public partial class GameComponent : IAsyncDisposable
{
    [Inject] private IActionApi ActionApi { get; set; } = default!;
    [Inject] private IGameApi GameApi { get; set; } = default!;
    [Inject] private IPlayerApi PlayerApi { get; set; } = default!;
    [Inject] private IGameNotificationService GameNotificationService { get; set; } = default!;
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Parameter] public int GameId { get; set; }
    private Game? Game { get; set; }

    private string ActionResultString { get; set; } = string.Empty;
    private Player Player { get; set; } = default!;

    private BoardLimit _boardLimit = new(null);


    protected override async Task OnInitializedAsync()
    {
        GameNotificationService.Initialize(NavigationManager.ToAbsoluteUri("/hubGame"));
        GameNotificationService.SubscribeTilesPlayed(TilesPlayed);
        GameNotificationService.SubscribeTilesSwapped(TilesSwapped);
        GameNotificationService.SubscribeTurnSkipped(TurnSkipped);
        GameNotificationService.SubscribePlayerIdTurn(PlayerIdTurn);
        GameNotificationService.SubscribeGameOver(GameOver);
        GameNotificationService.SubscribePlayersInGame(PlayersInGame);
        await GameNotificationService.Start();

        if (GameId > 0)
        {
            Game = await GameApi.GetGame(GameId);
            Player = await PlayerApi.GetByGameId(GameId);
            await GameNotificationService.SendPlayerInGame(GameId, Player.Id);
        }
        _boardLimit = new(Game?.Board.Tiles);
    }
    
    private void TilesPlayed(int playerId, Move move)
    {
        Game!.Board.Tiles.UnionWith(move.Tiles);
        StateHasChanged();
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

    private void PlayersInGame(HashSet<int> playersIds)
    {
        Console.WriteLine($"playerIds {playersIds.SelectMany(item => $"{item} - ")} in game");
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

    public async ValueTask DisposeAsync() => await GameNotificationService.Stop();
}