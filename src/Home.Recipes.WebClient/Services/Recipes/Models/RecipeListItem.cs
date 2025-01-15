namespace Home.Recipes.WebClient.Services.Recipes.Models;

public sealed record RecipeListItem(
    Guid RecipeId,
    string Name,
    string Description,
    string ImagePath,
    TimeSpan PreparationTime,
    TimeSpan CookingTime);
