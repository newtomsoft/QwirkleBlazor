namespace Qwirkle.WebApi.Client.Blazor.Tests.ComponentTests;

public static class ComponentFactory<T> where T : ComponentBase
{
    public static IRenderedComponent<T> RenderComponent(bool isAuthorized)
    {
        var (renderedComponent, _) = RenderComponentWithNavigationManager(isAuthorized);
        return renderedComponent;
    }

    public static IRenderedComponent<T> RenderComponent(bool isAuthorized, out FakeNavigationManager navigationManager)
    {
        var (renderedComponent, fakeNavigationManager) = RenderComponentWithNavigationManager(isAuthorized);
        navigationManager = fakeNavigationManager;
        return renderedComponent;
    }

    private static (IRenderedComponent<T>, FakeNavigationManager) RenderComponentWithNavigationManager(bool isAuthorized)
    {
        var context = new TestContext();
        if (isAuthorized) context.AddTestAuthorization().SetAuthorized("userName");
        else context.AddTestAuthorization().SetNotAuthorized();
        var authorizeApi = new Mock<IAuthorizeApi>();
        context.Services.AddSingleton(authorizeApi.Object);
        var identityAuthenticationStateProvider = new Mock<IdentityAuthenticationStateProvider>(authorizeApi.Object);
        context.Services.AddSingleton(identityAuthenticationStateProvider.Object);
        var navigationManager = context.Services.GetRequiredService<FakeNavigationManager>();
        var component = context.RenderComponent<T>();
        return (component, navigationManager);
    }
}