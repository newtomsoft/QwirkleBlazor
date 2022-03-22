namespace Qwirkle.WebApi.Client.Blazor.Services.Implementations.WindowManager;

public class AreaManager : IAreaManager
{
    private readonly IJSRuntime _jsRuntime;
    private readonly AreaDimension _menuDimension = new() { Width = 200, Height = 56 }; //todo use it for menu
    private readonly AreaDimension _commandArea = new() { Width = 200, Height = 200 }; //todo use it for command
    private AreaDimension _boardDimension = default!;



    public BoardLimit BoardLimit { get; private set; }
    public string BoardSquareSize { get; private set; }

    public AreaManager(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
        BoardLimit = new BoardLimit(new[] {Coordinates.From(0, 0),});
    }

    public async Task OnBoardChanged(object source, BoardChangedEventArgs eventArgs)
    {
        Console.WriteLine("AreaManager: board changed");

        var coordinatesInBoard = new List<Coordinates>(eventArgs.Board.Tiles.Select(t => t.Coordinates));
        BoardLimit = GetBoardLimits(coordinatesInBoard);
        await UpdateBoardSquareSizeAsync();
    }
    public async Task OnTileInBoardDropped(object sender, TileInBoardDroppedEventArgs eventArgs)
    {
        Console.WriteLine("AreaManager: tile dropped in board");
        BoardLimit.Enlarge(eventArgs.Coordinates);
        await UpdateBoardSquareSizeAsync();
    }

    private BoardLimit GetBoardLimits(IEnumerable<Coordinates> coordinates)
    {
        BoardLimit.Update(coordinates);
        return BoardLimit;
    }

    public async Task UpdateBoardSquareSizeAsync()
    {
        var dimension = await GetSquareDimensionAsync(BoardLimit);
        BoardSquareSize = $"height:{dimension.Height}px; width:{dimension.Width}px;";
    }


    private async Task<AreaDimension> GetSquareDimensionAsync(BoardLimit boardLimit)
    {
        _boardDimension = await ComputeBoardDimensionAsync();
        var columnNumber = boardLimit.ColumnNumber;
        var lineNumber = boardLimit.LinesNumber;
        var maxWidth = _boardDimension.Width / columnNumber;
        var maxHeight = _boardDimension.Height / lineNumber;
        var widthHeight = Math.Min(maxWidth, maxHeight);
        return new AreaDimension { Width = widthHeight, Height = widthHeight };
    }

    private async Task<AreaDimension> ComputeBoardDimensionAsync()
    {
        var windowDimension = await GetWindowDimensionAsync();
        return windowDimension.Orientation == WindowOrientation.Portrait
            ? windowDimension.ReducedByHeight(_menuDimension.Height + _commandArea.Height)
            : windowDimension.ReducedByWidth(_menuDimension.Width + _commandArea.Width);
    }

    private async Task<AreaDimension> GetWindowDimensionAsync() => await _jsRuntime.InvokeAsync<AreaDimension>("getWindowDimensions");

}