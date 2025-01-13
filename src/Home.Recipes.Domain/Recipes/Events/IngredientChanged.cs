using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home.Recipes.Domain.Recipes.Events;

public sealed record IngredientChanged(int Index, string Name, double Value);

public sealed record MassIngredientChanged(int Index, string Name, double Value);

public sealed record FluidIngredientChanged(int Index, string Name, double Value);
