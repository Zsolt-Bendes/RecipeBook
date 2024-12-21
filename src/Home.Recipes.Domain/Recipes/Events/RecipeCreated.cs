using Home.Recipes.Domain.Recipes.ValueObjects;

namespace Home.Recipes.Domain.Recipes.Events;

public sealed record RecipeCreated(
    Guid RecipeId,
    string Name,
    string Description,
    List<IngredientBase> Ingredients,
    List<RecipeStep> Steps,
    TimeSpan PreparationTime,
    TimeSpan CookingTime,
    DateTimeOffset CreatedAt);
