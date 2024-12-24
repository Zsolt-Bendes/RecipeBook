using Home.Recipes.Domain.Recipes.ValueObjects;
using UnitsNet.Units;

namespace Home.Recipes.TestHelpers.Builders.Events;

public sealed class MassIngredientBuilder
{
    private string _ingredientName = new("Tomato");
    private MassUnit _massUnit = MassUnit.Gram;
    private double _value = 100;

    public IngredientBase Build() => new MassIngredient(_ingredientName, _massUnit, _value);

    public MassIngredientBuilder WithIngredientName(string ingredientName)
    {
        _ingredientName = ingredientName;
        return this;
    }

    public MassIngredientBuilder WithMassUnit(MassUnit unit)
    {
        _massUnit = unit;
        return this;
    }

    public MassIngredientBuilder WithValue(double value)
    {
        _value = value;
        return this;
    }
}
