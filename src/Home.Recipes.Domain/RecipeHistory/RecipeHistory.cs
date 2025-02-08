using Home.Recipes.Domain.RecipeHistory.Events;

namespace Home.Recipes.Domain.RecipeHistory;

public sealed record RecipeHistory(
    Guid Id,
    Guid RecipeId,
    string RecipeName,
    DateTimeOffset CreatedAt)
{
    public static RecipeHistory Create(RecipeCooked recipeCooked) => new RecipeHistory(
        recipeCooked.RecipeHistoryId,
        recipeCooked.RecipeId,
        recipeCooked.RecipeName,
        recipeCooked.CookedAt);
}
