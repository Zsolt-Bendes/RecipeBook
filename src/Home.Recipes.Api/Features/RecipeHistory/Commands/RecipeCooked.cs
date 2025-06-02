using Home.Recipes.Domain.RecipeHistory.Events;
using JasperFx.Core;
using Wolverine.Attributes;

namespace Home.Recipes.Api.Features.RecipeHistory.Commands;

public sealed record RecipeCookedCommand(Guid RecipeId);

public static class RecipeCookedEndpoint
{
    internal const string Endpoint = "/recipes/cooked";

    public static async Task<(ProblemDetails, Recipe?)> LoadAsync(
        RecipeCookedCommand command,
        IDocumentSession session,
        CancellationToken cancellationToken)
    {
        var recipe = await session.LoadAsync<Recipe>(command.RecipeId, cancellationToken);

        if (recipe is null)
        {
            return (new ProblemDetails
            {
                Title = "Recipe not found",
                Detail = $"Recipe with id {command.RecipeId} not found",
                Status = StatusCodes.Status404NotFound,
            }, null);
        }

        return (WolverineContinue.NoProblems, recipe);
    }

    [WolverinePost(Endpoint)]
    [Transactional]
    public static IResult Post(
        RecipeCookedCommand command,
        Recipe recipe,
        IDocumentSession session)
    {
        var id = CombGuidIdGeneration.NewGuid();
        var recipeCooked = new RecipeCooked(
            id,
            recipe.Id,
            recipe.Name.Name,
            DateTimeOffset.UtcNow);

        session.Events.StartStream(id, recipeCooked);

        return Results.NoContent();
    }
}
