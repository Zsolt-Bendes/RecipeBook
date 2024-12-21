namespace Home.Recipes.Domain.Recipes.Exceptions;

[Serializable]
public class IngredientNotFoundException : Exception
{
    public IngredientNotFoundException()
    { }

    public IngredientNotFoundException(Guid recipeId, int index)
         : base($"Recipe [{recipeId}] has no ingredient at index [{index}]!")
    {
    }

    public IngredientNotFoundException(string message)
        : base(message)
    { }

    public IngredientNotFoundException(string message, Exception inner)
        : base(message, inner)
    { }
}