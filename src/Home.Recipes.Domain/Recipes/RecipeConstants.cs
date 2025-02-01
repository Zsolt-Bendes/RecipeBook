namespace Home.Recipes.Domain.Recipes;

public static class RecipeConstants
{
    public const int MaxNameLength = 200;
    public const int MaxDescriptionLength = 600;
    public const int MaxStepLength = 400;

    public const int MaxIngredientNameLength = 80;

    public const string StaticFilesPath = "StaticFiles/";
    public const string DefaultImagePath = StaticFilesPath + "default_cooking.jpg";
    public const string DefaultThumbnailPath = StaticFilesPath + "thumb_default_cooking.jpg";
}
