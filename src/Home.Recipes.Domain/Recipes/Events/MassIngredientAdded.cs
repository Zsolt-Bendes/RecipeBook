using UnitsNet.Units;

namespace Home.Recipes.Domain.Recipes.Events;

public sealed record MassIngredientAdded(string IngredientName, MassUnit Unit, double Value);
