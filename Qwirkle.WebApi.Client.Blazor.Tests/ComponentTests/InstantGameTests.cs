namespace Qwirkle.WebApi.Client.Blazor.Tests.ComponentTests;

public class InstantGameTests
{
    [Fact]
    public void InstantGameTest()
    {
        const int gameId = 33;
        var instantGameApi = new Mock<IInstantGameApi>();
        var modelGameStartedTask = Task.FromResult(new InstantGameModel { GameId = gameId, UsersNames = new[] { "player1", "player2", "player3", "player4" } });
        instantGameApi.Setup(x => x.JoinInstantGame(2)).Returns(modelGameStartedTask);
        var instantGameComponent = ComponentFactory<InstantGameComponent>.RenderComponent(instantGameApi.Object, out var navigationManager);
        instantGameComponent.Find("#btn2Players").Click();

        var newUri = new Uri(navigationManager.Uri);
        newUri.LocalPath.ShouldBe($"{Page.Game}/{gameId}");
    }

    [Fact]
    public void InstantGameWaiting1PlayerTest()
    {
        var instantGameApi = new Mock<IInstantGameApi>();
        var modelGameWaitingTask = Task.FromResult(new InstantGameModel { GameId = 0, UsersNames = new[] { "player1" } });
        instantGameApi.Setup(x => x.JoinInstantGame(2)).Returns(modelGameWaitingTask);
        var instantGameComponent = ComponentFactory<InstantGameComponent>.RenderComponent(instantGameApi.Object, out var navigationManager);
        instantGameComponent.Find("#btn2Players").Click();

        var markup = instantGameComponent.Markup;
        var newUri = new Uri(navigationManager.Uri);
        newUri.LocalPath.ShouldNotContain($"{Page.Game}");
        //todo Assert player wait 1 opponent
    }

    [Fact]
    public void InstantGameWaiting2PlayersTest()
    {
        var instantGameApi = new Mock<IInstantGameApi>();
        var modelGameWaitingTask = Task.FromResult(new InstantGameModel { GameId = 0, UsersNames = new[] { "player1" } });
        instantGameApi.Setup(x => x.JoinInstantGame(3)).Returns(modelGameWaitingTask);
        var instantGameComponent = ComponentFactory<InstantGameComponent>.RenderComponent(instantGameApi.Object, out var navigationManager);
        instantGameComponent.Find("#btn3Players").Click();

        var markup = instantGameComponent.Markup;
        var newUri = new Uri(navigationManager.Uri);
        newUri.LocalPath.ShouldNotContain($"{Page.Game}");
        //todo Assert player wait 2 opponents
        //idem 4

        //simuler joueur arrivant ?
    }
}