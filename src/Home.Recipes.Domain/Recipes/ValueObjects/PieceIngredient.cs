namespace Home.Recipes.Domain.Recipes.ValueObjects;

public sealed class PieceIngredient : IngredientBase
{
    public PieceIngredient(string ingredientName, double pieces)
        : base(ingredientName)
    {
        Pieces = pieces.Throw().IfLessThanOrEqualTo(0);
    }

    public double Pieces { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return IngredientName;
        yield return Pieces;
    }
}
