using Home.Recipes.Api.Infrastructure;
using Wolverine;
using Wolverine.FluentValidation;
using Wolverine.Http;
using Wolverine.Http.FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddDebug();
builder.Logging.AddConsole();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Host.UseWolverine(opts => opts.UseFluentValidation());

var app = builder.Build();

app.MapWolverineEndpoints(opts => { opts.UseFluentValidationProblemDetailMiddleware(); });

app.Run();
