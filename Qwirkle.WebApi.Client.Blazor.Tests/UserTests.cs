//using AngleSharp.Dom;

//namespace Qwirkle.WebApi.Client.Blazor.Tests;

//public class UserTests
//{
//    [Fact]
//    public void LoginWithoutLoginAndPassword()
//    {
//        using var testContext = new TestContext();
//        var jsrMock = new Mock<IJSRuntime>();
//        var authorizeApi = new Mock<IAuthorizeApi>();
//        var identityAuthenticationStateProvider = new Mock<IdentityAuthenticationStateProvider>();
//        testContext.Services.AddSingleton(jsrMock.Object);
//        testContext.Services.AddSingleton(authorizeApi.Object);
//        testContext.Services.AddSingleton(identityAuthenticationStateProvider.Object);
//        var loginComponent = testContext.RenderComponent<Login>();

//        loginComponent.Find("#btnSignIn").Click();

//        loginComponent.Find("#validationUserName").Text().ShouldNotBe("");
//        loginComponent.Find("#validationPassword").Text().ShouldNotBe("");
//    }

//    [Fact]
//    public void LoginAsRegisteredUser()
//    {
//        using var testContext = new TestContext();
//        var jsrMock = new Mock<IJSRuntime>();
//        var authorizeApi = new Mock<IAuthorizeApi>();
//        var identityAuthenticationStateProvider = new Mock<IIdentityAuthenticationStateProvider>();
//        testContext.Services.AddSingleton(jsrMock.Object);
//        testContext.Services.AddSingleton(authorizeApi.Object);
//        testContext.Services.AddSingleton(identityAuthenticationStateProvider.Object);
//        var loginComponent = testContext.RenderComponent<Login>();

//        var elementUserName = loginComponent.Find("#inputUsername");
//        elementUserName.TextContent = "userNameTest";

//        var elementPassword = loginComponent.Find("#inputPassword");
//        elementPassword.TextContent = "passwordTest";

//        loginComponent.Find("#btnSignIn").Click();

//        loginComponent.Find("#validationUserName").Text().ShouldBe("");
//        loginComponent.Find("#validationPassword").Text().ShouldBe("");
//    }

//    [Fact]
//    public void LoginAsGuest()
//    {
//        using var testContext = new TestContext();
//        var jsrMock = new Mock<IJSRuntime>();
//        var authorizeApi = new Mock<IAuthorizeApi>();
//        var identityAuthenticationStateProvider = new Mock<IIdentityAuthenticationStateProvider>();
//        testContext.Services.AddSingleton(jsrMock.Object);
//        testContext.Services.AddSingleton(authorizeApi.Object);
//        testContext.Services.AddSingleton(identityAuthenticationStateProvider.Object);
//        var loginComponent = testContext.RenderComponent<Login>();
        
//        var btnLogin = loginComponent.Find("#btnLoginAsGuest");
//        btnLogin.Click();

//        var indexComponent = testContext.RenderComponent<Index>();
//        var h1 = indexComponent.Find("<h1>");
        
//    }


//}