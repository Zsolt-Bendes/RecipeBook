using Home.Recipes.Domain.Recipes.Events;
using Home.Recipes.Domain.Recipes.ValueObjects;
using Marten.Schema.Identity;

namespace Home.Recipes.TestHelpers.Builders.Events;

public sealed class RecipeCreatedBuilder
{
    private static FluidIngredientBaseBuilder _fluidIngredientBaseBuilder = new();
    private static MassIngredientBuilder _massIngredientBuilder = new();
    private static PieceIngredientBuilder _pieceIngredientBuilder = new();

    private Guid _recipeId = CombGuidIdGeneration.NewGuid();
    private string _name = "Spaghetti";
    private string _description = "Classic spaghetti how my family likes it!";
    private List<IngredientBase> _ingredients = [];
    private List<RecipeStep> _steps = [];
    private TimeSpan _preparationTime = TimeSpan.FromMinutes(15);
    private TimeSpan _cookingTime = TimeSpan.FromMinutes(30);
    private DateTimeOffset _createdAt = DateTimeOffset.UtcNow;

    public RecipeCreated Build() => new RecipeCreated(
        _recipeId,
        _name,
        _description,
        _ingredients,
        _steps,
        _preparationTime,
        _cookingTime,
        _createdAt);

    public RecipeCreatedBuilder WithRecipeId(Guid recipeId)
    {
        _recipeId = recipeId;
        return this;
    }

    public RecipeCreatedBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public RecipeCreatedBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    //public RecipeCreatedBuilder WithIngredients(List<IngredientBase> ingredients)
    //{
    //    _ingredients = ingredients;
    //    return this;
    //}

    public RecipeCreatedBuilder WithSteps(List<RecipeStep> steps)
    {
        _steps = steps;
        return this;
    }

    public RecipeCreatedBuilder WithPreparationTime(TimeSpan preparationTime)
    {
        _preparationTime = preparationTime;
        return this;
    }

    public RecipeCreatedBuilder WithCookingTime(TimeSpan cookingTime)
    {
        _cookingTime = cookingTime;
        return this;
    }

    public RecipeCreatedBuilder WithCreatedAt(DateTimeOffset createdAt)
    {
        _createdAt = createdAt;
        return this;
    }

    public RecipeCreatedBuilder WithFluidIngredients(params Action<FluidIngredientBaseBuilder>[] actions)
    {
        foreach (var action in actions)
        {
            action.Invoke(_fluidIngredientBaseBuilder);
            _ingredients.Add(_fluidIngredientBaseBuilder.Build());
        }

        return this;
    }

    public RecipeCreatedBuilder WithMassIngredients(params Action<MassIngredientBuilder>[] actions)
    {
        foreach (var action in actions)
        {
            action.Invoke(_massIngredientBuilder);
            _ingredients.Add(_massIngredientBuilder.Build());
        }

        return this;
    }

    public RecipeCreatedBuilder WithPieceIngredients(params Action<PieceIngredientBuilder>[] actions)
    {
        _ingredients = [];
        foreach (var action in actions)
        {
            action.Invoke(_pieceIngredientBuilder);
            _ingredients.Add(_pieceIngredientBuilder.Build());
        }

        return this;
    }
}
