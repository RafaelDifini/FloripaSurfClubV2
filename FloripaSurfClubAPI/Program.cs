using FloripaSurfClubAPI;
using FloripaSurfClubAPI.Endpoints;
using FloripaSurfClubAPI.Extensions;
using FloripaSurfClubAPI.Models;
using FloripaSurfClubCore.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();

var app = builder.Build();

app.UseCors(ApiConfiguration.CorsPolicyName);
app.ConfigureDevEnvironment();
app.MapEndpoints();
app.Run();
