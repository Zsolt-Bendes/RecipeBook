namespace Home.Recipes.Domain.RecipeHistory.Events;

public sealed record RecipeCooked(
    Guid RecipeHistoryId,
    Guid RecipeId,
    string RecipeName,
    DateTimeOffset CookedAt);
