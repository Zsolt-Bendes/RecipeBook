using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Wolverine.Http.Marten;

namespace Home.Recipes.Api.Features.Recipes.Commands;

public sealed class ChangeRecipeImageEndpoint
{
    internal const string Endpoint = "/recipes/{recipeId}/changeImage";

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
        IFormFile image,
        [Aggregate] Recipe recipe,
        CancellationToken cancellationToken)
    {
        var imagePath = RecipeConstants.StaticFilesPath + recipe.Id;
        using (var stream = File.Create(imagePath))
        {
            await CreateThumbnailAsync(image, recipe.Id, cancellationToken);
            await CreateCoverImageAsync(image, recipe.Id, cancellationToken);
        }

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
