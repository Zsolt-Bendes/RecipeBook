using Home.Recipes.Domain.Recipes.Events;
using Home.Recipes.Domain.Recipes.Exceptions;
using Home.Recipes.Domain.Recipes.ValueObjects;
using UnitsNet.Units;

namespace Home.Recipes.Domain.Recipes;

public sealed record Recipe(
    Guid Id,
    RecipeName Name,
    RecipeDescription Description,
    RecipeTime PreparationTime,
    RecipeTime CookingTime,
    string ImagePath,
    List<IngredientBase> Ingredients,
    List<RecipeStep> Steps)
{
    private const string DefaultImagePath = "StaticFiles/default_cooking.jpg";

    public static Recipe Create(RecipeCreated recipeCreated) => new Recipe(
            recipeCreated.RecipeId,
            new RecipeName(recipeCreated.Name),
            new RecipeDescription(recipeCreated.Description),
            new RecipeTime(recipeCreated.PreparationTime),
            new RecipeTime(recipeCreated.CookingTime),
            DefaultImagePath,
            recipeCreated.Ingredients,
            recipeCreated.Steps);

    public static Recipe Apply(CookingTimeAdjusted timeAdjusted, Recipe recipe) =>
        recipe with { CookingTime = new RecipeTime(timeAdjusted.Time) };

    public static void Apply(FluidIngredientAdded ingredientAdded, Recipe recipe)
    {
        if (recipe.Ingredients.Any(_ => _.IngredientName == ingredientAdded.IngredientName))
        {
            var index = recipe.Ingredients.FindIndex(_ => _.IngredientName == ingredientAdded.IngredientName);
            recipe.Ingredients[index] = new FluidIngredient(ingredientAdded.IngredientName, ingredientAdded.Unit, ingredientAdded.Value);
        }
        else
        {
            recipe.Ingredients.Add(new FluidIngredient(ingredientAdded.IngredientName, ingredientAdded.Unit, ingredientAdded.Value));
        }
    }

    public static void Apply(MassIngredientAdded ingredientAdded, Recipe recipe)
    {
        if (recipe.Ingredients.Any(_ => _.IngredientName == ingredientAdded.IngredientName))
        {
            var index = recipe.Ingredients.FindIndex(_ => _.IngredientName == ingredientAdded.IngredientName);
            recipe.Ingredients[index] = new MassIngredient(ingredientAdded.IngredientName, ingredientAdded.Unit, ingredientAdded.Value);
        }
        else
        {
            recipe.Ingredients.Add(new MassIngredient(ingredientAdded.IngredientName, ingredientAdded.Unit, ingredientAdded.Value));
        }
    }

    public static void Apply(PieceIngredientAdded ingredientAdded, Recipe recipe)
    {
        if (recipe.Ingredients.Any(_ => _.IngredientName == ingredientAdded.IngredientName))
        {
            var index = recipe.Ingredients.FindIndex(_ => _.IngredientName == ingredientAdded.IngredientName);
            recipe.Ingredients[index] = new PieceIngredient(ingredientAdded.IngredientName, ingredientAdded.Value);
        }
        else
        {
            recipe.Ingredients.Add(new PieceIngredient(ingredientAdded.IngredientName, ingredientAdded.Value));
        }
    }

    public static void Apply(IngredientRemoved ingredientRemoved, Recipe recipe)
    {
        if (ingredientRemoved.Index < 0 || ingredientRemoved.Index >= recipe.Ingredients.Count)
        {
            throw new IngredientNotFoundException(recipe.Id, ingredientRemoved.Index);
        }

        recipe.Ingredients.RemoveAt(ingredientRemoved.Index);
    }

    public static Recipe Apply(PreparationTimeAdjusted timeAdjusted, Recipe recipe) =>
       recipe with { PreparationTime = new RecipeTime(timeAdjusted.Time) };

    public static Recipe Apply(RecipeDescriptionChanged descriptionChanged, Recipe recipe) =>
       recipe with { Description = new RecipeDescription(descriptionChanged.Description) };

    public static Recipe Apply(RecipeNameChanged nameChanged, Recipe recipe) =>
      recipe with { Name = new RecipeName(nameChanged.Name) };

    public static void Apply(StepAdded stepAdded, Recipe recipe) => recipe.Steps.Add(new RecipeStep(stepAdded.Text));

    public static void Apply(StepRemoved stepRemoved, Recipe recipe)
    {
        if (stepRemoved.Index < 0 || stepRemoved.Index >= recipe.Steps.Count)
        {
            throw new StepNotFoundException(recipe.Id, stepRemoved.Index);
        }

        recipe.Steps.RemoveAt(stepRemoved.Index);
    }

    public static void Apply(RecipeStepUpdated stepUpdated, Recipe recipe)
    {
        if (stepUpdated.Index < 0 || stepUpdated.Index >= recipe.Steps.Count)
        {
            throw new StepNotFoundException(recipe.Id, stepUpdated.Index);
        }

        recipe.Steps[stepUpdated.Index] = new RecipeStep(stepUpdated.Text);
    }

    public static void Apply(IngredientChanged pieceIngredientChanged, Recipe recipe)
    {
        if (pieceIngredientChanged.Index < 0 || pieceIngredientChanged.Index >= recipe.Ingredients.Count)
        {
            throw new IngredientNotFoundException(recipe.Id, pieceIngredientChanged.Index);
        }

        if (recipe.Ingredients[pieceIngredientChanged.Index] is PieceIngredient)
        {
            recipe.Ingredients[pieceIngredientChanged.Index] = new PieceIngredient(
                pieceIngredientChanged.Name,
                pieceIngredientChanged.Value);
        }

        if (recipe.Ingredients[pieceIngredientChanged.Index] is MassIngredient)
        {
            recipe.Ingredients[pieceIngredientChanged.Index] = new MassIngredient(
                pieceIngredientChanged.Name,
                MassUnit.Milligram,
                pieceIngredientChanged.Value);
        }

        if (recipe.Ingredients[pieceIngredientChanged.Index] is FluidIngredient)
        {
            recipe.Ingredients[pieceIngredientChanged.Index] = new FluidIngredient(
                pieceIngredientChanged.Name,
                VolumeUnit.Milliliter,
                pieceIngredientChanged.Value);
        }
    }

    public static bool ShouldDelete(RecipeDeleted recipeDeleted, Recipe recipe) => true;
}
