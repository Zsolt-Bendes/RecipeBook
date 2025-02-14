using Home.Recipes.WebClient.Services.RecipeHistory;
using Home.Recipes.WebClient.Services.RecipeHistory.Models;
using Microsoft.AspNetCore.Components;

namespace Home.Recipes.WebClient.Pages;

public partial class RecipeHistory
{
    private readonly RecipeHistoryService _recipeHistoryService;
    private readonly NavigationManager _navigationManager;

    private RecipeHistoryQueryResponse? _recipeHistories;
    private string? _searchText;

    public RecipeHistory(RecipeHistoryService recipeHistoryService, NavigationManager navigationManager)
    {
        _recipeHistoryService = recipeHistoryService;
        _navigationManager = navigationManager;
    }

    override protected async Task OnInitializedAsync()
    {
        await SearchRecipesAsync();
        await base.OnInitializedAsync();
    }

    private async Task SearchRecipesAsync()
    {
        _recipeHistories = await _recipeHistoryService.GetRecipeHistoriesAsync(_searchText);
    }

    private void NavigateBack()
    {
        _navigationManager.NavigateTo("/");
    }
}
