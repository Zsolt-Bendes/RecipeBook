using Home.Recipes.Api.Features.RecipeHistory.Queries;
using Home.Recipes.Domain.RecipeHistory.Events;
using Microsoft.VisualBasic;
using System.Net;

namespace Home.Recipes.IntegrationTests.RecipeHistory;

[Collection(nameof(Fixture))]
public sealed class When_marking_a_recipe_as_cooked
{
    private readonly Fixture _fixture;

    public When_marking_a_recipe_as_cooked(Fixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Given_recipe_does_not_exists_then_empty_list_is_returned()
    {
        // Arrange
        var recipeId = Guid.NewGuid();

        // Act
        var response = await _fixture.Host.Scenario(_ =>
        {
            _.Get.Url(GetRecipeHistory.Endpoint + "?searchText=123");
            _.StatusCodeShouldBe(HttpStatusCode.OK);
        });

        // Assert
        var parsedResult = await response.ReadAsJsonAsync<RecipeHistoryQueryResponse>();

        parsedResult.History.Should().BeEmpty();
    }

    [Fact]
    public async Task Given_a_recipe_exists_with_history_then_history_events_should_be_returned()
    {
        // Arrange
        var existingRecipe = ObjectMother
            .Events
            .RecipeCreated
            .Default
            .Build();

        await _fixture.CreateRecipeAsync(existingRecipe);

        await using var session = await _fixture.Store.LightweightSerializableSessionAsync();

        var evt = new RecipeCooked(
            Guid.NewGuid(),
            existingRecipe.RecipeId,
            existingRecipe.Name,
            DateTime.UtcNow);

        session.Events.Append(existingRecipe.RecipeId, evt);

        await session.SaveChangesAsync();

        // Act
        var result = await _fixture.Host.Scenario(_ =>
        {
            _.Get.Url(GetRecipeHistory.Endpoint);
            _.StatusCodeShouldBe(HttpStatusCode.OK);
        });

        // Assert
        var parsedResult = await result.ReadAsJsonAsync<RecipeHistoryQueryResponse>();

        parsedResult.History.Should().HaveCount(1);
    }
}
