namespace Home.Recipes.WebClient.Services.Recipes.Models;

public sealed record RemoveRecipeStepCommand(Guid RecipeId, int Index)
{
    public sealed class RemoveRecipeStepCommandValidator : AbstractValidator<RemoveRecipeStepCommand>
    {
        public RemoveRecipeStepCommandValidator()
        {
            RuleFor(_ => _.RecipeId).NotEqual(Guid.Empty);
            RuleFor(_ => _.Index).GreaterThanOrEqualTo(0);
        }
    }
}

