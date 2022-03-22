namespace Qwirkle.WebApi.Client.Blazor.Services.Contracts.NotificationReceiver;

public interface INotificationReceiver
{
    void Initialize(Game game, Player currentPlayer, BoardLimit boardLimit, Action stateHaveChange);
    void TilesPlayed(int playerId, Move move);
    void GameOver(int playerId);
    void PlayerIdTurn(int playerId);
    void TurnSkipped(int playerId);
    void TilesSwapped(int playerId);
    void PlayersInGame(HashSet<int> playersIds);
}

public class NoNotificationReceiver : INotificationReceiver
{
    public void Initialize(Game game, Player currentPlayer, BoardLimit boardLimit, Action stateHaveChange)
    {
    }

    public void TilesPlayed(int playerId, Move move)
    {
    }

    public void GameOver(int playerId)
    {
    }

    public void PlayerIdTurn(int playerId)
    {
    }

    public void TurnSkipped(int playerId)
    {
    }

    public void TilesSwapped(int playerId)
    {
    }

    public void PlayersInGame(HashSet<int> playersIds)
    {
    }
}