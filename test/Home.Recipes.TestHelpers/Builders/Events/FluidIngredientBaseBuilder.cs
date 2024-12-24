using Home.Recipes.Domain.Recipes.ValueObjects;
using UnitsNet.Units;

namespace Home.Recipes.TestHelpers.Builders.Events;

public sealed class FluidIngredientBaseBuilder
{
    private string _ingredientName = new("Tomato");
    private MassUnit _massUnit = MassUnit.Gram;
    private double _value = 100;
    private VolumeUnit _fluidUnit = VolumeUnit.Milliliter;

    public IngredientBase Build() => new FluidIngredient(_ingredientName, _fluidUnit, _value);

    public FluidIngredientBaseBuilder WithIngredientName(string ingredientName)
    {
        _ingredientName = ingredientName;
        return this;
    }

    public FluidIngredientBaseBuilder WithValue(double value)
    {
        _value = value;
        return this;
    }

    public FluidIngredientBaseBuilder WithFluidUnit(VolumeUnit unit)
    {
        _fluidUnit = unit;
        return this;
    }
}