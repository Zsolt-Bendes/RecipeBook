namespace Home.Recipes.Domain.Recipes.ValueObjects;

public sealed class IngredientName : ValueObject
{
    public IngredientName(string name)
    {
        Name = name
            .Throw()
            .IfNullOrEmpty(_ => _)
            .Throw()
            .IfLongerThan(RecipeConstants.MaxIngredientNameLength);
    }

    public string Name { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }
}
