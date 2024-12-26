namespace Home.Recipes.Api.Features.Recipes.Commands;

public sealed record ChangePreparationTimeCommand(Guid RecipeId, TimeSpan Time)
{
    public sealed class ChangePreparationTimeCommandValidator : AbstractValidator<ChangePreparationTimeCommand>
    {
        public ChangePreparationTimeCommandValidator()
        {
            RuleFor(_ => _.RecipeId).NotEqual(Guid.Empty);
            RuleFor(_ => _.Time).GreaterThan(TimeSpan.Zero);
        }
    }
}

public static class ChangePreparationTimeEndpoint
{
    internal const string Endpoint = "/recipes/changePreparationTime";

    public static async Task<ProblemDetails> LoadAsync(
        ChangePreparationTimeCommand command,
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
    public static PreparationTimeAdjusted Put(ChangePreparationTimeCommand command, Recipe recipe)
    {
        return new PreparationTimeAdjusted(command.Time);
    }
}
