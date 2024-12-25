using Home.Recipes.Api.Features.Recipes;
using System.Net;

namespace Home.Recipes.IntegrationTests.Recipes;

[Collection(nameof(Fixture))]
public sealed class When_changing_preparation_time
{
    private readonly Fixture _fixture;

    public When_changing_preparation_time(Fixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Given_recipe_does_not_exists_then_not_found_is_returned()
    {
        // Arrange
        var dto = new ChangePreparationTimeCommand(Guid.NewGuid(), TimeSpan.FromMinutes(50));

        // Act & Assert
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Url(ChangePreparationTimeEndpoint.Endpoint);
            _.Put.Json(dto);
            _.StatusCodeShouldBe(HttpStatusCode.NotFound);
        });
    }

    [Fact]
    public async Task Given_recipe_exists_then_preparation_time_is_updated()
    {
        // Arrange
        var evt = ObjectMother.Events.RecipeCreated.Default.Build();
        await _fixture.CreateRecipeAsync(evt);

        var dto = new ChangePreparationTimeCommand(evt.RecipeId, TimeSpan.FromMinutes(50));

        // Act
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Url(ChangePreparationTimeEndpoint.Endpoint);
            _.Put.Json(dto);
            _.StatusCodeShouldBe(HttpStatusCode.NoContent);
        });

        // Assert
        var recipeFromDb = await _fixture.LoadRecipeFromDbAsync(evt.RecipeId);

        recipeFromDb.Should().NotBeNull();
        recipeFromDb!.PreparationTime.Time.Should().Be(dto.Time);
    }

}
