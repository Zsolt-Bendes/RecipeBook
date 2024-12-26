using Home.Recipes.Api.Features.Recipes;
using Home.Recipes.Domain.Recipes.ValueObjects;
using System.Net;

namespace Home.Recipes.IntegrationTests.Recipes;

[Collection(nameof(Fixture))]
public sealed class When_changing_recipe_step
{
    private readonly Fixture _fixture;

    public When_changing_recipe_step(Fixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task When_recipe_does_not_exists_then_not_found_is_returned()
    {
        // Arrange
        var dto = new ChangeRecipeStepCommand(Guid.NewGuid(), 0, "updated value");

        // Act & Assert
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Url(ChangeRecipeStepEndpoint.Endpoint);
            _.Put.Json(dto);
            _.StatusCodeShouldBe(HttpStatusCode.NotFound);
        });
    }

    [Fact]
    public async Task When_recipe_exists_with_step_then_step_is_updated()
    {
        // Arrange
        var recipeCreated = ObjectMother
            .Events
            .RecipeCreated
            .Default
            .WithSteps([new RecipeStep("step 1")])
            .Build();
        await _fixture.CreateRecipeAsync(recipeCreated);

        var dto = new ChangeRecipeStepCommand(recipeCreated.RecipeId, 0, "updated value");

        // Act 
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Url(ChangeRecipeStepEndpoint.Endpoint);
            _.Put.Json(dto);
            _.StatusCodeShouldBe(HttpStatusCode.NoContent);
        });

        // Assert
        var recipeFromDb = await _fixture.LoadRecipeFromDbAsync(recipeCreated.RecipeId);

        recipeFromDb.Should().NotBeNull();
        recipeFromDb!.Steps[0].Text.Should().Be(dto.Text);
    }
}
