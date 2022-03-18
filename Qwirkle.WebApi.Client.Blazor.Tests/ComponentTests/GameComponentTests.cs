using Qwirkle.Domain.Entities;

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
        var gameApi = new Mock<IGameApi>();
        gameApi.Setup(x => x.GetGame(gameId)).Returns(Task.FromResult(new Game()));
        var actionApi = new Mock<IActionApi>();
        actionApi.Setup(x => x.SkipTurn(skipTurnModel)).Returns(skipTurnReturnTask);
        var playerApi = new Mock<IPlayerApi>();
        IGameNotificationService notificationService = new NoGameNotification();

        var gameComponent = ComponentFactory<GameComponent>.RenderComponent(gameApi.Object, actionApi.Object, playerApi.Object, notificationService, out var navigationManager);

        gameComponent.Find("#actionResult").Text().ShouldBe(string.Empty);
        gameComponent.Find("#btnSkipTurn").Click();
        gameComponent.Find("#actionResult").Text().ShouldBe(skipTurnReturnTask.Result.Code.ToString());
    }
}
