using Home.Recipes.Domain.Recipes;
using Home.Recipes.Domain.Recipes.Events;
using Wolverine.Http;
using Wolverine.Marten;

namespace Home.Recipes.Api.Features.Recipes;

public sealed record RemoveRecipeStepCommand(Guid RecipeId, int Index);

public static class RemoveRecipeStepEndpoint
{
    internal const string Endpoint = "/recipes/removeStep";

    [WolverinePut(Endpoint)]
    [AggregateHandler, EmptyResponse]
    public static StepRemoved Put(AddRecipeStepCommand command, Recipe recipe)
    {
        return new StepRemoved(command.Index);
    }
}
