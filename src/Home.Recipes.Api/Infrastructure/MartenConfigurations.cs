using Marten.Events.Projections;
using System.Text.Json;

namespace Home.Recipes.Api.Infrastructure;

internal sealed class MartenConfigurations : StoreOptions
{
    public MartenConfigurations(string connectionString)
    {
        Connection(connectionString);
        UseSystemTextJsonForSerialization(new JsonSerializerOptions
        {
            WriteIndented = true,
            AllowOutOfOrderMetadataProperties = true,
        });

        Schema.For<Domain.Recipes.Recipe>();
        Schema.For<Domain.RecipeHistory.RecipeHistory>();

        Projections.Snapshot<Domain.Recipes.Recipe>(SnapshotLifecycle.Inline);
        Projections.Snapshot<Domain.RecipeHistory.RecipeHistory>(SnapshotLifecycle.Inline);
    }
}
