namespace Home.Recipes.WebClient.Services.Recipes.Models;

public sealed record AddIngredientCommand(
    Guid RecipeId,
    IngredientType IngredientType,
    string Name,
    double ValueBase)
{
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
};