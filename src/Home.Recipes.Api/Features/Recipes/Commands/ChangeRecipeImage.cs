using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Home.Recipes.Api.Features.Recipes.Commands;

public sealed class ChangeRecipeImageEndpoint
{
    internal const string Endpoint = "/recipes/changeImage";

    public static async Task<ProblemDetails> LoadAsync(
        Guid recipeId,
        IDocumentSession session,
        CancellationToken cancellationToken)
    {
        var recipe = await session.LoadAsync<Recipe>(recipeId, cancellationToken);
        if (recipe is null)
        {
            return
                new ProblemDetails
                {
                    Title = "Recipe not found",
                    Status = (int)HttpStatusCode.NotFound,
                    Detail = $"Recipe with id {recipeId} not found",
                };
        }

        return WolverineContinue.NoProblems;
    }

    [WolverinePost(Endpoint)]
    [EmptyResponse]
    public static async Task<RecipeImageAdded> Post(
        [FromForm(Name = "RecipeId")] Guid recipeId,
        [FromForm(Name = "Image")] IFormFile image,
        IDocumentSession session,
        CancellationToken cancellationToken)
    {
        var imagePath = RecipeConstants.StaticFilesPath + recipeId;

        await CreateThumbnailAsync(image, recipeId, cancellationToken);
        await CreateCoverImageAsync(image, recipeId, cancellationToken);

        return new RecipeImageAdded();
    }

    private static async Task CreateThumbnailAsync(
        IFormFile file,
        Guid recipeId,
        CancellationToken cancellationToken)
    {
        var imagePath = RecipeConstants.StaticFilesPath + "thumb_" + recipeId + ".jpeg";
        using Image image = await Image.LoadAsync(file.OpenReadStream(), cancellationToken);

        int desiredWidth = 300;
        int desiredHeight = 0;

        image.Mutate(x => x.Resize(new ResizeOptions
        {
            Size = new Size(desiredWidth, desiredHeight),
            Mode = ResizeMode.Max
        }));

        await image.SaveAsJpegAsync(imagePath, cancellationToken);
    }

    private static async Task CreateCoverImageAsync(
       IFormFile file,
       Guid recipeId,
       CancellationToken cancellationToken)
    {
        var imagePath = RecipeConstants.StaticFilesPath + recipeId + ".jpeg";
        using Image image = await Image.LoadAsync(file.OpenReadStream(), cancellationToken);

        int desiredWidth = 900;
        int desiredHeight = 0;

        image.Mutate(x => x.Resize(new ResizeOptions
        {
            Size = new Size(desiredWidth, desiredHeight),
            Mode = ResizeMode.Max
        }));

        await image.SaveAsJpegAsync(imagePath, cancellationToken);
    }
}
