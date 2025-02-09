using Home.Recipes.WebClient.Services.RecipeHistory.Models;
using System.Text.Json;

namespace Home.Recipes.WebClient.Services.RecipeHistory;

public sealed class RecipeHistoryService
{
    private readonly static JsonSerializerOptions _jsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };
    private readonly HttpClient _httpClient;

    public RecipeHistoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<RecipeHistoryQueryResponse?> GetRecipeHistoriesAsync(
        string? searchText,
        CancellationToken cancellationToken = default)
    {
        var endpoint = "/recipes/history";
        if (!string.IsNullOrEmpty(searchText))
        {
            endpoint += $"?searchText={searchText}";
        }

        var response = await _httpClient.GetAsync(endpoint, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await JsonSerializer.DeserializeAsync<RecipeHistoryQueryResponse>(
            await response.Content.ReadAsStreamAsync(cancellationToken),
            _jsonSerializerOptions,
            cancellationToken);
    }

    public async Task RecipeCookedAsync(Guid recipeId, CancellationToken cancellationToken = default)
    {
        var endpoint = $"/recipes/{recipeId}/cooked";
        var response = await _httpClient.PostAsync(endpoint, null, cancellationToken);
        response.EnsureSuccessStatusCode();
    }
}
