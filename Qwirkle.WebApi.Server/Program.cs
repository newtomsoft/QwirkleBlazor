var appBuilder = WebApplication.CreateBuilder(args);
var cors = new Cors();
appBuilder.Configuration.GetSection("Cors").Bind(cors);
appBuilder.Host.UseSerilog((_, configuration) => configuration.ReadFrom.Configuration(appBuilder.Configuration));
appBuilder.Services.AddDbContext<DefaultDbContext>(options => options.UseSqlServer(appBuilder.Configuration.GetConnectionString("Qwirkle")));
appBuilder.Services.AddSignalR();
appBuilder.Services.AddSingleton<INotification, SignalRNotification>();
appBuilder.Services.AddSingleton<InstantGameService>();
appBuilder.Services.AddScoped<IRepository, Repository>();
appBuilder.Services.AddScoped<IAuthentication, Authentication>();
appBuilder.Services.AddScoped<UserService>();
appBuilder.Services.AddScoped<CoreService>();
appBuilder.Services.AddScoped<InfoService>();
appBuilder.Services.AddScoped<BotService>();
appBuilder.Services.AddControllers();
appBuilder.Services.AddIdentity();
appBuilder.Services.AddQwirkleCors(cors);
appBuilder.Services.AddOptions();
appBuilder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/login");
appBuilder.Services.AddSession();

var app = appBuilder.Build();

if (app.Environment.IsDevelopment()) app.UseWebAssemblyDebugging();
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
app.UseQwirkleCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); endpoints.MapHub<HubQwirkle>("/hubGame"); });
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
