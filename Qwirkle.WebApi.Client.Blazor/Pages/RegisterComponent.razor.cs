namespace Qwirkle.WebApi.Client.Blazor.Pages;

public partial class RegisterComponent
{
    [Inject] private NavigationManager? NavigationManager { get; set; }
    [Inject] private IdentityAuthenticationStateProvider? AuthStateProvider { get; set; }

    private RegisterModel RegisterModel { get; } = new() { SignInPersistent = true };
    private string Error { get; set; } = string.Empty;

    private async Task RegisterUser()
    {
        try
        {
            await AuthStateProvider!.Register(RegisterModel);
            NavigationManager!.NavigateTo(Page.Home);
        }
        catch (Exception ex)
        {
            Error = ex.Message;
        }
    }

    private async Task LoginAsGuest()
    {
        await AuthStateProvider!.RegisterGuest();
        NavigationManager!.NavigateTo(Page.InstantGame);
    }
}
