namespace Home.Recipes.Domain.Recipes.ValueObjects;

public sealed class RecipeDescription : ValueObject
{
    public RecipeDescription(string description)
    {
        Description = description
            .Throw()
            .IfNullOrEmpty(_ => _)
            .Throw()
            .IfLongerThan(RecipeConstants.MaxDescriptionLength);
    }

    public string Description { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Description;
    }
}
