using Home.Recipes.Api.Features.Recipes;
using Home.Recipes.Domain.Recipes;
using Home.Recipes.Domain.Recipes.Events;
using Home.Recipes.Domain.Recipes.ValueObjects;
using Marten.Schema.Identity;
using System.Net;

namespace Home.Recipes.IntegrationTests.Recipes;

[Collection(nameof(Fixture))]
public sealed class When_adding_ingredient : IAsyncLifetime
{
    private readonly Fixture _fixture;
    private readonly Guid _recipeId = CombGuidIdGeneration.NewGuid();

    public When_adding_ingredient(Fixture fixture)
    {
        _fixture = fixture;
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
        await using var session = _fixture.Store.LightweightSession();

        var evt = new RecipeCreated(
            _recipeId,
            "name",
            "description",
            [],
            [],
            TimeSpan.FromMinutes(1),
            TimeSpan.FromMinutes(1),
            DateTime.UtcNow);

        session.Events.StartStream<Recipe>(evt.RecipeId, evt);

        await session.SaveChangesAsync();
    }

    [Fact]
    public async Task Given_recipe_does_not_exists_then_not_found_is_returned()
    {
        // Arrange
        var dto = new AddIngredientCommand(Guid.NewGuid(), IngredientType.Fluid, "ingredient", 1);

        // Act & Assert
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Json(dto).ToUrl(AddIngredientEndpoint.Endpoint);
            _.StatusCodeShouldBe(HttpStatusCode.NotFound);
        });
    }

    [Fact]
    public async Task Given_recipe_exists_and_ingredient_type_is_fluid_then_fluid_ingredient_is_added_to_the_recipe()
    {
        // Arrange
        var dto = new AddIngredientCommand(_recipeId, IngredientType.Fluid, "ingredient", 1);

        // Act 
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Json(dto).ToUrl(AddIngredientEndpoint.Endpoint);
            _.StatusCodeShouldBe(HttpStatusCode.NoContent);
        });

        // Assert
        var recipe = await _fixture.LoadRecipeFromDbAsync(_recipeId);

        recipe.Should().NotBeNull();
        recipe!.Ingredients.Should().HaveCount(1);
        recipe.Ingredients[0].Should().BeOfType(typeof(FluidIngredient));
    }

    [Fact]
    public async Task Given_recipe_exists_and_ingredient_type_is_mass_then_mass_ingredient_is_added_to_the_recipe()
    {
        // Arrange
        var dto = new AddIngredientCommand(_recipeId, IngredientType.Mass, "ingredient", 1);

        // Act 
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Json(dto).ToUrl(AddIngredientEndpoint.Endpoint);
            _.StatusCodeShouldBe(HttpStatusCode.NoContent);
        });

        // Assert
        var recipe = await _fixture.LoadRecipeFromDbAsync(_recipeId);

        recipe.Should().NotBeNull();
        recipe!.Ingredients.Should().HaveCount(1);
        recipe.Ingredients[0].Should().BeOfType(typeof(MassIngredient));
    }

    [Fact]
    public async Task Given_recipe_exists_and_ingredient_type_is_piece_then_piece_ingredient_is_added_to_the_recipe()
    {
        // Arrange
        var dto = new AddIngredientCommand(_recipeId, IngredientType.Piece, "ingredient", 1);

        // Act 
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Json(dto).ToUrl(AddIngredientEndpoint.Endpoint);
            _.StatusCodeShouldBe(HttpStatusCode.NoContent);
        });

        // Assert
        var recipe = await _fixture.LoadRecipeFromDbAsync(_recipeId);

        recipe.Should().NotBeNull();
        recipe!.Ingredients.Should().HaveCount(1);
        recipe.Ingredients[0].Should().BeOfType(typeof(PieceIngredient));
    }
}
