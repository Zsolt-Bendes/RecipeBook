using System.Net;

namespace Home.Recipes.IntegrationTests.Recipes;

[Collection(nameof(Fixture))]
public sealed class When_deleting_a_recipe
{
    private readonly Fixture _fixture;

    public When_deleting_a_recipe(Fixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Given_recipe_does_not_exists_then_not_found_is_returned()
    {
        // Act & Assert
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Delete.Url($"/recipes/{Guid.NewGuid()}");
            _.StatusCodeShouldBe(HttpStatusCode.NotFound);
        });
    }

    [Fact]
    public async Task Given_recipe_exists_then_the_recipe_is_removed_from_db()
    {
        // Arrange
        var evt = ObjectMother.Events.RecipeCreated.Default.Build();
        await _fixture.CreateRecipeAsync(evt);

        // Act
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Delete.Url($"/recipes/{evt.RecipeId}");
            _.StatusCodeShouldBe(HttpStatusCode.NoContent);
        });

        // Assert
        var recipeFromDb = await _fixture.LoadRecipeFromDbAsync(evt.RecipeId);

        recipeFromDb.Should().BeNull();
    }
}
