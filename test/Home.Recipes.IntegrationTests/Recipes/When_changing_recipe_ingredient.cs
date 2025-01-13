using Home.Recipes.Api.Features.Recipes.Commands;
using Home.Recipes.Domain.Recipes.ValueObjects;
using System.Net;

namespace Home.Recipes.IntegrationTests.Recipes;

[Collection(nameof(Fixture))]
public sealed class When_changing_recipe_ingredient
{
    private readonly Fixture _fixture;

    public When_changing_recipe_ingredient(Fixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Given_Ingredient_does_not_exists_then_not_found_is_returned()
    {
        // Arrange
        var createRecipeEvent = ObjectMother.Events.RecipeCreated.Default.Build();
        await _fixture.CreateRecipeAsync(createRecipeEvent);

        var dto = new ChangeRecipeIngredientCommand(createRecipeEvent.RecipeId, 0, "ingredient", 1);

        // Act & Assert
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Json(dto).ToUrl(ChangeRecipeIngredientEndpoint.Endpoint);
            _.StatusCodeShouldBe(HttpStatusCode.NotFound);
        });
    }

    [Fact]
    public async Task Given_recipe_does_not_exists_then_not_found_is_returned()
    {
        // Arrange
        var dto = new ChangeRecipeIngredientCommand(Guid.NewGuid(), 0, "ingredient", 1);

        // Act & Assert
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Json(dto).ToUrl(ChangeRecipeIngredientEndpoint.Endpoint);
            _.StatusCodeShouldBe(HttpStatusCode.NotFound);
        });
    }

    [Fact]
    public async Task Given_Ingredient_exist_and_is_type_of_piece_then_ingredient_name_is_updated()
    {
        // Arrange
        var createRecipeEvent = ObjectMother.Events.RecipeCreated.Default.WithPieceIngredients(_ => _.Build()).Build();
        await _fixture.CreateRecipeAsync(createRecipeEvent);

        var dto = new ChangeRecipeIngredientCommand(createRecipeEvent.RecipeId, 0, "ingredient 2", 1);

        // Act
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Json(dto).ToUrl(ChangeRecipeIngredientEndpoint.Endpoint);
            _.StatusCodeShouldBe(HttpStatusCode.NoContent);
        });

        // Assert
        var recipe = await _fixture.LoadRecipeFromDbAsync(createRecipeEvent.RecipeId);

        recipe.Should().NotBeNull();
        recipe!.Ingredients[0].IngredientName.Should().Be("ingredient 2");
    }

    [Fact]
    public async Task Given_Ingredient_exist_and_is_type_of_fluid_then_ingredient_name_is_updated()
    {
        // Arrange
        var createRecipeEvent = ObjectMother.Events.RecipeCreated.Default.WithFluidIngredients(_ => _.Build()).Build();
        await _fixture.CreateRecipeAsync(createRecipeEvent);

        var dto = new ChangeRecipeIngredientCommand(createRecipeEvent.RecipeId, 0, "ingredient 2", 1);

        // Act
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Json(dto).ToUrl(ChangeRecipeIngredientEndpoint.Endpoint);
            _.StatusCodeShouldBe(HttpStatusCode.NoContent);
        });

        // Assert
        var recipe = await _fixture.LoadRecipeFromDbAsync(createRecipeEvent.RecipeId);

        recipe.Should().NotBeNull();
        recipe!.Ingredients[0].IngredientName.Should().Be("ingredient 2");
    }

    [Fact]
    public async Task Given_Ingredient_exist_and_is_type_of_mass_then_ingredient_name_is_updated()
    {
        // Arrange
        var createRecipeEvent = ObjectMother.Events.RecipeCreated.Default.WithMassIngredients(_ => _.Build()).Build();
        await _fixture.CreateRecipeAsync(createRecipeEvent);

        var dto = new ChangeRecipeIngredientCommand(createRecipeEvent.RecipeId, 0, "ingredient 2", 1);

        // Act
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Json(dto).ToUrl(ChangeRecipeIngredientEndpoint.Endpoint);
            _.StatusCodeShouldBe(HttpStatusCode.NoContent);
        });

        // Assert
        var recipe = await _fixture.LoadRecipeFromDbAsync(createRecipeEvent.RecipeId);

        recipe.Should().NotBeNull();
        recipe!.Ingredients[0].IngredientName.Should().Be("ingredient 2");
    }

    [Fact]
    public async Task Given_Ingredient_exist_then_value_is_updated()
    {
        // Arrange
        var createRecipeEvent = ObjectMother.Events.RecipeCreated.Default.WithPieceIngredients(_ => _.Build()).Build();
        await _fixture.CreateRecipeAsync(createRecipeEvent);

        var dto = new ChangeRecipeIngredientCommand(createRecipeEvent.RecipeId, 0, "ingredient 2", 2);

        // Act
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Json(dto).ToUrl(ChangeRecipeIngredientEndpoint.Endpoint);
            _.StatusCodeShouldBe(HttpStatusCode.NoContent);
        });

        // Assert
        var recipe = await _fixture.LoadRecipeFromDbAsync(createRecipeEvent.RecipeId);

        recipe.Should().NotBeNull();
        (recipe!.Ingredients[0] as PieceIngredient)!.Pieces.Should().Be(2);
    }
}
