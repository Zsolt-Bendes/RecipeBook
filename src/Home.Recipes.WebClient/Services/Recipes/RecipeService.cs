using Home.Recipes.WebClient.Services.Recipes.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace Home.Recipes.WebClient.Services.Recipes;

public sealed class RecipeService
{
    private readonly static JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    private readonly HttpClient _httpClient;

    public RecipeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<RecipeListQueryResponse?> GetRecipesAsync(string? searchText, CancellationToken cancellationToken = default)
    {
        var url = "/recipes";
        if (!string.IsNullOrEmpty(searchText))
        {
            url += $"?searchText={searchText}";
        }

        var response = await _httpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await JsonSerializer.DeserializeAsync<RecipeListQueryResponse>(
            await response.Content.ReadAsStreamAsync(cancellationToken),
            _jsonSerializerOptions,
            cancellationToken);
    }

    public async Task<RecipeDetailResponse?> GetRecipeDetailsAsync(Guid recipeId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"/recipes/{recipeId}", cancellationToken);
        response.EnsureSuccessStatusCode();

        return await JsonSerializer.DeserializeAsync<RecipeDetailResponse>(
           await response.Content.ReadAsStreamAsync(cancellationToken),
           _jsonSerializerOptions,
           cancellationToken);
    }

    public async Task<CreateRecipeResponse?> CreateRecipeAsync(CreateRecipeCommand command, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync($"/recipes/create", command, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await JsonSerializer.DeserializeAsync<CreateRecipeResponse>(
           await response.Content.ReadAsStreamAsync(cancellationToken),
           _jsonSerializerOptions,
           cancellationToken);
    }

    public async Task DeleteRecipeAsync(Guid recipeId, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.DeleteAsync($"/recipes/{recipeId}", cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task AddRecipeStepAsync(AddRecipeStepCommand command, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PutAsJsonAsync($"/recipes/addStep", command, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task RemoveRecipeStepAsync(RemoveRecipeStepCommand command, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PutAsJsonAsync($"/recipes/removeStep", command, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task ChangeRecipeStepAsync(ChangeRecipeStepCommand command, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PutAsJsonAsync($"/recipes/updateStep", command, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task AddRecipeIngredientAsync(AddIngredientCommand command, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PutAsJsonAsync($"/recipes/addIngredient", command, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task RemoveRecipeIngredientAsync(RemoveIngredientCommand command, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PutAsJsonAsync($"/recipes/removeIngredient", command, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task ChangePreparationTimeAsync(ChangePreparationTimeCommand command, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PutAsJsonAsync($"/recipes/changePreparationTime", command, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task ChangeCookingTimeAsync(ChangeCookingTimeCommand command, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PutAsJsonAsync($"/recipes/changeCookingTime", command, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task ChangeRecipeNameAndDescriptionAsync(ChangeRecipeNameAndDescriptionCommand command, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PutAsJsonAsync($"/recipes/changeNameAndDescription", command, cancellationToken);
        response.EnsureSuccessStatusCode();
    }
}