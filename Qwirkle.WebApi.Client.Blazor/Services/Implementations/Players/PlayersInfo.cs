namespace Qwirkle.WebApi.Client.Blazor.Services.Implementations.Players;

public class PlayersInfo : IPlayersInfo
{
    public PlayerInfo[] Get { get; private set; } = Array.Empty<PlayerInfo>();

    public void Initialize(IEnumerable<PlayerInfo> playersInfos, string playerPseudo)
    {
        var players = playersInfos.ToArray();
        var player = players.Single(p => p.Pseudo == playerPseudo);
        var firstPart = players.Where(p => p.GamePosition >= player.GamePosition).OrderBy(p => p.GamePosition).ToList();
        var secondPart = players.Where(p => p.GamePosition < player.GamePosition).OrderBy(p => p.GamePosition);
        firstPart.AddRange(secondPart);
        Get = firstPart.ToArray();
    }
    public void OnPlayerTurnChanged(object source, PlayerTurnChangedEventArgs eventArgs)
    {
        Get.Single(p => p.IsTurn).IsTurn = false;
        Get.Single(p => p.Pseudo == eventArgs.Pseudo).IsTurn = true;
    }

    public void OnPlayerPointsChanged(object source, PlayerPointsChangedEventArgs eventArgs)
    {
        Get.Single(p => p.Pseudo == eventArgs.Pseudo).Points += eventArgs.Points;
    }
}