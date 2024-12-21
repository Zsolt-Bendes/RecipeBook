using Home.Recipes.Domain.Recipes;
using Home.Recipes.Domain.Recipes.Events;
using Wolverine.Http;
using Wolverine.Marten;

namespace Home.Recipes.Api.Features.Recipes;

public sealed record UpdateRecipeStepCommand(Guid RecipeId, int Index, string Text);

public static class UpdateRecipeStepEndpoint
{
    internal const string Endpoint = "/recipes/updateStep";

    [WolverinePut(Endpoint)]
    [AggregateHandler, EmptyResponse]
    public static StepUpdated Put(UpdateRecipeStepCommand command, Recipe recipe)
    {
        return new StepUpdated(command.Text, command.Index);
    }
}
