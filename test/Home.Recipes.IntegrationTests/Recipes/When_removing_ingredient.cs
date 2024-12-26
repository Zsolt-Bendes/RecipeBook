using Home.Recipes.Api.Features.Recipes;
using System.Net;

namespace Home.Recipes.IntegrationTests.Recipes;

[Collection(nameof(Fixture))]
public sealed class When_removing_ingredient
{
    private readonly Fixture _fixture;

    public When_removing_ingredient(Fixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Given_empty_recipe_id_then_bad_request_is_returned()
    {
        // Arrange
        var dto = new RemoveIngredientCommand(Guid.Empty, 0);

        // Act & Assert
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Json(dto).ToUrl(RemoveIngredientEndpoint.Endpoint);
            _.StatusCodeShouldBe(HttpStatusCode.BadRequest);
        });
    }

    [Fact]
    public async Task Given_index_is_negative_then_bad_request_is_returned()
    {
        // Arrange
        var dto = new RemoveIngredientCommand(Guid.Empty, -1);

        // Act & Assert
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Json(dto).ToUrl(RemoveIngredientEndpoint.Endpoint);
            _.StatusCodeShouldBe(HttpStatusCode.BadRequest);
        });
    }

    [Fact]
    public async Task Given_recipe_does_not_exists_then_not_found_is_returned()
    {
        // Arrange
        var dto = new RemoveIngredientCommand(Guid.NewGuid(), 1);

        // Act & Assert
        _ = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Url(RemoveIngredientEndpoint.Endpoint);
            _.Put.Json(dto);
            _.StatusCodeShouldBe(HttpStatusCode.NotFound);
        });
    }

    [Fact]
    public async Task Given_recipe_exists_with_ingredient_then_ingredient_is_removed_from_recipe()
    {
        // Arrange
        var evt = ObjectMother
           .Events
           .RecipeCreated
           .Default
           .WithPieceIngredients(_ => _.Build())
           .Build();
        await _fixture.CreateRecipeAsync(evt);

        var dto = new RemoveIngredientCommand(evt.RecipeId, 0);

        // Act
        _ = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Url(RemoveIngredientEndpoint.Endpoint);
            _.Put.Json(dto);
            _.StatusCodeShouldBe(HttpStatusCode.NoContent);
        });

        // Assert
        var recipeFromDb = await _fixture.LoadRecipeFromDbAsync(evt.RecipeId);

        recipeFromDb.Should().NotBeNull();
        recipeFromDb!.Ingredients.Should().HaveCount(0);
    }
}
