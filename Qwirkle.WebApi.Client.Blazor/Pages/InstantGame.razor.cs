namespace Qwirkle.WebApi.Client.Blazor.Pages;

public partial class InstantGame
{
    [Inject]
    private NavigationManager _navigationManager { get; set; }

    [CascadingParameter]
    private Task<AuthenticationState> authenticationStateTask { get; set; }
}
