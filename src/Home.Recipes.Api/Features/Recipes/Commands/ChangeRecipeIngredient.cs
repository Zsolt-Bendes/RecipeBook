namespace Home.Recipes.Api.Features.Recipes.Commands;

public sealed record ChangeRecipeIngredientCommand(
    Guid RecipeId,
    int Index,
    string Name,
    double Value)
{
    public sealed class ChangeRecipeIngredientCommandValidator : AbstractValidator<ChangeRecipeIngredientCommand>
    {
        public ChangeRecipeIngredientCommandValidator()
        {
            RuleFor(_ => _.RecipeId).NotEqual(Guid.Empty);
            RuleFor(_ => _.Index).GreaterThanOrEqualTo(0);
            RuleFor(_ => _.Name).NotEmpty().MaximumLength(RecipeConstants.MaxIngredientNameLength);
            RuleFor(_ => _.Value).GreaterThan(0);
        }
    }
}

public sealed class ChangeRecipeIngredientEndpoint
{
    internal const string Endpoint = "/recipes/changeRecipeIngredient";

    public static async Task<ProblemDetails> LoadAsync(
      ChangeRecipeIngredientCommand command,
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

        if(recipe.Ingredients.Count < command.Index + 1)
        {
            return
                new ProblemDetails
                {
                    Title = "Ingredient not found",
                    Status = (int)HttpStatusCode.NotFound,
                    Detail = $"Ingredient with index {command.Index} not found",
                };
        }

        return WolverineContinue.NoProblems;
    }

    [WolverinePut(Endpoint)]
    [AggregateHandler, EmptyResponse]
    public static IngredientChanged Put(ChangeRecipeIngredientCommand command, Recipe recipe)
    {
        return new IngredientChanged(command.Index, command.Name, command.Value);
    }
}
