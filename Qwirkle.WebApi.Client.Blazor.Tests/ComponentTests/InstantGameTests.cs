namespace Qwirkle.WebApi.Client.Blazor.Tests.ComponentTests;

public class InstantGameTests
{
    [Fact]
    public void AuthorizedTest()
    {
        using var testContext = new TestContext();
        var authContext = testContext.AddTestAuthorization();
        authContext.SetNotAuthorized();
        var component = testContext.RenderComponent<InstantGame>();

        var h1 = component.Find("h1");

    }
}