using Home.Recipes.Api.Features.Recipes;
using Home.Recipes.Domain.Recipes.ValueObjects;
using System.Net;

namespace Home.Recipes.IntegrationTests.Recipes;

[Collection(nameof(Fixture))]
public sealed class When_changing_name_and_description_step
{
    private readonly Fixture _fixture;

    public When_changing_name_and_description_step(Fixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Given_empty_recipe_id_then_bad_request_is_returned()
    {
        // Arrange
        var dto = new ChangeRecipeNameAndDescriptionCommand(Guid.Empty, "name", "description");

        // Act & Assert
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Json(dto).ToUrl(ChangeRecipeNameAndDescriptionEndpoint.Endpoint);
            _.StatusCodeShouldBe(HttpStatusCode.BadRequest);
        });
    }

    [Fact]
    public async Task Given_name_and_description_is_empty_then_bad_request_is_returned()
    {
        // Arrange
        var dto = new ChangeRecipeNameAndDescriptionCommand(Guid.NewGuid(), string.Empty, string.Empty);

        // Act & Assert
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Json(dto).ToUrl(ChangeRecipeNameAndDescriptionEndpoint.Endpoint);
            _.StatusCodeShouldBe(HttpStatusCode.BadRequest);
        });
    }

    [Fact]
    public async Task When_recipe_does_not_exists_then_not_found_is_returned()
    {
        // Arrange
        var dto = new ChangeRecipeNameAndDescriptionCommand(Guid.NewGuid(), "name", "description");

        // Act & Assert
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Url(ChangeRecipeNameAndDescriptionEndpoint.Endpoint);
            _.Put.Json(dto);
            _.StatusCodeShouldBe(HttpStatusCode.NotFound);
        });
    }

    [Fact]
    public async Task When_recipe_exists_and_dto_contains_name_then_only_recipe_name_is_updated()
    {
        // Arrange
        var recipeCreated = ObjectMother
           .Events
           .RecipeCreated
           .Default
           .WithSteps([new RecipeStep("step 1")])
           .Build();
        await _fixture.CreateRecipeAsync(recipeCreated);

        var dto = new ChangeRecipeNameAndDescriptionCommand(recipeCreated.RecipeId, "new name", null);

        // Act
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Url(ChangeRecipeNameAndDescriptionEndpoint.Endpoint);
            _.Put.Json(dto);
            _.StatusCodeShouldBe(HttpStatusCode.NoContent);
        });

        // Assert
        var recipeFromDb = await _fixture.LoadRecipeFromDbAsync(recipeCreated.RecipeId);

        recipeFromDb.Should().NotBeNull();
        recipeFromDb!.Name.Name.Should().Be(dto.Name);
        recipeFromDb!.Description.Description.Should().Be(recipeCreated.Description);
    }

    [Fact]
    public async Task When_recipe_exists_and_dto_contains_description_then_only_recipe_description_is_updated()
    {
        // Arrange
        var recipeCreated = ObjectMother
           .Events
           .RecipeCreated
           .Default
           .WithSteps([new RecipeStep("step 1")])
           .Build();
        await _fixture.CreateRecipeAsync(recipeCreated);

        var dto = new ChangeRecipeNameAndDescriptionCommand(recipeCreated.RecipeId, null, "new description");

        // Act
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Url(ChangeRecipeNameAndDescriptionEndpoint.Endpoint);
            _.Put.Json(dto);
            _.StatusCodeShouldBe(HttpStatusCode.NoContent);
        });

        // Assert
        var recipeFromDb = await _fixture.LoadRecipeFromDbAsync(recipeCreated.RecipeId);

        recipeFromDb.Should().NotBeNull();
        recipeFromDb!.Description.Description.Should().Be(dto.Description);
        recipeFromDb!.Name.Name.Should().Be(recipeCreated.Name);
    }
}
