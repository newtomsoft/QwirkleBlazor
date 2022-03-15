namespace Qwirkle.WebApi.Client.Blazor.Pages;

public partial class Game
{
    [Inject] private IActionApi ActionApi { get; set; }

    private string ActionResultString { get; set; } = string.Empty;

    private async Task PlayTiles()
    {
        var tileModel = new TileModel { GameId = 241, Shape = TileShape.Circle, Color = TileColor.Green, X = 0, Y = 0 };
        var tiles = new List<TileModel> { tileModel };
        var playReturn = await ActionApi.PlayTiles(tiles);
    }

    private async Task SkipTurn(SkipTurnModel skipTurnModel)
    {
        var skipTurnReturn = await ActionApi.SkipTurn(skipTurnModel);
        ActionResultString = skipTurnReturn.Code.ToString();
    }
}
