using JasperFx.Core;
using Wolverine.Attributes;

namespace Home.Recipes.Api.Features.Recipes.Commands;

public sealed record CreateRecipeCommand(
    string Name,
    string Description,
    TimeSpan CookingTime,
    TimeSpan PreparationTime)
{
    public sealed class CreateRecipeCommandValidator : AbstractValidator<CreateRecipeCommand>
    {
        public CreateRecipeCommandValidator()
        {
            RuleFor(_ => _.Name).NotEmpty().MaximumLength(RecipeConstants.MaxNameLength);
            RuleFor(_ => _.Description).NotEmpty().MaximumLength(RecipeConstants.MaxDescriptionLength);
            RuleFor(_ => _.CookingTime).GreaterThan(TimeSpan.Zero);
            RuleFor(_ => _.PreparationTime).GreaterThan(TimeSpan.Zero);
        }
    }
}

public sealed record CreateRecipeResponse(Guid RecipeId);

public static class CreateRecipeEndpoint
{
    internal const string Endpoint = "/recipes/create";

    public async static Task<ProblemDetails> LoadAsync(
        CreateRecipeCommand command,
        IDocumentSession session,
        CancellationToken cancellationToken)
    {
        var recipeWithSameNameExists = await session
            .Query<Recipe>()
            .AnyAsync(_ => _.Name.Name == command.Name, cancellationToken);

        if (recipeWithSameNameExists)
        {
            return new ProblemDetails
            {
                Detail = "Recipe with the same name already exists!",
                Status = 400,
            };
        }

        return WolverineContinue.NoProblems;
    }

    [WolverinePost(Endpoint)]
    [Transactional]
    public static CreateRecipeResponse Post(
        CreateRecipeCommand command,
        IDocumentSession session,
        TimeProvider timeProvider)
    {
        var id = CombGuidIdGeneration.NewGuid();
        var evt = new RecipeCreated(
            id,
            command.Name,
            command.Description,
            [],
            [],
            command.PreparationTime,
            command.CookingTime,
            timeProvider.GetUtcNow());

        session.Events.StartStream(id, evt);

        return new CreateRecipeResponse(id);
    }
}
