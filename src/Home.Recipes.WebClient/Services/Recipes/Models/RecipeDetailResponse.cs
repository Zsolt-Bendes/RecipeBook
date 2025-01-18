namespace Home.Recipes.WebClient.Services.Recipes.Models;

public sealed record RecipeDetailResponse(
    Guid RecipeId,
    string Name,
    string Description,
    string ImagePath,
    TimeSpan PreparationTime,
    TimeSpan CookingTime,
    List<Ingredient> Ingredients,
    List<string> Steps);
