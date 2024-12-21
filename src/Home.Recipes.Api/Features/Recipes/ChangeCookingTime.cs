using Home.Recipes.Domain.Recipes;
using Home.Recipes.Domain.Recipes.Events;
using Wolverine.Http;
using Wolverine.Marten;

namespace Home.Recipes.Api.Features.Recipes;

public sealed record ChangeCookingTimeCommand(Guid RecipeId, TimeSpan Time);

public static class ChangeCookingTimeEndpoint
{
    internal const string Endpoint = "/recipes/changeCookingTime";

    [WolverinePut(Endpoint)]
    [AggregateHandler, EmptyResponse]
    public static CookingTimeAdjusted Put(ChangeCookingTimeCommand command, Recipe recipe)
    {
        return new CookingTimeAdjusted(command.Time);
    }
}
