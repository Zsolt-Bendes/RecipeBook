namespace Home.Recipes.WebClient.Services.Recipes.Models;

public sealed class ChangeRecipeStepCommand
{
    public ChangeRecipeStepCommand(Guid recipeId, int index, string text)
    {
        RecipeId = recipeId;
        Index = index;
        Text = text;
    }

    public Guid RecipeId { get; set; }

    public int Index { get; set; }

    public string Text { get; set; }

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

