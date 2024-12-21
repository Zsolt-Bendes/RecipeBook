using Home.Recipes.Domain.Recipes;
using Home.Recipes.Domain.Recipes.ValueObjects;
using UnitsNet.Units;
using Wolverine.Http;
using Wolverine.Marten;

namespace Home.Recipes.Api.Features.Recipes;

public sealed record AddIngredientCommand(
    Guid RecipeId,
    IngredientType IngredientType,
    string Name,
    double ValueBase);

public enum IngredientType
{
    Piece,
    Fluid,
    Mass,
}

public static class AddIngredientEndpoint
{
    internal const string Endpoint = "/recipes/addIngredient";

    [WolverinePut(Endpoint)]
    [AggregateHandler, EmptyResponse]
    public static IngredientBase Post(AddIngredientCommand command, Recipe recipe)
    {
        return command.IngredientType switch
        {
            IngredientType.Piece => new PieceIngredient(command.Name, command.ValueBase),
            IngredientType.Mass => new MassIngredient(command.Name, MassUnit.Gram, command.ValueBase),
            IngredientType.Fluid => new FluidIngredient(command.Name, VolumeUnit.Milliliter, command.ValueBase),
            _ => throw new NotImplementedException()
        };
    }
}
