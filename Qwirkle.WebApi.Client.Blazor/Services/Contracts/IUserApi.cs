namespace Qwirkle.WebApi.Client.Blazor.Services.Contracts;

public interface IUserApi
{
    Task Login(LoginModel loginModel);
    Task Register(RegisterModel registerModel);
    Task RegisterGuest();
    Task Logout();
    Task<UserInfo> GetUserInfo();
}