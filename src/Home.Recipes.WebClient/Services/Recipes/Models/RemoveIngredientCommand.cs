namespace Home.Recipes.WebClient.Services.Recipes.Models;

public sealed record RemoveIngredientCommand(Guid RecipeId, int Index)
{
    public sealed class RemoveIngredientCommandValidator : AbstractValidator<RemoveIngredientCommand>
    {
        public RemoveIngredientCommandValidator()
        {
            RuleFor(_ => _.RecipeId).NotEqual(Guid.Empty);
            RuleFor(_ => _.Index).GreaterThanOrEqualTo(0);
        }
    }
}

