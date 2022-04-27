namespace Qwirkle.WebApi.Client.Blazor.Tests.Arrange;

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

    public static IRenderedComponent<T> RenderComponent<TU>(TU api, out FakeNavigationManager fakeNavigationManager) where TU : class
    {
        (var renderedComponent, fakeNavigationManager) = RenderComponentWithNavigationManager(api);
        return renderedComponent;
    }

    public static IRenderedComponent<T> RenderComponent<TU, TV>(TU tuApi, TV tvApi, out FakeNavigationManager fakeNavigationManager) where TU : class where TV : class
    {
        (var renderedComponent, fakeNavigationManager) = RenderComponentWithNavigationManager(tuApi, tvApi);
        return renderedComponent;
    }

    public static IRenderedComponent<T> RenderComponent<TU, TV, TW>(TU tuApi, TV tvApi, TW twApi, out FakeNavigationManager fakeNavigationManager) where TU : class where TV : class where TW : class
    {
        (var renderedComponent, fakeNavigationManager) = RenderComponentWithNavigationManager(tuApi, tvApi, twApi);
        return renderedComponent;
    }

    public static IRenderedComponent<T> RenderComponent<TU, TV, TW, TX>(TU tuApi, TV tvApi, TW twApi, TX txApi, out FakeNavigationManager fakeNavigationManager) where TU : class where TV : class where TW : class where TX : class
    {
        (var renderedComponent, fakeNavigationManager) = RenderComponentWithNavigationManager(tuApi, tvApi, twApi, txApi);
        return renderedComponent;
    }


    private static (IRenderedComponent<T>, FakeNavigationManager) RenderComponentWithNavigationManager(bool isAuthorized)
    {
        var context = new TestContext();
        if (isAuthorized) context.AddTestAuthorization().SetAuthorized("userName");
        else context.AddTestAuthorization().SetNotAuthorized();
        var userApi = new Mock<IApiUser>();
        context.Services.AddSingleton(userApi.Object);
        var identityAuthenticationStateProvider = new Mock<IdentityAuthenticationStateProvider>(userApi.Object);
        context.Services.AddSingleton(identityAuthenticationStateProvider.Object);
        var navigationManager = context.Services.GetRequiredService<FakeNavigationManager>();

        var component = context.RenderComponent<T>();
        return (component, navigationManager);
    }

    private static (IRenderedComponent<T>, FakeNavigationManager) RenderComponentWithNavigationManager<TU>(TU serviceInstance) where TU : class
    {
        var context = new TestContext();
        AddService(serviceInstance, context);

        AddUserAuthorizationService(context);
        return Render(context);
    }

    private static (IRenderedComponent<T>, FakeNavigationManager) RenderComponentWithNavigationManager<TU, TV>(TU tuServiceInstance, TV tvServiceInstance) where TU : class where TV : class
    {
        var context = new TestContext();
        AddService(tuServiceInstance, context);
        AddService(tvServiceInstance, context);
        AddUserAuthorizationService(context);
        return Render(context);
    }

    private static (IRenderedComponent<T>, FakeNavigationManager) RenderComponentWithNavigationManager<TU, TV, TW>(TU tuServiceInstance, TV tvServiceInstance, TW twServiceInstance) where TU : class where TV : class where TW : class
    {
        var context = new TestContext();
        AddService(tuServiceInstance, context);
        AddService(tvServiceInstance, context);
        AddService(twServiceInstance, context);
        AddUserAuthorizationService(context);
        return Render(context);
    }

    private static (IRenderedComponent<T>, FakeNavigationManager) RenderComponentWithNavigationManager<TU, TV, TW, TX>(TU tuServiceInstance, TV tvServiceInstance, TW twServiceInstance, TX txServiceInstance) where TU : class where TV : class where TW : class where TX : class
    {
        var context = new TestContext();
        AddService(tuServiceInstance, context);
        AddService(tvServiceInstance, context);
        AddService(twServiceInstance, context);
        AddService(txServiceInstance, context);
        AddUserAuthorizationService(context);
        return Render(context);
    }

    private static (IRenderedComponent<T>, FakeNavigationManager) Render(TestContext context)
    {
        var navigationManager = context.Services.GetRequiredService<FakeNavigationManager>();
        var component = context.RenderComponent<T>();
        return (component, navigationManager);
    }

    private static void AddUserAuthorizationService(TestContext context)
    {
        context.AddTestAuthorization().SetAuthorized("userName");
        context.Services.AddSingleton(new Mock<INotificationInstantGame>().Object);

        var snackBar = new Mock<ISnackbar>();
        snackBar.Setup(x => x.Add(It.IsAny<string>(), It.IsAny<Severity>(), null)).Verifiable();
        context.Services.AddSingleton(snackBar.Object);

        var userApi = new Mock<IApiUser>();
        context.Services.AddSingleton(userApi.Object);
        var identityAuthenticationStateProvider = new Mock<IdentityAuthenticationStateProvider>(userApi.Object);
        context.Services.AddSingleton(identityAuthenticationStateProvider.Object);
    }

    private static void AddService<TU>(TU serviceInstance, TestContext context) where TU : class => context.Services.AddSingleton(serviceInstance);
}
