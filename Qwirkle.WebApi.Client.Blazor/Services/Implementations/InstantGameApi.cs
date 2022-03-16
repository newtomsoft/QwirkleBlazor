namespace Qwirkle.WebApi.Client.Blazor.Services.Implementations;

public class InstantGameApi : IInstantGameApi
{
    private readonly HttpClient _httpClient;
    private const string ControllerName = "InstantGame";

    public InstantGameApi(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<InstantGameModel> JoinInstantGame(int playersNumber)
    {
        var response = await _httpClient.GetAsync($"{ControllerName}/Join/{playersNumber}");
        if (response.StatusCode == HttpStatusCode.BadRequest) throw new Exception(await response.Content.ReadAsStringAsync());
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<InstantGameModel>();
    }
}