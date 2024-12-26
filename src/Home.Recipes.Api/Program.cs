using Home.Recipes.Api.Infrastructure;
using Wolverine;
using Wolverine.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddDebug();
builder.Logging.AddConsole();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Host.UseWolverine();

var app = builder.Build();

app.MapWolverineEndpoints(opts => { });

app.Run();
