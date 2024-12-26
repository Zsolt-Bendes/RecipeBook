using Home.Recipes.Api.Features.Recipes.Queries;
using System.Net;

namespace Home.Recipes.IntegrationTests.Recipes;

[Collection(nameof(Fixture))]
public sealed class When_quiring_recipe_list
{
    private readonly Fixture _fixture;

    public When_quiring_recipe_list(Fixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Given_no_recipes_then_empty_list_is_returned()
    {
        // Arrange
        // Act & Assert
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Get.Url(RecipeListEndpoint.Endpoint);
            _.StatusCodeShouldBe(HttpStatusCode.OK);
            _.ContentShouldBe("{\"recipes\":[]}");
        });
    }

    [Fact]
    public async Task Given_recipes_exists_then_list_is_returned()
    {
        // Arrange
        var existingRecipe = ObjectMother
            .Events
            .RecipeCreated
            .Default
            .Build();

        await _fixture.CreateRecipeAsync(existingRecipe);

        // Act
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Get.Url(RecipeListEndpoint.Endpoint);
        });

        // Assert
        var responseContent = await response.ReadAsJsonAsync<RecipeListQueryResponse>();

        responseContent.Recipes.Should().HaveCount(1);
    }
}
