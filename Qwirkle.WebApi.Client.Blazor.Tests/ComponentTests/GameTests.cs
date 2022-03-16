namespace Qwirkle.WebApi.Client.Blazor.Tests.ComponentTests;
public class GameTests
{
    [Fact]
    public async Task SkipTurnTest()
    {
        var gameId = 1;
        var returnCode = ReturnCode.Ok;
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
