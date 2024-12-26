using Wolverine.Http.Marten;

namespace Home.Recipes.Api.Features.Recipes;

public static class DeleteRecipeEndpoint
{
    internal const string Endpoint = "/recipes/{recipeId}";

    [WolverineDelete(Endpoint)]
    public static (IResult, RecipeDeleted) Delete([Aggregate] Recipe recipe)
    {
        return (Results.NoContent(), new RecipeDeleted());
    }
}
