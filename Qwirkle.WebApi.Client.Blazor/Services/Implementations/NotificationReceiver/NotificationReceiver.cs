namespace Qwirkle.WebApi.Client.Blazor.Services.Implementations.NotificationReceiver;

public class NotificationReceiver : INotificationReceiver
{
    private readonly IDragNDropManager _dragNDropManager = default!;
    private readonly ISnackbar _snackBar = default!;
    private Game _game = default!;
    private Action _stateHasChanged = default!;
    private Player _currentPlayer = default!;
    private BoardLimit _boardLimit;
    private string PlayerName(int playerId) => _game.Players.Single(p => p.Id == playerId).Pseudo;

    public NotificationReceiver(ISnackbar snackBar, IDragNDropManager dragNDropManager)
    {
        _snackBar = snackBar;
        _dragNDropManager = dragNDropManager;
    }

    public void Initialize(Game game, Player currentPlayer, BoardLimit boardLimit, Action stateHaveChange)
    {
        _game = game;
        _stateHasChanged = stateHaveChange;
        _boardLimit = boardLimit;
        _currentPlayer = currentPlayer;
    }

    public void TilesPlayed(int playerId, Move move)
    {
        var (tileOnBoards, points) = move;
        _game.Board.Tiles.UnionWith(tileOnBoards);
        //_boardLimit.Update();
        //_dragNDropManager.UpdateBoardLimitAndSquareSizeAsync(_game.Board.Tiles.Select(t => t.Coordinates));
        _dragNDropManager.UpdateDropZones(); //todo do better
        //_areaManager.
        _stateHasChanged();
        if (_currentPlayer.Id != playerId) _snackBar.Add($"{PlayerName(playerId)} has played {tileOnBoards.Count} tiles and got {points} points");
    }

    public void GameOver(int playerId)
    {
        var playerName = _currentPlayer.Id == playerId ? "You" : PlayerName(playerId);
        _snackBar.Add($"Game is over. {playerName} win");
    }

    public void PlayerIdTurn(int playerId)
    {
        var message = _currentPlayer.Id == playerId ? "It's your turn" : $"It's {PlayerName(playerId)}'s turn";
        _snackBar.Add(message);
    }

    public void TurnSkipped(int playerId)
    {
        if (_currentPlayer.Id != playerId) _snackBar.Add($"{PlayerName(playerId)} has skipped his turn");
    }

    public void TilesSwapped(int playerId)
    {
        if (_currentPlayer.Id != playerId) _snackBar.Add($"{PlayerName(playerId)} has swapped tiles");
    }

    public void PlayersInGame(HashSet<int> playersIds)
    {
        playersIds.Remove(_currentPlayer.Id);
        if (playersIds.Count == 1)
        {
            _snackBar.Add($"{PlayerName(playersIds.First())} is online");
            return;
        }

        var stringBuilder = new StringBuilder();
        foreach (var playerId in playersIds)
        {
            stringBuilder.Append(PlayerName(playerId));
            stringBuilder.Append(" - ");
        }
        stringBuilder.Remove(stringBuilder.Length - 3, 2);
        stringBuilder.Append("are online");
        _snackBar.Add(stringBuilder.ToString());
    }
}