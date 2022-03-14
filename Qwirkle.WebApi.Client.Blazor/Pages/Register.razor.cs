namespace Qwirkle.WebApi.Client.Blazor.Pages;

public partial class Register
{
    RegisterParameters RegisterParameters { get; set; } = new();
    string Error { get; set; }

    async Task RegisterUser()
    {
        Error = null;
        try
        {
            await authStateProvider.Register(RegisterParameters);
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
