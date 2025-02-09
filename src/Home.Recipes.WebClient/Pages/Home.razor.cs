using Home.Recipes.WebClient.Components.Modals;
using Home.Recipes.WebClient.Services.Recipes;
using Home.Recipes.WebClient.Services.Recipes.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace Home.Recipes.WebClient.Pages;

public partial class Home
{
    private readonly RecipeService _recipeService;
    private readonly NavigationManager _navigationManager;
    private readonly IDialogService _dialogService;
    private readonly string _serverUrl;

    private RecipeListQueryResponse? _recipeList;
    private string? _searchText;

    public Home(
        RecipeService recipeService,
        NavigationManager navigationManager,
        IConfiguration configuration,
        IDialogService dialogService)
    {
        _recipeService = recipeService;
        _navigationManager = navigationManager;
        _dialogService = dialogService;
        _serverUrl = configuration.GetConnectionString("WebApi")!;
    }

    protected override async Task OnInitializedAsync()
    {
        await SearchRecipesAsync();

        await base.OnInitializedAsync();
    }

    private async Task SearchRecipesAsync()
    {
        _recipeList = await _recipeService.GetRecipesAsync(_searchText);
    }

    private void NavigateToRecipeDetails(Guid recipeId)
    {
        _navigationManager.NavigateTo($"/recipes/{recipeId}");
    }

    private async Task OpenCreateDialog()
    {
        var dialog = await _dialogService.ShowDialogAsync<CreateRecipeModal>(new DialogParameters()
        {
            Title = "Add new recipe",
            PreventDismissOnOverlayClick = true,
            PreventScroll = true,
        });
    }
}
