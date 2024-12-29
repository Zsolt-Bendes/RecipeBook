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
        var response = await _httpClient.PostAsJsonAsync($"/recipes", command, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await JsonSerializer.DeserializeAsync<CreateRecipeResponse>(
           await response.Content.ReadAsStreamAsync(cancellationToken),
           _jsonSerializerOptions,
           cancellationToken);
    }
}