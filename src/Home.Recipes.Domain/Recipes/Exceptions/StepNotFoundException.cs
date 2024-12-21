namespace Home.Recipes.Domain.Recipes.Exceptions;

[Serializable]
public sealed class StepNotFoundException : Exception
{
    public StepNotFoundException()
    {
    }

    public StepNotFoundException(Guid recipeId, int index)
        : base($"Recipe [{recipeId}] has no step at index [{index}]!")
    { }

    public StepNotFoundException(string message)
        : base(message)
    { }

    public StepNotFoundException(string message, Exception inner)
        : base(message, inner)
    { }
}
