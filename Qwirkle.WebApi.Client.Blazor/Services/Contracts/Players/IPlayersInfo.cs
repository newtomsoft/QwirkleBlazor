namespace Qwirkle.WebApi.Client.Blazor.Services.Contracts.Players;

public interface IPlayersInfo
{
    PlayerInfo[] Get { get; }

    void Initialize(IEnumerable<PlayerInfo> playersInfos, string playerPseudo);
    void OnPlayerTurnChanged(object source, PlayerTurnChangedEventArgs eventArgs);
    void OnPlayerPointsChanged(object source, PlayerPointsChangedEventArgs eventArgs);
}