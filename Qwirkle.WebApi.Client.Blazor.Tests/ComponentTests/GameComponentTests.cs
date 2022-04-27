namespace Qwirkle.WebApi.Client.Blazor.Tests.ComponentTests;

public class GameComponentTests
{
    [Fact]
    public void SkipTurnTest()
    {
        const int gameId = 1;
        const ReturnCode returnCode = ReturnCode.Ok;
        var skipTurnModel = new SkipTurnModel { GameId = gameId };
        var skipTurnReturnTask = Task.FromResult(new SkipTurnReturn { GameId = gameId, Code = returnCode });
        var gameApi = new Mock<IApiGame>();
        gameApi.Setup(x => x.GetGame(gameId)).Returns(Task.FromResult(new Game()));
        var actionApi = new Mock<IApiAction>();
        actionApi.Setup(x => x.SkipTurn(skipTurnModel)).Returns(skipTurnReturnTask);
        var playerApi = new Mock<IApiPlayer>();
        INotificationGame notificationService = new NoNotificationGame();

        var gameComponent = ComponentFactory<GameComponent>.RenderComponent(gameApi.Object, actionApi.Object, playerApi.Object, notificationService, out var navigationManager);

        gameComponent.Find("#actionResult").Text().ShouldBe(string.Empty);

        //TODO
        //gameComponent.Find("#btnSkipTurn").Click();
    }
}
