namespace Home.Recipes.WebClient.Services.Recipes.Models;

public sealed class ChangeRecipeIngredientCommand
{
    public ChangeRecipeIngredientCommand(
        Guid recipeId,
        int index,
        string name,
        double value)
    {
        RecipeId = recipeId;
        Index = index;
        Name = name;
        Value = value;
    }

    public Guid RecipeId { get; set; }

    public int Index { get; set; }

    public string Name { get; set; }

    public double Value { get; set; }

    public sealed class ChangeRecipeIngredientCommandValidator : AbstractValidator<ChangeRecipeIngredientCommand>
    {
        public ChangeRecipeIngredientCommandValidator()
        {
            RuleFor(_ => _.RecipeId).NotEqual(Guid.Empty);
            RuleFor(_ => _.Index).GreaterThanOrEqualTo(0);
            RuleFor(_ => _.Name).NotEmpty().MaximumLength(RecipeConstants.MaxIngredientNameLength);
            RuleFor(_ => _.Value).GreaterThan(0);
        }
    }
}
