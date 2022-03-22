namespace Qwirkle.WebApi.Client.Blazor.Services.Contracts.WindowManager;

public interface IAreaManager
{
    Task OnTileInBoardDropped(object sender, TileInBoardDroppedEventArgs eventArgs);
    
    BoardLimit BoardLimit { get; }
    string BoardSquareSize { get; }

    Task OnBoardChanged(object source, BoardChangedEventArgs eventArgs);
    Task UpdateBoardSquareSizeAsync();
}