namespace Qwirkle.WebApi.Client.Blazor.Tests.ComponentTests;

public class LoginTests
{
    [Fact]
    public void LoginWithoutLoginAndPassword()
    {
        var loginComponent = ComponentFactory<Login>.RenderComponent(false);
        loginComponent.Find("#btnSignIn").Click();

        loginComponent.Find("#validationUserName").Text().ShouldNotBe("");
        loginComponent.Find("#validationPassword").Text().ShouldNotBe("");
    }


    [Fact]
    public void LoginAsRegisteredUser()
    {
        var loginComponent = ComponentFactory<Login>.RenderComponent(false, out var navigationManager);
        loginComponent.Find("#inputUsername").Change("userTest");
        loginComponent.Find("#inputPassword").Change("passwordTest");
        loginComponent.Find("#btnSignIn").Click();

        var newUri = new Uri(navigationManager.Uri);
        newUri.LocalPath.ShouldBe(Page.Home);
    }


    [Fact]
    public void LoginAsGuestShouldRedirectToInstantGame()
    {
        var loginComponent = ComponentFactory<Login>.RenderComponent(false, out var navigationManager);
        loginComponent.Find("#btnLoginAsGuest").Click();

        var newUri = new Uri(navigationManager.Uri);
        newUri.LocalPath.ShouldBe(Page.InstantGame);
    }
}