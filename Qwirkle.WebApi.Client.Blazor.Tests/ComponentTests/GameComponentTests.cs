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

        using var testContext = new TestContext();
        var actionApiMock = new Mock<IActionApi>();
        actionApiMock.Setup(x => x.SkipTurn(skipTurnModel)).Returns(skipTurnReturnTask);
        var actionApi = actionApiMock.Object;

        var authContext = testContext.AddTestAuthorization();
        authContext.SetAuthorized("newtom");

        testContext.Services.AddSingleton(actionApi);
        var gameComponent = testContext.RenderComponent<GameComponent>();

        gameComponent.Find("#actionResult").Text().ShouldBe(string.Empty);

        gameComponent.Find("#btnSkipTurn").Click();

        gameComponent.Find("#actionResult").Text().ShouldBe(skipTurnReturnTask.Result.Code.ToString());
    }
}
