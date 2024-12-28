namespace Home.Recipes.WebClient.Services.Recipes.Models;

public sealed record CreateRecipeCommand(
    string Name,
    string Description,
    TimeSpan CookingTime,
    TimeSpan PreparationTime)
{
    public sealed class CreateRecipeCommandValidator : AbstractValidator<CreateRecipeCommand>
    {
        public CreateRecipeCommandValidator()
        {
            RuleFor(_ => _.Name).NotEmpty().MaximumLength(RecipeConstants.MaxNameLength);
            RuleFor(_ => _.Description).NotEmpty().MaximumLength(RecipeConstants.MaxDescriptionLength);
            RuleFor(_ => _.CookingTime).GreaterThan(TimeSpan.Zero);
            RuleFor(_ => _.PreparationTime).GreaterThan(TimeSpan.Zero);
        }
    }
}
