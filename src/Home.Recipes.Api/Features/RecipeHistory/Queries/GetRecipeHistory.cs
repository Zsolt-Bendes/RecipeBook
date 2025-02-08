namespace Home.Recipes.Api.Features.RecipeHistory.Queries;

public sealed record RecipeHistoryQueryResponse(IReadOnlyList<RecipeHistoryListItem> History);

public sealed record RecipeHistoryListItem(
    Guid Id,
    Guid RecipeId,
    string RecipeName,
    DateTimeOffset CreatedAt);

public static class GetRecipeHistory
{
    internal const string Endpoint = "/recipes/history";

    [WolverineGet(Endpoint)]
    public static async Task<IResult> Get(
        string? searchText,
        IQuerySession session,
        CancellationToken cancellationToken)
    {
        IReadOnlyList<RecipeHistoryListItem>? result = null;
        if (string.IsNullOrWhiteSpace(searchText))
        {
            result = await session.Query<Domain.RecipeHistory.RecipeHistory>()
                .OrderByDescending(_ => _.CreatedAt)
                .Select(_ => new RecipeHistoryListItem(
                    _.Id,
                    _.RecipeId,
                    _.RecipeName,
                    _.CreatedAt))
                .ToListAsync(cancellationToken);
        }
        else
        {
            result = await session.Query<Domain.RecipeHistory.RecipeHistory>()
                .Where(_ => _.RecipeName.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(_ => _.CreatedAt)
                .Select(_ => new RecipeHistoryListItem(
                    _.Id,
                    _.RecipeId,
                    _.RecipeName,
                    _.CreatedAt))
                .ToListAsync(cancellationToken);
        }

        return Results.Ok(new RecipeHistoryQueryResponse(result));
    }
}
