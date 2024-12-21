using Home.Recipes.Api.Features.Recipes;
using Home.Recipes.Domain.Recipes;
using System.Net;

namespace Home.Recipes.IntegrationTests.Recipes;

[Collection(nameof(Fixture))]
public sealed class When_creating_recipe
{
    private readonly Fixture _fixture;

    public When_creating_recipe(Fixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Given_recipe_does_not_exists_then_new_recipe_is_created_and_stored()
    {
        // Arrange
        var dto = new CreateRecipeCommand("American Pie", "Sweet and warm", TimeSpan.FromMinutes(40), TimeSpan.FromHours(1));

        // Act
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Post.Url(CreateRecipeEndpoint.Endpoint);
            _.Post.Json(dto);

            _.StatusCodeShouldBeSuccess();
        });

        // Assert
        var recipeFromDb = await LoadRecipeFromDbAsync((await response.ReadAsJsonAsync<CreateRecipeResponse>()).RecipeId);

        recipeFromDb.Should().NotBeNull();
        recipeFromDb!.Name.Name.Should().Be(dto.Name);
    }

    [Fact]
    public async Task Given_recipe_with_same_name_exists_then_bad_request_is_returned()
    {
        // Arrange
        var dto = new CreateRecipeCommand("American Pie2", "Sweet and warm", TimeSpan.FromMinutes(40), TimeSpan.FromHours(1));

        _ = await _fixture.Host.Scenario(_ =>
        {
            _.Post.Url(CreateRecipeEndpoint.Endpoint);
            _.Post.Json(dto);

            _.IgnoreStatusCode();
        });

        // Act & Assert
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Post.Url(CreateRecipeEndpoint.Endpoint);
            _.Post.Json(dto);

            _.StatusCodeShouldBe(HttpStatusCode.BadRequest);
        });
    }

    private async Task<Recipe?> LoadRecipeFromDbAsync(Guid recipeId)
    {
        await using var session = await _fixture.Store.LightweightSerializableSessionAsync();

        return await session.LoadAsync<Recipe>(recipeId);
    }
}
