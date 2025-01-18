using Blazored.Modal;
using Blazored.Modal.Services;
using Home.Recipes.WebClient.Components.Modals;
using Home.Recipes.WebClient.Services.Recipes;
using Home.Recipes.WebClient.Services.Recipes.Models;
using Microsoft.AspNetCore.Components;

namespace Home.Recipes.WebClient.Pages;

public partial class RecipeDetailPage
{
    private readonly RecipeService _recipeService;
    private readonly NavigationManager _navigationManager;
    private readonly string _serverUrl;

    private RecipeDetailResponse? _recipeDetail;

    public RecipeDetailPage(RecipeService recipeService, NavigationManager navigationManager, IConfiguration configuration)
    {
        _recipeService = recipeService;
        _navigationManager = navigationManager;
        _serverUrl = configuration.GetConnectionString("WebApi")!;
    }

    [Parameter]
    public Guid RecipeId { get; set; }

    [CascadingParameter] IModalService Modal { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _recipeDetail = await _recipeService.GetRecipeDetailsAsync(RecipeId);
        await base.OnInitializedAsync();
    }

    private async Task DeleteRecipeAsync()
    {
        var confirmationDialog = Modal.Show<DeleteRecipeConfirmationModal>("Delete Recipe");
        var result = await confirmationDialog.Result;

        if (result.Confirmed)
        {
            await _recipeService.DeleteRecipeAsync(RecipeId);
            _navigationManager.NavigateTo("/");
        }
    }

    private async Task AddRecipeStepAsync()
    {
        var parameters = new ModalParameters().Add(nameof(AddRecipeStepModal.RecipeId), RecipeId);

        var dialog = Modal.Show<AddRecipeStepModal>("Add new step", parameters);
        var result = await dialog.Result;

        if (result.Confirmed)
        {
            var command = result.Data as AddRecipeStepCommand;
            await _recipeService.AddRecipeStepAsync(command!);

            _recipeDetail!.Steps.Add(command!.Text);
        }
    }

    private async Task RemoveRecipeStepAsync(int index)
    {
        var confirmationDialog = Modal.Show<DeleteRecipeConfirmationModal>("Remove step");
        var result = await confirmationDialog.Result;

        if (result.Confirmed)
        {
            await _recipeService.RemoveRecipeStepAsync(new RemoveRecipeStepCommand(RecipeId, index));
            _recipeDetail!.Steps.RemoveAt(index);
        }
    }

    private async Task EditRecipeAsync(int index)
    {
        var currentText = _recipeDetail!.Steps[index];
        var parameters = new ModalParameters()
            .Add(nameof(EditRecipeStepModal.RecipeId), RecipeId)
            .Add(nameof(EditRecipeStepModal.Index), index)
            .Add(nameof(EditRecipeStepModal.Text), currentText);

        var dialog = Modal.Show<EditRecipeStepModal>("Edit step", parameters);
        var result = await dialog.Result;

        if (result.Confirmed)
        {
            var command = result.Data as ChangeRecipeStepCommand;
            _recipeDetail!.Steps[index] = command!.Text;
        }
    }

    private async Task AddRecipeIngredientAsync()
    {
        var parameters = new ModalParameters()
           .Add(nameof(EditRecipeStepModal.RecipeId), RecipeId);

        var dialog = Modal.Show<AddRecipeIngredientModal>("Add new ingredient", parameters);
        var result = await dialog.Result;

        if (result.Confirmed)
        {
            var command = result.Data as AddIngredientCommand;
            _recipeDetail!.Ingredients.Add(new Ingredient(
                command!.Name,
                command.IngredientType,
                command.ValueBase));

        }
    }

    private async Task EditRecipeIngredientAsync(int index)
    {
        var currentIngredient = _recipeDetail!.Ingredients[index];
        var parameters = new ModalParameters()
            .Add(nameof(EditRecipeIngredientModal.RecipeId), RecipeId)
            .Add(nameof(EditRecipeIngredientModal.Index), index)
            .Add(nameof(EditRecipeIngredientModal.Name), currentIngredient.Name)
            .Add(nameof(EditRecipeIngredientModal.Value), currentIngredient.Value);

        var dialog = Modal.Show<EditRecipeIngredientModal>("Edit ingredient", parameters);
        var result = await dialog.Result;

        if (result.Confirmed)
        {
            var command = result.Data as ChangeRecipeIngredientCommand;
            _recipeDetail!.Ingredients[index].Value = command!.Value;
            _recipeDetail!.Ingredients[index].Name = command.Name;
        }
    }

    private async Task RemoveRecipeIngredientAsync(int index)
    {
        var confirmationDialog = Modal.Show<DeleteRecipeConfirmationModal>("Remove ingredient");
        var result = await confirmationDialog.Result;

        if (result.Confirmed)
        {
            await _recipeService.RemoveRecipeIngredientAsync(new RemoveIngredientCommand(RecipeId, index));
            _recipeDetail!.Ingredients.RemoveAt(index);
        }
    }
}
