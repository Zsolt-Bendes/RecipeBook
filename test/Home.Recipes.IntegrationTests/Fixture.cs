using Home.Recipes.Api.Infrastructure;
using Home.Recipes.Domain.Recipes;
using Home.Recipes.Domain.Recipes.Events;
using Marten;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

namespace Home.Recipes.IntegrationTests;

[CollectionDefinition(nameof(Fixture))]
public sealed class FixtureDefinition : ICollectionFixture<Fixture> { }

public sealed class Fixture : IAsyncLifetime
{
    public IAlbaHost Host { get; private set; } = null!;

    public IDocumentStore Store { get; private set; } = null!;

    private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder()
            .WithDatabase("recipes")
            .WithUsername("postgres")
            .Build();

    public async Task DisposeAsync()
    {
        if (Host is not null)
        {
            await Host.DisposeAsync();
        }

        await _postgreSqlContainer?.StopAsync()!;
    }

    public async Task InitializeAsync()
    {
        await _postgreSqlContainer.StartAsync();

        var postgresUrl = _postgreSqlContainer.GetConnectionString() + ";Database=recipes;";

        Host = await AlbaHost.For<Program>(x => x.ConfigureServices((ctx, collection) => collection
            .AddMarten(new MartenConfigurations(postgresUrl))
            .UseLightweightSessions()));

        Store = Host.Services.GetRequiredService<IDocumentStore>();
    }

    public async Task<Recipe?> LoadRecipeFromDbAsync(Guid recipeId)
    {
        await using var session = await Store.LightweightSerializableSessionAsync();

        return await session.LoadAsync<Recipe>(recipeId);
    }

    public async Task CreateRecipeAsync(RecipeCreated evt)
    {
        await using var session = await Store.LightweightSerializableSessionAsync();

        session.Events.StartStream(evt.RecipeId, evt);

        await session.SaveChangesAsync();
    }

    public string GetLongTestString(int length) => new string('a', length);
}
