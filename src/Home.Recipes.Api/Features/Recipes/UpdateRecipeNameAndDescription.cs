using Home.Recipes.Domain.Recipes;
using Home.Recipes.Domain.Recipes.Events;
using Marten;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Wolverine.Http;
using Wolverine.Marten;

namespace Home.Recipes.Api.Features.Recipes;

public sealed record UpdateRecipeNameAndDescriptionCommand(
    Guid RecipeId,
    string? Name,
    string? Description);

public static class UpdateRecipeNameAndDescriptionEndpoint
{
    internal const string Endpoint = "/recipes/updateNameAndDescription";

    public static async Task<ProblemDetails> LoadAsync(
        UpdateRecipeNameAndDescriptionCommand command,
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
    public static IEnumerable<object> Put(UpdateRecipeNameAndDescriptionCommand command, Recipe recipe)
    {
        if (!string.IsNullOrEmpty(command.Name))
        {
            yield return new RecipeNameChanged(command.Name);
        }
        if (!string.IsNullOrEmpty(command.Description))
        {
            yield return new RecipeDescriptionChanged(command.Description);
        }
    }
}
