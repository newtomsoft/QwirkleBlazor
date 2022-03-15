﻿namespace Qwirkle.WebApi.Client.Blazor.Tests.ComponentTests;

public class RegisterTests
{
    public static TheoryData<RegisterTestsInput> RegisterNewType => new()
    {
        new ("", new RegisterModel { Email = "user@test.fr", UserName = "userName", Password = "password", PasswordConfirm = "password" }),
        new ("#validationUserName", new RegisterModel { Email = "user@test.fr", Password = "password", PasswordConfirm = "password" }),
        new ("#validationEmail", new RegisterModel { Email = "badEmailAddress", UserName = "userName", Password = "password", PasswordConfirm = "password" }),
        new ("#validationEmail", new RegisterModel { UserName = "userName", Password = "password", PasswordConfirm = "password" }),
        new ("#validationPassword", new RegisterModel { Email = "user@test.fr", UserName = "userName", PasswordConfirm = "password" }),
        new ("#validationPassword", new RegisterModel { Email = "user@test.fr", UserName = "userName", Password = "tiny", PasswordConfirm = "tiny" }),
        new ("#validationPasswordConfirm", new RegisterModel { Email = "user@test.fr", UserName = "userName", Password = "password1", PasswordConfirm = "password2" }),
    };

    [Theory(DisplayName = "Register")]
    [MemberData(nameof(RegisterNewType))]
    public void Register(RegisterTestsInput input)
    {
        var loginComponent = ComponentFactory<Register>.RenderComponent(false, out var navigationManager);

        loginComponent.Find("#inputUsername").Change(input.RegisterModel.UserName);
        loginComponent.Find("#inputEmail").Change(input.RegisterModel.Email);
        loginComponent.Find("#inputPassword").Change(input.RegisterModel.Password);
        loginComponent.Find("#inputPasswordConfirm").Change(input.RegisterModel.PasswordConfirm);

        loginComponent.Find("#btnRegister").Click();

        if (string.IsNullOrEmpty(input.InputSelectorInError) is not true)
            loginComponent.Find(input.InputSelectorInError).Text().ShouldNotBe("");
        else
        {
            var newUri = new Uri(navigationManager.Uri);
            newUri.LocalPath.ShouldBe(Constant.PageHome);
        }
    }
}