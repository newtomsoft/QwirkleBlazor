namespace Qwirkle.WebApi.Client.Blazor.Pages;

public partial class Login
{
    LoginParameters LoginParameters { get; set; } = new() { RememberMe = true };
    string Error { get; set; }

    async Task LoginAsUser()
    {
        Error = null;
        try
        {
            await authStateProvider.Login(LoginParameters);
            navigationManager.NavigateTo("");
        }
        catch (Exception ex)
        {
            Error = ex.Message;
        }
    }

    private async Task LoginAsGuest()
    {
        await authStateProvider.RegisterGuest();
        navigationManager.NavigateTo("");
    }
}

