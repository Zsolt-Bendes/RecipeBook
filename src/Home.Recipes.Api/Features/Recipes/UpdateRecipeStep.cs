using Home.Recipes.Domain.Recipes;
using Home.Recipes.Domain.Recipes.Events;
using Marten;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Wolverine.Http;
using Wolverine.Marten;

namespace Home.Recipes.Api.Features.Recipes;

public sealed record UpdateRecipeStepCommand(Guid RecipeId, int Index, string Text);

public static class UpdateRecipeStepEndpoint
{
    internal const string Endpoint = "/recipes/updateStep";

    public static async Task<ProblemDetails> LoadAsync(
        UpdateRecipeStepCommand command,
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
    public static StepUpdated Put(UpdateRecipeStepCommand command, Recipe recipe)
    {
        return new StepUpdated(command.Text, command.Index);
    }
}
