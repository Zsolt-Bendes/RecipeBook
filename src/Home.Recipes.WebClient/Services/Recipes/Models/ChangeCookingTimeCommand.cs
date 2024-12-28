namespace Home.Recipes.WebClient.Services.Recipes.Models;

public sealed record ChangeCookingTimeCommand(Guid RecipeId, TimeSpan Time)
{
    public sealed class ChangeCookingTimeCommandValidator : AbstractValidator<ChangeCookingTimeCommand>
    {
        public ChangeCookingTimeCommandValidator()
        {
            RuleFor(_ => _.RecipeId).NotEqual(Guid.Empty);
            RuleFor(_ => _.Time).GreaterThan(TimeSpan.Zero);
        }
    }
}

