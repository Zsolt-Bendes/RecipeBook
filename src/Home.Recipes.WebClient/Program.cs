using Blazored.Modal;
using Home.Recipes.WebClient;
using Home.Recipes.WebClient.Services.Recipes;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Tewr.Blazor.FileReader;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient<RecipeService>(httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.Configuration.GetConnectionString("WebApi")!);
});

builder.Services.AddFileReaderService(options => options.UseWasmSharedBuffer = true);

builder.Services.AddBlazoredModal();

await builder.Build().RunAsync();
