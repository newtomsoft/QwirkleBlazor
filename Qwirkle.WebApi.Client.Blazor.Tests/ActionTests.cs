namespace Qwirkle.WebApi.Client.Blazor.Tests;

public class ActionTests
{
    [Fact]
    public void Test1()
    {
        using var testContext = new TestContext();
        var jsrMock = new Mock<IJSRuntime>();
        var authorizeApi = new Mock<IAuthorizeApi>();
        testContext.Services.AddSingleton(jsrMock.Object);
        testContext.Services.AddSingleton(authorizeApi.Object);
        var component = testContext.RenderComponent<Game>();
        
    }
}