using System.Text.Json.Serialization;

namespace Home.Recipes.Domain.Recipes.ValueObjects;

[JsonDerivedType(typeof(FluidIngredient), 0)]
[JsonDerivedType(typeof(MassIngredient), 1)]
[JsonDerivedType(typeof(PieceIngredient), 2)]
public abstract class IngredientBase : ValueObject
{
    protected IngredientBase(string ingredientName)
    {
        IngredientName = ingredientName;
    }

    public string IngredientName { get; }
}
