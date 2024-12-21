using Home.Recipes.Domain.Recipes;
using Home.Recipes.Domain.Recipes.Events;
using Wolverine.Http;
using Wolverine.Marten;

namespace Home.Recipes.Api.Features.Recipes;

public sealed record UpdateRecipeNameAndDescriptionCommand(
    Guid RecipeId,
    string? Name,
    string? Description);

public static class UpdateRecipeNameAndDescriptionEndpoint
{
    internal const string Endpoint = "/recipes/updateNameAndDescription";

    [WolverinePut(Endpoint)]
    [AggregateHandler, EmptyResponse]
    public static Events Put(UpdateRecipeNameAndDescriptionCommand command, Recipe recipe)
    {
        var evts = new Events();
        if (!string.IsNullOrEmpty(command.Name))
        {
            evts.Add(new RecipeNameChanged(command.Name));
        }
        if (!string.IsNullOrEmpty(command.Description))
        {
            evts.Add(new RecipeDescriptionChanged(command.Description));
        }

        return evts;
    }
}
