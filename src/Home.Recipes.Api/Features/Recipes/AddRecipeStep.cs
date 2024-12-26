namespace Home.Recipes.Api.Features.Recipes;

public sealed record AddRecipeStepCommand(Guid RecipeId, string Text)
{
    public sealed class AddRecipeStepCommandValidator : AbstractValidator<AddRecipeStepCommand>
    {
        public AddRecipeStepCommandValidator()
        {
            RuleFor(_ => _.RecipeId).NotEqual(Guid.Empty);
            RuleFor(_ => _.Text).NotEmpty().MaximumLength(RecipeConstants.MaxStepLength);
        }
    }
}

public static class AddRecipeStepEndpoint
{
    internal const string Endpoint = "/recipes/addStep";

    public static async Task<ProblemDetails> LoadAsync(
        AddRecipeStepCommand command,
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
    public static StepAdded Put(AddRecipeStepCommand command, Recipe recipe)
    {
        return new StepAdded(command.Text);
    }
}
