using UnitsNet.Units;

namespace Home.Recipes.Domain.Recipes.Events;

public sealed record FluidIngredientAdded(string IngredientName, VolumeUnit Unit, double Value);
