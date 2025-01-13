namespace Home.Recipes.WebClient.Services.Recipes.Models;

public sealed class Ingredient
{
    public Ingredient(string name, IngredientType type, double value)
    {
        Name = name;
        Type = type;
        Value = value;
    }

    public string Name { get; set; }

    public IngredientType Type { get; set; }

    public double Value { get; set; }
}
