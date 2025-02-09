namespace Home.Recipes.WebClient.Services.RecipeHistory.Models;

public sealed record RecipeHistoryQueryResponse(IReadOnlyList<RecipeHistoryListItem> History);
