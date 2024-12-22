using UnitsNet.Units;

namespace Home.Recipes.Domain.Recipes.ValueObjects;

public sealed class FluidIngredient : IngredientBase
{
    public FluidIngredient(string ingredientName, VolumeUnit unit, double volumeInMilliliter)
        : base(ingredientName)
    {
        Unit = unit;
        VolumeInMilliliter = volumeInMilliliter;
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
