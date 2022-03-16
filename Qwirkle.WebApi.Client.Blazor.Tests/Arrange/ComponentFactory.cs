namespace Qwirkle.WebApi.Client.Blazor.Tests.Arrange;

public static class ComponentFactory<T, TU> where T : ComponentBase where TU : class
{
    public static IRenderedComponent<T> RenderComponent(TU api, out FakeNavigationManager fakeNavigationManager)
    {
        (var renderedComponent, fakeNavigationManager) = RenderComponentWithNavigationManager(api);
        return renderedComponent;
    }

    public static IRenderedComponent<T> RenderComponent(TU api)
    {
        var (renderedComponent, _) = RenderComponentWithNavigationManager(api);
        return renderedComponent;
    }

    private static (IRenderedComponent<T>, FakeNavigationManager) RenderComponentWithNavigationManager(TU api)
    {
        var context = new TestContext();
        context.Services.AddSingleton(api);
        context.AddTestAuthorization().SetAuthorized("userName");
        var notificationMock = new Mock<INotification>();
        context.Services.AddSingleton(notificationMock.Object);

        var userApi = new Mock<IUserApi>();
        context.Services.AddSingleton(userApi.Object);
        var identityAuthenticationStateProvider = new Mock<IdentityAuthenticationStateProvider>(userApi.Object);
        context.Services.AddSingleton(identityAuthenticationStateProvider.Object);
        var navigationManager = context.Services.GetRequiredService<FakeNavigationManager>();
        var component = context.RenderComponent<T>();
        return (component, navigationManager);
    }
}

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
        var userApi = new Mock<IUserApi>();
        context.Services.AddSingleton(userApi.Object);
        var identityAuthenticationStateProvider = new Mock<IdentityAuthenticationStateProvider>(userApi.Object);
        context.Services.AddSingleton(identityAuthenticationStateProvider.Object);
        var navigationManager = context.Services.GetRequiredService<FakeNavigationManager>();
        
        var component = context.RenderComponent<T>();
        return (component, navigationManager);
    }
}