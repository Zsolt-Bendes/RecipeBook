using Home.Recipes.Api.Features.Recipes.Commands;
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
    public async Task Given_empty_recipe_id_then_bad_request_is_returned()
    {
        // Arrange
        var dto = new AddIngredientCommand(Guid.Empty, IngredientType.Fluid, "ingredient", 0);

        // Act & Assert
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Json(dto).ToUrl(AddIngredientEndpoint.Endpoint);
            _.StatusCodeShouldBe(HttpStatusCode.BadRequest);
        });
    }

    [Fact]
    public async Task Given_dto_with_empty_ingredient_name_then_bad_request_is_returned()
    {
        // Arrange
        var dto = new AddIngredientCommand(_recipeId, IngredientType.Fluid, string.Empty, 0);

        // Act & Assert
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Json(dto).ToUrl(AddIngredientEndpoint.Endpoint);
            _.StatusCodeShouldBe(HttpStatusCode.BadRequest);
        });
    }

    [Fact]
    public async Task Given_dto_with_negative_value_then_bad_request_is_returned()
    {
        // Arrange
        var dto = new AddIngredientCommand(_recipeId, IngredientType.Fluid, "ingredient", -1);

        // Act & Assert
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Json(dto).ToUrl(AddIngredientEndpoint.Endpoint);
            _.StatusCodeShouldBe(HttpStatusCode.BadRequest);
        });
    }

    [Fact]
    public async Task Given_dto_with_to_long_ingredient_name_then_bad_request_is_returned()
    {
        // Arrange
        var dto = new AddIngredientCommand(
            _recipeId,
            IngredientType.Fluid,
            _fixture.GetLongTestString(RecipeConstants.MaxIngredientNameLength + 1),
            1);

        // Act & Assert
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Json(dto).ToUrl(AddIngredientEndpoint.Endpoint);
            _.StatusCodeShouldBe(HttpStatusCode.BadRequest);
        });
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

    [Fact]
    public async Task Given_recipe_with_same_piece_recipe_name_exists_then_ingredient_value_is_updated()
    {
        // Arrange
        const string expectedName = "paprika";
        var existingRecipe = ObjectMother
            .Events
            .RecipeCreated
            .Default
            .WithPieceIngredients(_ => _.WithIngredientName(expectedName))
            .Build();

        await _fixture.CreateRecipeAsync(existingRecipe);
        var dto = new AddIngredientCommand(existingRecipe.RecipeId, IngredientType.Piece, expectedName, 2);

        // Act 
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Json(dto).ToUrl(AddIngredientEndpoint.Endpoint);
            _.StatusCodeShouldBe(HttpStatusCode.NoContent);
        });

        // Assert
        var recipe = await _fixture.LoadRecipeFromDbAsync(existingRecipe.RecipeId);

        recipe.Should().NotBeNull();
        recipe!.Ingredients.Should().HaveCount(1);
        (recipe.Ingredients[0] as PieceIngredient)!.Pieces.Should().Be(2);
    }

    [Fact]
    public async Task Given_recipe_with_same_mass_recipe_name_exists_then_ingredient_value_is_updated()
    {
        // Arrange
        const string expectedName = "paprika";
        var existingRecipe = ObjectMother
            .Events
            .RecipeCreated
            .Default
            .WithMassIngredients(_ => _.WithIngredientName(expectedName))
            .Build();

        await _fixture.CreateRecipeAsync(existingRecipe);
        var dto = new AddIngredientCommand(existingRecipe.RecipeId, IngredientType.Mass, expectedName, 2);

        // Act 
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Json(dto).ToUrl(AddIngredientEndpoint.Endpoint);
            _.StatusCodeShouldBe(HttpStatusCode.NoContent);
        });

        // Assert
        var recipe = await _fixture.LoadRecipeFromDbAsync(existingRecipe.RecipeId);

        recipe.Should().NotBeNull();
        recipe!.Ingredients.Should().HaveCount(1);
        (recipe.Ingredients[0] as MassIngredient)!.WightInGramm.Should().Be(2);
    }

    [Fact]
    public async Task Given_recipe_with_same_fluid_recipe_name_exists_then_ingredient_value_is_updated()
    {
        // Arrange
        const string expectedName = "paprika";
        var existingRecipe = ObjectMother
            .Events
            .RecipeCreated
            .Default
            .WithFluidIngredients(_ => _.WithIngredientName(expectedName))
            .Build();

        await _fixture.CreateRecipeAsync(existingRecipe);
        var dto = new AddIngredientCommand(existingRecipe.RecipeId, IngredientType.Fluid, expectedName, 2);

        // Act 
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Json(dto).ToUrl(AddIngredientEndpoint.Endpoint);
            _.StatusCodeShouldBe(HttpStatusCode.NoContent);
        });

        // Assert
        var recipe = await _fixture.LoadRecipeFromDbAsync(existingRecipe.RecipeId);

        recipe.Should().NotBeNull();
        recipe!.Ingredients.Should().HaveCount(1);
        (recipe.Ingredients[0] as FluidIngredient)!.VolumeInMilliliter.Should().Be(2);
    }
}
