namespace Home.Recipes.WebClient.Services.RecipeHistory.Models;

public sealed record RecipeHistoryListItem(
    Guid Id,
    Guid RecipeId,
    string RecipeName,
    DateTimeOffset CreatedAt);