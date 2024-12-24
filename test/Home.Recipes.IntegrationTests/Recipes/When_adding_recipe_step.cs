using Home.Recipes.Api.Features.Recipes;
using System.Net;

namespace Home.Recipes.IntegrationTests.Recipes;

[Collection(nameof(Fixture))]
public sealed class When_adding_recipe_step : IAsyncLifetime
{
    private readonly Fixture _fixture;
    private Guid _recipeId;

    public When_adding_recipe_step(Fixture fixture)
    {
        _fixture = fixture;
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    [Fact]
    public async Task Given_recipe_does_not_exists_then_not_found_is_returned()
    {
        // Arrange
        var dto = new AddRecipeStepCommand(Guid.NewGuid(), "Cut the onions small");

        // Act & Assert
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Json(dto).ToUrl(AddRecipeStepEndpoint.Endpoint);
            _.StatusCodeShouldBe(HttpStatusCode.NotFound);
        });
    }

    [Fact]
    public async Task Given_recipe_exists_then_new_step_is_added_to_the_end_of_steps()
    {
        // Arrange
        var dto = new AddRecipeStepCommand(_recipeId, "Cut the onions small");

        // Act
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Json(dto).ToUrl(AddRecipeStepEndpoint.Endpoint);
            _.StatusCodeShouldBe(HttpStatusCode.NoContent);
        });

        // Assert
        var recipeFromDb = await _fixture.LoadRecipeFromDbAsync(_recipeId);

        recipeFromDb.Should().NotBeNull();
        recipeFromDb!.Steps.Should().HaveCount(1);
    }

    public async Task InitializeAsync()
    {
        var evt = ObjectMother.Events.RecipeCreated.Default.Build();
        _recipeId = evt.RecipeId;

        await _fixture.CreateRecipeAsync(evt);
    }
}
