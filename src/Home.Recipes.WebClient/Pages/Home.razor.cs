using Blazored.Modal.Services;
using Home.Recipes.WebClient.Services.Recipes;
using Home.Recipes.WebClient.Services.Recipes.Models;
using Microsoft.AspNetCore.Components;

namespace Home.Recipes.WebClient.Pages;

public partial class Home
{
    private readonly RecipeService _recipeService;
    private readonly NavigationManager _navigationManager;
    private readonly string _serverUrl;

    private RecipeListQueryResponse? _recipeList;


    public Home(RecipeService recipeService, NavigationManager navigationManager, IConfiguration configuration)
    {
        _recipeService = recipeService;
        _navigationManager = navigationManager;
        _serverUrl = configuration.GetConnectionString("WebApi")!;
    }

    [CascadingParameter]
    public IModalService Modal { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await SearchRecipesAsync(null);

        await base.OnInitializedAsync();
    }

    private async Task SearchRecipesAsync(string? text)
    {
        _recipeList = await _recipeService.GetRecipesAsync(text);
    }

    private void NavigateToRecipeDetails(Guid recipeId)
    {
        _navigationManager.NavigateTo($"/recipes/{recipeId}");
    }
}
