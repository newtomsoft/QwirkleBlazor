namespace Qwirkle.WebApi.Client.Blazor.Services.Implementations;

public interface IIdentityAuthenticationStateProvider
{
    Task Login(LoginParameters loginParameters);
    Task Register(RegisterParameters registerParameters);
    Task RegisterGuest();
    Task Logout();
}