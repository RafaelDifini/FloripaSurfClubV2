using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FloripaSurfClubWeb;
using MudBlazor.Services;
using FloripaSurfClubWeb.Security;
using Microsoft.AspNetCore.Components.Authorization;
using FloripaSurfClubCore.Handlers;
using FloripaSurfClubWeb.Handlers;
using System.Globalization;

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
builder.Services.AddScoped<IAccountHandler,AccountHandler>();
builder.Services.AddScoped<IAlunoHandler, AlunoHandler>();
builder.Services.AddScoped<IAtendenteHandler, AtendenteHandler>();
builder.Services.AddScoped<IAulasHandler, AulasHandler>();
builder.Services.AddScoped<ICaixaHandler, CaixaHandler>();
builder.Services.AddScoped<IClienteHandler, ClienteHandler>();
builder.Services.AddScoped<IProfessorHandler, ProfessorHandler>();
#endregion

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("pr-BR");

await builder.Build().RunAsync();

