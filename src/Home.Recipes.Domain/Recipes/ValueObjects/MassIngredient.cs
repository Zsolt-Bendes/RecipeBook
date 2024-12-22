using UnitsNet.Units;

namespace Home.Recipes.Domain.Recipes.ValueObjects;

public sealed class MassIngredient : IngredientBase
{
    public MassIngredient(string ingredientName, MassUnit unit, double wightInGramm)
        : base(ingredientName)
    {
        Unit = unit;
        WightInGramm = wightInGramm;
    }

    public MassUnit Unit { get; }

    public double WightInGramm { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return IngredientName;
        yield return Unit;
        yield return WightInGramm;
    }
}
