using Home.Recipes.WebClient.Services.Recipes;
using Microsoft.AspNetCore.Components;

namespace Home.Recipes.WebClient.Pages;

public partial class RecipeDetailPage
{
    private readonly RecipeService _recipeService;

    public RecipeDetailPage(RecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    [Parameter]
    public Guid RecipeId { get; set; }
}
