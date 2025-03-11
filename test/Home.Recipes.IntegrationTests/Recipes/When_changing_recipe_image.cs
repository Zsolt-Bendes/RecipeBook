using Home.Recipes.Api.Features.Recipes.Commands;
using Home.Recipes.Domain.Recipes;
using System.Net;

namespace Home.Recipes.IntegrationTests.Recipes;

[Collection(nameof(Fixture))]
public sealed class When_changing_recipe_image
{
    private readonly Fixture _fixture;

    public When_changing_recipe_image(Fixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Given_recipe_does_not_exists_then_not_found_is_returned()
    {
        using var imageFile = File.OpenRead(RecipeConstants.DefaultImagePath);
        using var content = new StreamContent(imageFile);
        using var formData = new MultipartFormDataContent();

        // Act & Assert
        _ = await _fixture.Host.Scenario(_ =>
        {
            formData.Add(content, "Image", imageFile.Name);

            _.Post.MultipartFormData(formData).ToUrl(ChangeRecipeImageEndpoint.Endpoint.Replace("{recipeId}", Guid.NewGuid().ToString()));
            _.StatusCodeShouldBe(HttpStatusCode.NotFound);
        });
    }
}
