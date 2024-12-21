namespace Home.Recipes.Domain.Recipes.ValueObjects;

public sealed class RecipeName : ValueObject
{
    public RecipeName(string name)
    {
        Name = name
            .Throw()
            .IfNullOrEmpty(_ => _)
            .Throw()
            .IfLongerThan(RecipeConstants.MaxDescriptionLength);
    }

    public string Name { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }
}
