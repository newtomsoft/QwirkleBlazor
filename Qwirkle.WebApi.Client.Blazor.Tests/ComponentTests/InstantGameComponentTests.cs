namespace Qwirkle.WebApi.Client.Blazor.Tests.ComponentTests;

public class InstantGameComponentTests
{
    public static TheoryData<InstantGameTestsInput> InstantGameTestsInputs => new()
    {
        new(true, 2, new() { IsAdded = true, GameId = 33, UsersNames = new[] { "player1", "player2" } }),
        new(false, 2, new() { IsAdded = true, GameId = 0, UsersNames = new[] { "player1", } }),

        new(true, 3, new() { IsAdded = true, GameId = 69, UsersNames = new[] { "player1", "player2", "player3" } }),
        new(false, 3, new() { IsAdded = true, GameId = 0, UsersNames = new[] { "player1", "player2" } }),
        new(false, 3, new() { IsAdded = true, GameId = 0, UsersNames = new[] { "player1", } }),

        new(true, 4, new() { IsAdded = true, GameId = 42, UsersNames = new[] { "player1", "player2", "player3", "player4" } }),
        new(false, 4, new() { IsAdded = true, GameId = 0, UsersNames = new[] { "player1", "player2", "player3" } }),
        new(false, 4, new() { IsAdded = true, GameId = 0, UsersNames = new[] { "player1", "player2" } }),
        new(false, 4, new() { IsAdded = true, GameId = 0, UsersNames = new[] { "player1", } }),

    };

    [Theory(DisplayName = "InstantGame Test")]
    [MemberData(nameof(InstantGameTestsInputs))]
    public void InstantGameWaitingPlayersWhenPlayersNumberIsNotEnough(InstantGameTestsInput input)
    {
        var instantGameApi = new Mock<IApiInstantGame>();
        instantGameApi.Setup(x => x.JoinInstantGame(input.PlayersNumberNeedToStartGame)).Returns(Task.FromResult(input.InstantGameModel));
        var instantGameComponent = ComponentFactory<InstantGameComponent>.RenderComponent(instantGameApi.Object, out var navigationManager);

        instantGameComponent.Find($"#btn{input.PlayersNumberNeedToStartGame}Players").Click();

        if (input.ShouldGameBeStarted)
        {
            var newUri = new Uri(navigationManager.Uri);
            newUri.LocalPath.ShouldBe($"{PageName.Game}/{input.InstantGameModel.GameId}");
        }
        else
        {
            var newUri = new Uri(navigationManager.Uri);
            newUri.LocalPath.ShouldNotContain($"{PageName.Game}");
            var waitingPlayerElements = instantGameComponent.FindAll(".waitingPlayer").ToList();
            waitingPlayerElements.Count.ShouldBe(input.InstantGameModel.UsersNames.Length);
            for (var i = 0; i < input.InstantGameModel.UsersNames.Length; i++)
                waitingPlayerElements[i].TextContent.ShouldBe(input.InstantGameModel.UsersNames[i]);
        }
    }
}