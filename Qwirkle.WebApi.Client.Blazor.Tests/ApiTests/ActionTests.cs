namespace Qwirkle.WebApi.Client.Blazor.Tests.ApiTeststTests;
public class ActionTests
{
    [Fact]
    public async Task PlayTilesTest()
    {
        var tileModel = new TileModel { GameId = 1, Shape = TileShape.Circle, Color = TileColor.Green, X = 0, Y = 0 };
        var tiles = new List<TileModel> { tileModel };

        var expectedTileOnBoard = tileModel.ToTileOnBoard();
        var expectedTilesOnBoard = new List<TileOnBoard> { expectedTileOnBoard };
        var actionApiMock = new Mock<IActionApi>();
        var expectedMove = new Move(expectedTilesOnBoard, 5);
        actionApiMock.Setup(x => x.PlayTiles(tiles)).Returns(Task.FromResult(new PlayReturn(1, ReturnCode.Ok, expectedMove, Rack.Empty)));
        var actionApi = actionApiMock.Object;

        var playReturn = await actionApi.PlayTiles(tiles);
        playReturn.Move.ShouldBe(expectedMove);
    }
}
