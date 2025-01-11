namespace Home.Recipes.WebClient.Services.Recipes.Models;

public sealed class AddIngredientCommand
{
    public AddIngredientCommand(Guid recipeId)
    {
        RecipeId = recipeId;
    }

    public Guid RecipeId { get; set; }

    public IngredientType IngredientType { get; set; }

    public string Name { get; set; }

    public double ValueBase { get; set; }

    public sealed class AddIngredientCommandValidator : AbstractValidator<AddIngredientCommand>
    {
        public AddIngredientCommandValidator()
        {
            RuleFor(_ => _.RecipeId).NotEqual(Guid.Empty);
            RuleFor(_ => _.Name).NotEmpty().MaximumLength(RecipeConstants.MaxIngredientNameLength);
            RuleFor(_ => _.ValueBase).GreaterThan(0);
            RuleFor(_ => _.IngredientType).IsInEnum();
        }
    }
}
