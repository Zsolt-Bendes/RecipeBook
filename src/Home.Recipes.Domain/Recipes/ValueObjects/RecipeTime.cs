namespace Home.Recipes.Domain.Recipes.ValueObjects;

public sealed class RecipeTime : ValueObject
{
    public RecipeTime(TimeSpan time)
    {
        Time = time.Throw().IfDefault();
    }

    public TimeSpan Time { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}
