namespace Qwirkle.WebApi.Client.Blazor.Pages;

public partial class GameComponent : IAsyncDisposable
{
    [Parameter] public int GameId { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private IApiAction ApiAction { get; set; } = default!;
    [Inject] private IApiGame ApiGame { get; set; } = default!;
    [Inject] private IApiPlayer ApiPlayer { get; set; } = default!;
    [Inject] private INotificationGame NotificationGame { get; set; } = default!;
    [Inject] private ISnackbar SnackBar { get; set; } = default!;
    [Inject] private INotificationReceiver NotificationReceiver { get; set; } = default!;
    [Inject] private IAreaManager AreaManager { get; set; } = default!;
    [Inject] private IDragNDropManager DragNDropManager { get; set; } = default!;

    private string _actionResultString = string.Empty;
    private Game _game = default!;
    private Player _player = default!;
    private bool _isInitialized;

    protected override async Task OnInitializedAsync()
    {
        if (GameId <= 0) return;

        BoardChanged += (source, eventArgs) => DragNDropManager.OnBoardChanged(source, eventArgs);
        BoardChanged += (source, eventArgs) => AreaManager.OnBoardChanged(source, eventArgs);
        RackChanged += DragNDropManager.OnRackChanged;

        _game = await ApiGame.GetGame(GameId);
        OnBoardChanged(_game.Board);
        _player = await ApiPlayer.GetByGameId(GameId);
        OnRackChanged(_player.Rack);

        DragNDropManager.Initialize(StateHasChanged);
        await InitializeNotifications();

        _isInitialized = true;
        await NotificationGame.SendPlayerInGame(GameId, _player.Id);
    }



    private async Task InitializeNotifications()
    {
        NotificationReceiver.Initialize(_game, _player, AreaManager.BoardLimit, StateHasChanged);
        NotificationGame.SubscribePlayersInGame(NotificationReceiver.PlayersInGame);
        NotificationGame.SubscribeTilesPlayed(NotificationReceiver.TilesPlayed);
        NotificationGame.SubscribeTilesSwapped(NotificationReceiver.TilesSwapped);
        NotificationGame.SubscribeTurnSkipped(NotificationReceiver.TurnSkipped);
        NotificationGame.SubscribePlayerIdTurn(NotificationReceiver.PlayerIdTurn);
        NotificationGame.SubscribeGameOver(NotificationReceiver.GameOver);
        await NotificationGame.Start();
    }
    
    private async Task PlayTiles()
    {
        var tilesModel = DragNDropManager.TilesDroppedInBoard.Select(t => new TileModel { GameId = GameId, Shape = t.Tile.Shape, Color = t.Tile.Color, X = t.Coordinates.X, Y = t.Coordinates.Y }).ToList();
        if (!tilesModel.Any())
        {
            SnackBar.Add("please move some tiles into board");
            return;
        }
        var playReturn = await ApiAction.PlayTiles(tilesModel);
        if (playReturn is { Code: ReturnCode.Ok })
        {
            _player.Rack = playReturn.NewRack;
            OnRackChanged(playReturn.NewRack);
            _game.Board.AddTiles(tilesModel.Select(t => t.ToTileOnBoard()));
            OnBoardChanged(_game.Board);
            StateHasChanged();
            NavigationManager.NavigateTo($"{PageName.Game}/{GameId}"); //try
        }
        else
        {
            SnackBar.Add($"{playReturn.Code.ToDisplay()}");
        }
    }

    public event EventHandler<BoardChangedEventArgs> BoardChanged = default!;
    protected virtual void OnBoardChanged(Board board)
    {
        BoardChanged.Invoke(this, new BoardChangedEventArgs() { Board = board });
    }


    private async Task SwapTiles()
    {
        var tilesModel = DragNDropManager.TilesDroppedInBag.Select(t => new TileModel { GameId = GameId, Shape = t.Tile.Shape, Color = t.Tile.Color }).ToList();
        if (!tilesModel.Any())
        {
            SnackBar.Add("please move some tiles into bag");
            return;
        }
        var swapTilesReturn = await ApiAction.SwapTiles(tilesModel);
        if (swapTilesReturn is { Code: ReturnCode.Ok })
        {
            _player.Rack = swapTilesReturn.NewRack;
            OnRackChanged(_player.Rack);
            StateHasChanged();
        }
        else
        {
            SnackBar.Add($"{swapTilesReturn.Code.ToDisplay()}");
        }
    }

    public event EventHandler<RackChangedEventArgs> RackChanged = default!;
    protected virtual void OnRackChanged(Rack rack)
    {
        RackChanged.Invoke(this, new RackChangedEventArgs() { Rack = rack });
    }


    private async Task SkipTurn(SkipTurnModel skipTurnModel)
    {
        var skipTurnReturn = await ApiAction.SkipTurn(skipTurnModel);
        _actionResultString = skipTurnReturn.Code.ToString();
        if (skipTurnReturn is not { Code: ReturnCode.Ok })
            SnackBar.Add($"{skipTurnReturn.Code.ToDisplay()}", Severity.Error);
    }

    public async ValueTask DisposeAsync() => await NotificationGame.Stop();
}