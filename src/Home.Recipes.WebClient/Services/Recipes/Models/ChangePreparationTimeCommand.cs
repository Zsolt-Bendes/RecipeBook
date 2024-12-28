namespace Home.Recipes.WebClient.Services.Recipes.Models;

public sealed record ChangePreparationTimeCommand(Guid RecipeId, TimeSpan Time)
{
    public sealed class ChangePreparationTimeCommandValidator : AbstractValidator<ChangePreparationTimeCommand>
    {
        public ChangePreparationTimeCommandValidator()
        {
            RuleFor(_ => _.RecipeId).NotEqual(Guid.Empty);
            RuleFor(_ => _.Time).GreaterThan(TimeSpan.Zero);
        }
    }
}

