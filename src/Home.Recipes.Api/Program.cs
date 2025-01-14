using Home.Recipes.Api.Infrastructure;
using Microsoft.Extensions.FileProviders;
using Wolverine;
using Wolverine.FluentValidation;
using Wolverine.Http.FluentValidation;

const string corsPolicyName = "AllowWebClient";

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddDebug();
builder.Logging.AddConsole();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Host.UseWolverine(opts =>
{
    opts.UseFluentValidation();
});

builder.Services.AddCors(options =>
{
    var allowedWebClientUrl = builder.Configuration.GetValue<string>("AllowedWebClientUrl");
    options.AddPolicy(corsPolicyName, _ => _
        .WithOrigins(allowedWebClientUrl!)
        .AllowAnyMethod()
        .AllowAnyHeader());
});

var app = builder.Build();

app.UseCors(corsPolicyName);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "StaticFiles")),
    RequestPath = "/StaticFiles"
});
app.MapWolverineEndpoints(opts => { opts.UseFluentValidationProblemDetailMiddleware(); });

app.Run();
