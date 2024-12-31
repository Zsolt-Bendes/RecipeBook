using Home.Recipes.WebClient.Services.Recipes;
using Home.Recipes.WebClient.Services.Recipes.Models;

namespace Home.Recipes.WebClient.Pages;

public partial class Home
{
    private readonly RecipeService _recipeService;

    private RecipeListQueryResponse? _recipeList;

    public Home(RecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    protected override async Task OnInitializedAsync()
    {
        await SearchRecipesAsync(null);

        await base.OnInitializedAsync();
    }

    private async Task SearchRecipesAsync(string? text)
    {
        _recipeList = await _recipeService.GetRecipesAsync(text);
    }
}
