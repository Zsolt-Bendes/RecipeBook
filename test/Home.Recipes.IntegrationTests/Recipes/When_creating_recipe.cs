using Home.Recipes.Api.Features.Recipes.Commands;
using Home.Recipes.Domain.Recipes;
using System.Net;

namespace Home.Recipes.IntegrationTests.Recipes;

[Collection(nameof(Fixture))]
public sealed class When_creating_recipe
{
    private readonly Fixture _fixture;

    public When_creating_recipe(Fixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Given_name_is_to_long_then_bad_request_is_returned()
    {
        // Arrange
        var dto = new CreateRecipeCommand(
            _fixture.GetLongTestString(RecipeConstants.MaxNameLength),
            "Sweet and warm",
            TimeSpan.FromMinutes(40),
            TimeSpan.FromHours(1));

        // Act & Assert
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Json(dto).ToUrl(ChangeRecipeStepEndpoint.Endpoint);
            _.StatusCodeShouldBe(HttpStatusCode.BadRequest);
        });
    }

    [Fact]
    public async Task Given_name_is_empty_then_bad_request_is_returned()
    {
        // Arrange
        var dto = new CreateRecipeCommand(
            string.Empty,
            "Sweet and warm",
            TimeSpan.FromMinutes(40),
            TimeSpan.FromHours(1));

        // Act & Assert
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Json(dto).ToUrl(ChangeRecipeStepEndpoint.Endpoint);
            _.StatusCodeShouldBe(HttpStatusCode.BadRequest);
        });
    }

    [Fact]
    public async Task Given_description_is_empty_then_bad_request_is_returned()
    {
        // Arrange
        var dto = new CreateRecipeCommand(
            "American Pie",
            string.Empty,
            TimeSpan.FromMinutes(40),
            TimeSpan.FromHours(1));

        // Act & Assert
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Json(dto).ToUrl(ChangeRecipeStepEndpoint.Endpoint);
            _.StatusCodeShouldBe(HttpStatusCode.BadRequest);
        });
    }

    [Fact]
    public async Task Given_description_is_to_long_then_bad_request_is_returned()
    {
        // Arrange
        var dto = new CreateRecipeCommand(
            "American Pie",
            _fixture.GetLongTestString(RecipeConstants.MaxDescriptionLength + 1),
            TimeSpan.FromMinutes(40),
            TimeSpan.FromHours(1));

        // Act & Assert
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Json(dto).ToUrl(ChangeRecipeStepEndpoint.Endpoint);
            _.StatusCodeShouldBe(HttpStatusCode.BadRequest);
        });
    }

    [Fact]
    public async Task Given_preparation_time_is_negative_then_bad_request_is_returned()
    {
        // Arrange
        var dto = new CreateRecipeCommand(
            "American Pie",
            "Sweet and warm",
            TimeSpan.FromMinutes(-40),
            TimeSpan.FromHours(1));

        // Act & Assert
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Json(dto).ToUrl(ChangeRecipeStepEndpoint.Endpoint);
            _.StatusCodeShouldBe(HttpStatusCode.BadRequest);
        });
    }

    [Fact]
    public async Task Given_cooking_time_is_negative_then_bad_request_is_returned()
    {
        // Arrange
        var dto = new CreateRecipeCommand(
            "American Pie",
            "Sweet and warm",
            TimeSpan.FromMinutes(40),
            TimeSpan.FromHours(-1));

        // Act & Assert
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Put.Json(dto).ToUrl(ChangeRecipeStepEndpoint.Endpoint);
            _.StatusCodeShouldBe(HttpStatusCode.BadRequest);
        });
    }

    [Fact]
    public async Task Given_recipe_does_not_exists_then_new_recipe_is_created_and_stored()
    {
        // Arrange
        var dto = new CreateRecipeCommand("American Pie", "Sweet and warm", TimeSpan.FromMinutes(40), TimeSpan.FromHours(1));

        // Act
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Post.Url(CreateRecipeEndpoint.Endpoint);
            _.Post.Json(dto);

            _.StatusCodeShouldBeSuccess();
        });

        // Assert
        var recipeFromDb = await _fixture.LoadRecipeFromDbAsync((await response.ReadAsJsonAsync<CreateRecipeResponse>()).RecipeId);

        recipeFromDb.Should().NotBeNull();
        recipeFromDb!.Name.Name.Should().Be(dto.Name);
    }

    [Fact]
    public async Task Given_recipe_with_same_name_exists_then_bad_request_is_returned()
    {
        // Arrange
        var dto = new CreateRecipeCommand("American Pie2", "Sweet and warm", TimeSpan.FromMinutes(40), TimeSpan.FromHours(1));

        _ = await _fixture.Host.Scenario(_ =>
        {
            _.Post.Url(CreateRecipeEndpoint.Endpoint);
            _.Post.Json(dto);

            _.IgnoreStatusCode();
        });

        // Act & Assert
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Post.Url(CreateRecipeEndpoint.Endpoint);
            _.Post.Json(dto);

            _.StatusCodeShouldBe(HttpStatusCode.BadRequest);
        });
    }
}
