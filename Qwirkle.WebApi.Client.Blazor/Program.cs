var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddSingleton(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<IdentityAuthenticationStateProvider>();
builder.Services.AddSingleton<AuthenticationStateProvider>(s => s.GetRequiredService<IdentityAuthenticationStateProvider>());
builder.Services.AddSingleton<IUserApi, UserApi>();
builder.Services.AddSingleton<IActionApi, ActionApi>();
builder.Services.AddSingleton<IGameApi, GameApi>();
builder.Services.AddSingleton<IInstantGameApi, InstantGameApi>();
builder.Services.AddSingleton<INotification, Notification>();

await builder.Build().RunAsync();