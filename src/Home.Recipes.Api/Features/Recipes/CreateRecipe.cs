using Home.Recipes.Domain.Recipes;
using Home.Recipes.Domain.Recipes.Events;
using Marten;
using Marten.Schema.Identity;
using Microsoft.AspNetCore.Mvc;
using Wolverine.Attributes;
using Wolverine.Http;

namespace Home.Recipes.Api.Features.Recipes;

public sealed record CreateRecipeCommand(
    string Name,
    string Description,
    TimeSpan CookingTime,
    TimeSpan PreparationTime);

public sealed record CreateRecipeResponse(Guid RecipeId);

public static class CreateRecipeEndpoint
{
    internal const string Endpoint = "/recipes/create";

    public async static Task<ProblemDetails> LoadAsync(
        CreateRecipeCommand command,
        IDocumentSession session,
        CancellationToken cancellationToken)
    {
        var recipeWithSameNameExists = await session
            .Query<Recipe>()
            .AnyAsync(_ => _.Name.Name == command.Name, cancellationToken);

        if (recipeWithSameNameExists)
        {
            return new ProblemDetails
            {
                Detail = "Recipe with the same name already exists!",
                Status = 400,
            };
        }

        return WolverineContinue.NoProblems;
    }

    [WolverinePost(Endpoint)]
    [Transactional]
    public static CreateRecipeResponse Post(
        CreateRecipeCommand command,
        IDocumentSession session,
        TimeProvider timeProvider)
    {
        var id = CombGuidIdGeneration.NewGuid();
        var evt = new RecipeCreated(
            id,
            command.Name,
            command.Description,
            [],
            [],
            command.PreparationTime,
            command.CookingTime,
            timeProvider.GetUtcNow());

        session.Events.StartStream(id, evt);

        return new CreateRecipeResponse(id);
    }
}
