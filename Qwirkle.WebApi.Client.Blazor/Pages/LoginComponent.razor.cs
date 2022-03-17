namespace Qwirkle.WebApi.Client.Blazor.Pages;

public partial class LoginComponent
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
            NavigationManager.NavigateTo(Page.Home);
        }
        catch (Exception ex)
        {
            Error = ex.Message;
        }
    }

    private async Task LoginAsGuest()
    {
        await AuthStateProvider.RegisterGuest();
        NavigationManager.NavigateTo(Page.InstantGame);
    }
}

