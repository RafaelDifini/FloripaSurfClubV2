using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FloripaSurfClubWeb;
using MudBlazor.Services;
using FloripaSurfClubWeb.Security;
using Microsoft.AspNetCore.Components.Authorization;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubWeb.Handlers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<CookieHandler>();

builder.Services.AddScoped<AuthenticationStateProvider,CookieAuthenticationStateProvider>();
builder.Services.AddScoped(x => (ICookieAuthenticationStateProvider)x.GetRequiredService<AuthenticationStateProvider>());

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMudServices();

builder.Services.AddHttpClient(Configuration.HttpClientName, opt =>
{
    opt.BaseAddress = new Uri(Configuration.BackendUrl);
}).AddHttpMessageHandler<CookieHandler>();

#region Handlers
builder.Services.AddTransient<IAccountHandler,AccountHandler>();
builder.Services.AddTransient<IAlunoHandler, AlunoHandler>();
builder.Services.AddTransient<IAtendenteHandler, AtendenteHandler>();
builder.Services.AddTransient<IAulasHandler, AulasHandler>();
builder.Services.AddTransient<ICaixaHandler, CaixaHandler>();
builder.Services.AddTransient<IClienteHandler, ClienteHandler>();
builder.Services.AddTransient<IProfessorHandler, ProfessorHandler>();
#endregion

await builder.Build().RunAsync();

