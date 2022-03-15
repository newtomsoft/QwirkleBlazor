namespace Qwirkle.WebApi.Client.Blazor.Services.Contracts;

public interface IAuthorizeApi
{
    Task Login(LoginModel loginModel);
    Task Register(RegisterModel registerModel);
    Task RegisterGuest();
    Task Logout();
    Task<UserInfo> GetUserInfo();
}