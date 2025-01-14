namespace Home.Recipes.Api.Features.Recipes.Commands;

public sealed record ChangeRecipeImageCommand(Guid RecipeId, IFormFile Image)
{

}

public sealed class ChangeRecipeImageEndpoint
{
    internal const string Endpoint = "/recipes/changeImage";

    public static async Task<ProblemDetails> LoadAsync(
        ChangeRecipeImageCommand command,
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

    [WolverinePost(Endpoint)]
    [AggregateHandler, EmptyResponse]
    public static RecipeImageAdded Post(ChangeRecipeImageCommand command, Recipe recipe)
    {
        return new RecipeImageAdded(string.Empty);
    }
}
