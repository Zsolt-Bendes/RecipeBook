using Home.Recipes.Api.Features.Recipes.Common;
using Home.Recipes.Domain.Recipes.ValueObjects;
using Wolverine.Http.Marten;

namespace Home.Recipes.Api.Features.Recipes.Queries;

public sealed record RecipeDetailResponse(
    Guid RecipeId,
    string Name,
    string Description,
    string ImagePath,
    TimeSpan PreparationTime,
    TimeSpan CookingTime,
    List<Ingredient> Ingredients,
    List<string> Steps);

public sealed record Ingredient(string Name, IngredientType Type, double Value);

public static class RecipeDetailEndpoint
{
    internal const string Endpoint = "/recipes/{recipeId}";

    [WolverineGet(Endpoint)]
    public static IResult Get([Aggregate] Recipe recipe)
    {
        if (recipe is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(new RecipeDetailResponse(
            recipe.Id,
            recipe.Name.Name,
            recipe.Description.Description,
            recipe.ImagePath,
            recipe.PreparationTime.Time,
            recipe.CookingTime.Time,
            recipe.Ingredients.ConvertAll(ConvertIngredient),
            recipe.Steps.ConvertAll(_ => _.Text)));
    }

    private static Ingredient ConvertIngredient(IngredientBase ingredientBase)
    {
        if (ingredientBase is PieceIngredient pieceIngredient)
        {
            return new Ingredient(
                pieceIngredient.IngredientName,
                IngredientType.Piece,
                pieceIngredient.Pieces);
        }

        if (ingredientBase is FluidIngredient fluidIngredient)
        {
            return new Ingredient(
                fluidIngredient.IngredientName,
                IngredientType.Fluid,
                fluidIngredient.VolumeInMilliliter);
        }

        if (ingredientBase is MassIngredient massIngredient)
        {
            return new Ingredient(
                massIngredient.IngredientName,
                IngredientType.Mass,
                massIngredient.WightInGramm);
        }

        throw new InvalidOperationException("Unknown ingredient type");
    }
}
