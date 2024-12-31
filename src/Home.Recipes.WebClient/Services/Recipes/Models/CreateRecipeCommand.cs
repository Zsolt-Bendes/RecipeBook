using System.Text.Json.Serialization;

namespace Home.Recipes.WebClient.Services.Recipes.Models;

public sealed class CreateRecipeCommand
{
    public CreateRecipeCommand(
        string name,
        string description)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; set; }

    public string Description { get; set; }

    public TimeSpan CookingTime { get => TimeSpan.FromMinutes(CookingTimeInMinutes); }

    public TimeSpan PreparationTime { get => TimeSpan.FromMinutes(PreparationTimeInMinutes); }

    [JsonIgnore]
    public int PreparationTimeInMinutes { get; set; }

    [JsonIgnore]
    public int CookingTimeInMinutes { get; set; }

    public sealed class CreateRecipeCommandValidator : AbstractValidator<CreateRecipeCommand>
    {
        public CreateRecipeCommandValidator()
        {
            RuleFor(_ => _.Name).NotEmpty().MaximumLength(RecipeConstants.MaxNameLength);
            RuleFor(_ => _.Description).NotEmpty().MaximumLength(RecipeConstants.MaxDescriptionLength);
            RuleFor(_ => _.CookingTimeInMinutes).GreaterThanOrEqualTo(0);
            RuleFor(_ => _.PreparationTimeInMinutes).GreaterThanOrEqualTo(0);
        }
    }
}
