using Home.Recipes.Domain.Recipes;
using Home.Recipes.Domain.Recipes.Events;
using Wolverine.Http;
using Wolverine.Marten;

namespace Home.Recipes.Api.Features.Recipes;

public sealed record AddRecipeStepCommand(Guid RecipeId, int Index, string Text);

public static class AddRecipeStepEndpoint
{
    internal const string Endpoint = "/recipes/addStep";

    [WolverinePut(Endpoint)]
    [AggregateHandler, EmptyResponse]
    public static StepAdded Put(AddRecipeStepCommand command, Recipe recipe)
    {
        return new StepAdded(command.Text, command.Index);
    }
}
