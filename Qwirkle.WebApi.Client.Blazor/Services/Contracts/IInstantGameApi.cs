namespace Qwirkle.WebApi.Client.Blazor.Services.Contracts;

public interface IInstantGameApi
{
    public Task<InstantGameModel> JoinInstantGame(int playersNumber);
}