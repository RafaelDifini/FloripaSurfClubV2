using FloripaSurfClubAPI.Handlers;
using FloripaSurfClubAPI.Models.Account;
using FloripaSurfClubCore;
using FloripaSurfClubCore.Data;
using FloripaSurfClubCore.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Text.Json.Serialization;

namespace FloripaSurfClubAPI.Extensions
{
    public static class BuilderExtensions
    {
        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            ApiConfiguration.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
            Configuration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
            Configuration.FrontendUrl = builder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;
        }

        public static void AddDocumentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(x =>
            {
                x.CustomSchemaIds(n => n.FullName);
            });
        }


        public static void AddSecurity(this WebApplicationBuilder builder)
        {
            builder.Services
           .AddAuthentication(IdentityConstants.ApplicationScheme)
           .AddIdentityCookies();

            builder.Services.AddAuthorization();
        }

        public static void AddDataContexts(this WebApplicationBuilder builder)
        {
            var cultureInfo = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            var brazilTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


            builder.Services.AddDbContext<FloripaSurfClubContextV2>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Configurar Identity
            builder.Services
              .AddIdentityCore<UsuarioSistema>()
              .AddRoles<IdentityRole<Guid>>()
              .AddEntityFrameworkStores<FloripaSurfClubContextV2>()
            .AddApiEndpoints();

            //builder.Services.AddIdentity<UsuarioSistema, IdentityRole<Guid>>()
            // .AddEntityFrameworkStores<FloripaSurfClubContextV2>()
            // .AddDefaultTokenProviders();


            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(6);
                options.LoginPath = "/Account/Login";
                options.SlidingExpiration = true;

                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                    var jsonResponse = new
                    {
                        message = "Você não está autenticado. Faça login para continuar."
                    };

                    // Escreve a resposta JSON
                    context.Response.ContentType = "application/json";
                    return context.Response.WriteAsJsonAsync(jsonResponse);
                };
            });


            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

        }

        public static void AddCrossOrigin(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(
            options => options.AddPolicy(
                    ApiConfiguration.CorsPolicyName,
                    policy => policy
                    .WithOrigins([
                        Configuration.BackendUrl,
                        Configuration.FrontendUrl
                        ])
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                ));
        }

        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAulasHandler, AulaHandler>();
            builder.Services.AddScoped<IProfessorHandler, ProfessorHandler>();
            builder.Services.AddScoped<IClienteHandler, ClienteHandler>();
            builder.Services.AddScoped<IAccountHandler, AccountHandler>();
            builder.Services.AddScoped<IAlunoHandler, AlunoHandler>();
            builder.Services.AddScoped<IAtendenteHandler, AtendenteHandler>();
            builder.Services.AddScoped<ICaixaHandler, CaixaHandler>();

        }

        public static void AddSecrets(this WebApplicationBuilder builder)
        {
            builder.Configuration.AddUserSecrets<Program>();
        }
    }

}
