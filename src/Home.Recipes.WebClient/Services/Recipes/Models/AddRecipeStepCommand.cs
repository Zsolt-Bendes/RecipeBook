namespace Home.Recipes.WebClient.Services.Recipes.Models;

public sealed class AddRecipeStepCommand
{
    public AddRecipeStepCommand(Guid recipeId)
    {
        RecipeId = recipeId;
        Text = string.Empty;
    }

    public Guid RecipeId { get; }

    public string Text { get; set; }

    public sealed class AddRecipeStepCommandValidator : AbstractValidator<AddRecipeStepCommand>
    {
        public AddRecipeStepCommandValidator()
        {
            RuleFor(_ => _.RecipeId).NotEqual(Guid.Empty);
            RuleFor(_ => _.Text).NotEmpty().MaximumLength(RecipeConstants.MaxStepLength);
        }
    }
}

