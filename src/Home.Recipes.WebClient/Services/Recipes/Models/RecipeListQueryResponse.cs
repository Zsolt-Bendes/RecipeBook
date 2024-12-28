namespace Home.Recipes.WebClient.Services.Recipes.Models;

public sealed record RecipeListQueryResponse(IReadOnlyList<RecipeListItem> Recipes);
