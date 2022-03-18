namespace Qwirkle.WebApi.Client.Blazor.Services.Implementations;

public class UserApi : BaseApi, IUserApi
{
    protected override string ControllerName => "User";

    public UserApi(HttpClient httpClient) : base(httpClient) { }

    public async Task Login(LoginModel loginModel)
    {
        var result = await _httpClient.PostAsJsonAsync($"{ControllerName}/Login", loginModel);
        if (result.StatusCode == HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
        result.EnsureSuccessStatusCode();
    }

    public async Task Logout()
    {
        var result = await _httpClient.PostAsync($"{ControllerName}/Logout", null);
        result.EnsureSuccessStatusCode();
    }

    public async Task Register(RegisterModel registerModel)
    {
        var result = await _httpClient.PostAsJsonAsync($"{ControllerName}/Register", registerModel);
        if (result.StatusCode == HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
        result.EnsureSuccessStatusCode();
    }

    public async Task RegisterGuest()
    {
        var result = await _httpClient.GetAsync($"{ControllerName}/RegisterGuest");
        if (result.StatusCode == HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
        result.EnsureSuccessStatusCode();
    }

    public async Task<UserInfo> GetUserInfo() => await _httpClient.GetFromJsonAsync<UserInfo>($"{ControllerName}/Info");


    public void NotifyAuthenticationStateChanged(Task<AuthenticationState> task)
    {
        throw new NotImplementedException();
    }

    public Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        throw new NotImplementedException();
    }
}