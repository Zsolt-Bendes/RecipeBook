namespace Home.Recipes.Api.Features.Recipes;

public sealed record RemoveRecipeStepCommand(Guid RecipeId, int Index)
{
    public sealed class RemoveRecipeStepCommandValidator : AbstractValidator<RemoveRecipeStepCommand>
    {
        public RemoveRecipeStepCommandValidator()
        {
            RuleFor(_ => _.RecipeId).NotEqual(Guid.Empty);
            RuleFor(_ => _.Index).GreaterThanOrEqualTo(0);
        }
    }
}

public static class RemoveRecipeStepEndpoint
{
    internal const string Endpoint = "/recipes/removeStep";

    public static async Task<ProblemDetails> LoadAsync(
        RemoveRecipeStepCommand command,
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
    public static StepRemoved Put(RemoveRecipeStepCommand command, Recipe recipe)
    {
        return new StepRemoved(command.Index);
    }
}
