namespace Home.Recipes.WebClient.Services.Recipes.Models;

public sealed record AddRecipeStepCommand(Guid RecipeId, string Text)
{
    public sealed class AddRecipeStepCommandValidator : AbstractValidator<AddRecipeStepCommand>
    {
        public AddRecipeStepCommandValidator()
        {
            RuleFor(_ => _.RecipeId).NotEqual(Guid.Empty);
            RuleFor(_ => _.Text).NotEmpty().MaximumLength(RecipeConstants.MaxStepLength);
        }
    }
}

