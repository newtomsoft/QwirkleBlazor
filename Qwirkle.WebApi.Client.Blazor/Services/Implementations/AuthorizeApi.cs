namespace Qwirkle.WebApi.Client.Blazor.Services.Implementations;

public class AuthorizeApi : IAuthorizeApi
{
    private readonly HttpClient _httpClient;

    public AuthorizeApi(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task Login(LoginParameters loginParameters)
    {
        var result = await _httpClient.PostAsJsonAsync("User/Login", loginParameters);
        if (result.StatusCode == HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
        result.EnsureSuccessStatusCode();
    }

    public async Task Logout()
    {
        var result = await _httpClient.PostAsync("User/Logout", null);
        result.EnsureSuccessStatusCode();
    }

    public async Task Register(RegisterParameters registerParameters)
    {
        var result = await _httpClient.PostAsJsonAsync("User/Register", registerParameters);
        if (result.StatusCode == HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
        result.EnsureSuccessStatusCode();
    }

    public async Task RegisterGuest()
    {
        var result = await _httpClient.GetAsync("User/RegisterGuest");
        if (result.StatusCode == HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
        result.EnsureSuccessStatusCode();
    }

    public async Task<UserInfo> GetUserInfo() => await _httpClient.GetFromJsonAsync<UserInfo>("User/Info");


    public void NotifyAuthenticationStateChanged(Task<AuthenticationState> task)
    {
        throw new NotImplementedException();
    }

    public Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        throw new NotImplementedException();
    }
}