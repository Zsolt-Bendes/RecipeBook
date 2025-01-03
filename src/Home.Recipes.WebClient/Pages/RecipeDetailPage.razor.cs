﻿using Blazored.Modal;
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

    private RecipeDetailResponse? _recipeDetail;
    private bool _editMode;

    public RecipeDetailPage(RecipeService recipeService, NavigationManager navigationManager)
    {
        _recipeService = recipeService;
        _navigationManager = navigationManager;
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
}
