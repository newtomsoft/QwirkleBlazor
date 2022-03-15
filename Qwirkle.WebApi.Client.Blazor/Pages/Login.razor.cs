﻿namespace Qwirkle.WebApi.Client.Blazor.Pages;

public partial class Login
{
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private IdentityAuthenticationStateProvider AuthStateProvider { get; set; }

    private LoginModel LoginModel { get; } = new() { RememberMe = true };
    private string Error { get; set; } = string.Empty;


    private async Task LoginAsUser()
    {
        try
        {
            await AuthStateProvider.Login(LoginModel);
            NavigationManager.NavigateTo(Constant.PageHome);
        }
        catch (Exception ex)
        {
            Error = ex.Message;
        }
    }

    private async Task LoginAsGuest()
    {
        await AuthStateProvider.RegisterGuest();
        NavigationManager.NavigateTo(Constant.PageInstantGame);
    }
}

