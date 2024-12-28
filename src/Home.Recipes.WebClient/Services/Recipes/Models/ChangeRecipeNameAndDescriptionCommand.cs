namespace Home.Recipes.WebClient.Services.Recipes.Models;

public sealed record ChangeRecipeNameAndDescriptionCommand(
    Guid RecipeId,
    string? Name,
    string? Description)
{
    public sealed class ChangeRecipeNameAndDescriptionCommandValidator
        : AbstractValidator<ChangeRecipeNameAndDescriptionCommand>
    {
        public ChangeRecipeNameAndDescriptionCommandValidator()
        {
            RuleFor(_ => _.RecipeId).NotEqual(Guid.Empty);
            RuleFor(_ => _.Name)
                .NotEmpty()
                .When(_ => string.IsNullOrEmpty(_.Description))
                .MaximumLength(RecipeConstants.MaxNameLength);
            RuleFor(_ => _.Description)
                .NotEmpty()
                .When(_ => string.IsNullOrEmpty(_.Name))
                .MaximumLength(RecipeConstants.MaxDescriptionLength);
        }
    }
}

