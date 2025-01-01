using Home.Recipes.WebClient.Services.Recipes;
using Home.Recipes.WebClient.Services.Recipes.Models;
using Microsoft.AspNetCore.Components;

namespace Home.Recipes.WebClient.Pages;

public partial class RecipeDetailPage
{
    private readonly RecipeService _recipeService;
    private readonly NavigationManager _navigationManager;
    private RecipeDetailResponse? _recipeDetail;

    public RecipeDetailPage(RecipeService recipeService, NavigationManager navigationManager)
    {
        _recipeService = recipeService;
        _navigationManager = navigationManager;
    }

    [Parameter]
    public Guid RecipeId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _recipeDetail = await _recipeService.GetRecipeDetailsAsync(RecipeId);
        await base.OnInitializedAsync();
    }
}
