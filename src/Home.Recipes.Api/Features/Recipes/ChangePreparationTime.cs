using Home.Recipes.Domain.Recipes;
using Home.Recipes.Domain.Recipes.Events;
using Wolverine.Http;
using Wolverine.Marten;

namespace Home.Recipes.Api.Features.Recipes;

public sealed record ChangePreparationTimeCommand(Guid RecipeId, TimeSpan Time);

public static class ChangePreparationTimeEndpoint
{
    internal const string Endpoint = "/recipes/changePreparationTime";

    [WolverinePut(Endpoint)]
    [AggregateHandler, EmptyResponse]
    public static PreparationTimeAdjusted Put(ChangePreparationTimeCommand command, Recipe recipe)
    {
        return new PreparationTimeAdjusted(command.Time);
    }
}
