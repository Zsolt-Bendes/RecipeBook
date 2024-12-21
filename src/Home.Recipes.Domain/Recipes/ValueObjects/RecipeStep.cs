namespace Home.Recipes.Domain.Recipes.ValueObjects;

public sealed class RecipeStep : ValueObject
{
    public RecipeStep(string text)
    {
        Text = text
            .Throw()
            .IfNullOrEmpty(_ => _)
            .Throw()
            .IfLongerThan(RecipeConstants.MaxStepLength);
    }

    public string Text { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Text;
    }
}
