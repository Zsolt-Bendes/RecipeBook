using Home.Recipes.Domain.Recipes;
using Home.Recipes.Domain.Recipes.Events;
using Marten;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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

    public static async Task<ProblemDetails> LoadAsync(
        AddIngredientCommand command,
        IDocumentSession session,
        CancellationToken cancellationToken)
    {
        var recipe = await session.LoadAsync<Recipe>(command.RecipeId, cancellationToken);
        if (recipe is null)
        {
            return
                new ProblemDetails
                {
                    Title = "Recipe not found",
                    Status = (int)HttpStatusCode.NotFound,
                    Detail = $"Recipe with id {command.RecipeId} not found",
                };
        }

        return WolverineContinue.NoProblems;
    }

    [WolverinePut(Endpoint)]
    [AggregateHandler, EmptyResponse]
    public static object Put(AddIngredientCommand command, Recipe recipe)
    {
        return command.IngredientType switch
        {
            IngredientType.Piece => new PieceIngredientAdded(command.Name, command.ValueBase),
            IngredientType.Mass => new MassIngredientAdded(command.Name, MassUnit.Gram, command.ValueBase),
            IngredientType.Fluid => new FluidIngredientAdded(command.Name, VolumeUnit.Milliliter, command.ValueBase),
            _ => throw new NotImplementedException()
        };
    }
}
