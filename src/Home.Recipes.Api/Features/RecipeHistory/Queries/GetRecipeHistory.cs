namespace Home.Recipes.Api.Features.RecipeHistory.Queries;

public sealed record RecipeHistoryQueryResponse(IReadOnlyList<RecipeHistoryListItem> History);

public sealed record RecipeHistoryListItem(
    Guid Id,
    Guid RecipeId,
    string RecipeName,
    DateTimeOffset CreatedAt);

public static class GetRecipeHistory
{
    internal const string Endpoint = "/recipes/{recipeId}/history";

    [WolverineGet(Endpoint)]
    public static async Task<IResult> Get(
        Guid recipeId,
        IQuerySession session,
        CancellationToken cancellationToken)
    {
        var result = await session.Query<Domain.RecipeHistory.RecipeHistory>()
            .Where(_ => _.RecipeId == recipeId)
            .OrderByDescending(_ => _.CreatedAt)
            .Select(_ => new RecipeHistoryListItem(
                _.Id,
                _.RecipeId,
                _.RecipeName,
                _.CreatedAt))
            .ToListAsync(cancellationToken);

        return Results.Ok(new RecipeHistoryQueryResponse(result));
    }
}
