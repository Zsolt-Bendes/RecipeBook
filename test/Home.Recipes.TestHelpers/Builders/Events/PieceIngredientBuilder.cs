using Home.Recipes.Domain.Recipes.ValueObjects;

namespace Home.Recipes.TestHelpers.Builders.Events;

public sealed class PieceIngredientBuilder
{
    private string _ingredientName = new("Tomato");
    private double _value = 1;

    public PieceIngredient Build() => new PieceIngredient(_ingredientName, _value);

    public PieceIngredientBuilder WithIngredientName(string ingredientName)
    {
        _ingredientName = ingredientName;
        return this;
    }

    public PieceIngredientBuilder WithValue(double value)
    {
        _value = value;
        return this;
    }
}
