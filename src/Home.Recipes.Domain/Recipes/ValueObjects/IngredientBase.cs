namespace Home.Recipes.Domain.Recipes.ValueObjects;

public abstract class IngredientBase : ValueObject
{
    protected IngredientBase(string ingredientName)
    {
        IngredientName = ingredientName;
    }

    public string IngredientName { get; }
}
