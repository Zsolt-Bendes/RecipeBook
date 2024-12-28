namespace Home.Recipes.WebClient.Services.Recipes.Models;

public sealed record RecipeListItem(
    Guid RecipeId,
    string Name,
    string Description,
    TimeSpan PreparationTime,
    TimeSpan CookingTime);
