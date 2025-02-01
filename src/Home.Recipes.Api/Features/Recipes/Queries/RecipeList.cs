using Marten.Linq;

namespace Home.Recipes.Api.Features.Recipes.Queries;

public sealed record RecipeListQueryResponse(IReadOnlyList<RecipeListItem> Recipes);

public sealed record RecipeListItem(
    Guid RecipeId,
    string Name,
    string Description,
    string ImagePath,
    TimeSpan PreparationTime,
    TimeSpan CookingTime);

public static class RecipeListEndpoint
{
    internal const string Endpoint = "/recipes";

    [WolverineGet(Endpoint)]
    public static async Task<RecipeListQueryResponse> Get(
        string? searchText,
        IQuerySession session,
        CancellationToken cancellationToken)
    {
        var query = session.Query<Recipe>();
        if (!string.IsNullOrEmpty(searchText))
        {
            query = query.Where(_ => _.Name.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase)) as IMartenQueryable<Recipe>;
        }

        var result = await query!
            .Select(_ => new RecipeListItem(
                _.Id,
                _.Name.Name,
                _.Description.Description,
                _.ThumbnailPath,
                _.PreparationTime.Time,
                _.CookingTime.Time))
            .ToListAsync(cancellationToken);

        return new RecipeListQueryResponse(result);
    }
}
