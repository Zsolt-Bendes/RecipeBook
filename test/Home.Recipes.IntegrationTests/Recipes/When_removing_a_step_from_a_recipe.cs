using Home.Recipes.Api.Features.Recipes;
using Home.Recipes.Domain.Recipes.ValueObjects;
using System.Net;

namespace Home.Recipes.IntegrationTests.Recipes;

[Collection(nameof(Fixture))]
public sealed class When_removing_a_step_from_a_recipe
{
    private readonly Fixture _fixture;

    public When_removing_a_step_from_a_recipe(Fixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Given_recipe_does_not_exists_then_not_found_is_returned()
    {
        // Arrange
        var dto = new RemoveRecipeStepCommand(Guid.NewGuid(), 1);

        // Act & Assert
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Url(RemoveRecipeStepEndpoint.Endpoint);
            _.Put.Json(dto);
            _.StatusCodeShouldBe(HttpStatusCode.NotFound);
        });
    }

    [Fact]
    public async Task Given_recipe_exists_with_a_step_then_step_is_removed_from_recipe()
    {
        // Arrange
        var evt = ObjectMother
            .Events
            .RecipeCreated
            .Default
            .WithSteps([new RecipeStep("cut the onions")])
            .Build();
        await _fixture.CreateRecipeAsync(evt);

        var dto = new RemoveRecipeStepCommand(evt.RecipeId, 0);

        // Act
        await _fixture.Host.Scenario(_ =>
        {
            _.Put.Url(RemoveRecipeStepEndpoint.Endpoint);
            _.Put.Json(dto);
            _.StatusCodeShouldBe(HttpStatusCode.NoContent);
        });

        // Assert
        var recipeFromDb = await _fixture.LoadRecipeFromDbAsync(evt.RecipeId);

        recipeFromDb!.Steps.Should().BeEmpty();
    }
}
