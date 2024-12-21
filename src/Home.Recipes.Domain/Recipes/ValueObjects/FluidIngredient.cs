using UnitsNet.Units;

namespace Home.Recipes.Domain.Recipes.ValueObjects;

public sealed class FluidIngredient : IngredientBase
{
    public FluidIngredient(string ingredientName, VolumeUnit unit, double amount)
        : base(ingredientName)
    {
        Unit = unit;
        VolumeInMilliliter = amount;
    }

    public VolumeUnit Unit { get; }
    public double VolumeInMilliliter { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return IngredientName;
        yield return Unit;
        yield return VolumeInMilliliter;
    }
}
