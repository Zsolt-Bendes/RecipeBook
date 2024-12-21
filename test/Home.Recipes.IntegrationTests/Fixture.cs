using Alba;
using Home.Recipes.Api.Infrastructure;
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
            .WithDatabase("recipies")
            .WithUsername("postgres")
            //.WithPassword(TestConstants.MainPassword)
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

        var postgresUrl = _postgreSqlContainer.GetConnectionString() + ";Database=recipies;";

        Host = await AlbaHost.For<Program>(x => x.ConfigureServices((ctx, collection) => collection.AddMarten(new MartenConfigurations(postgresUrl))));

        Store = Host.Services.GetRequiredService<IDocumentStore>();
    }
}
