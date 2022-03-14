﻿namespace Qwirkle.WebApi.Client.Blazor.Services.Contracts;

public interface IAuthorizeApi
{
    Task Login(LoginParameters loginParameters);
    Task Register(RegisterParameters registerParameters);
    Task RegisterGuest();
    Task Logout();
    Task<UserInfo> GetUserInfo();
}