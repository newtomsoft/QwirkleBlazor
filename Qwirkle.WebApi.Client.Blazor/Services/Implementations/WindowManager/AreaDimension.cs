namespace Qwirkle.WebApi.Client.Blazor.Services.Implementations.WindowManager;

public class AreaDimension
{
    public int Width { get; init; }
    public int Height { get; init; }

    public WindowOrientation Orientation => Width > Height ? WindowOrientation.Landscape : WindowOrientation.Portrait;

    public AreaDimension ReducedByWidth(int width) => new() { Width = Width - width, Height = Height };
    public AreaDimension ReducedByHeight(int height) => new() { Width = Width, Height = Height - height };
    public AreaDimension ReducedByArea(AreaDimension area) => new() { Width = Width - area.Width, Height = Height - area.Height };
}