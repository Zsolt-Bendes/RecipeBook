namespace Home.Recipes.WebClient.Services.Recipes.Models;

public sealed record ChangeRecipeStepCommand(Guid RecipeId, int Index, string Text)
{
    public sealed class ChangeRecipeStepCommandValidator : AbstractValidator<ChangeRecipeStepCommand>
    {
        public ChangeRecipeStepCommandValidator()
        {
            RuleFor(_ => _.RecipeId).NotEqual(Guid.Empty);
            RuleFor(_ => _.Index).GreaterThanOrEqualTo(0);
            RuleFor(_ => _.Text).NotEmpty().MaximumLength(RecipeConstants.MaxStepLength);
        }
    }
}

