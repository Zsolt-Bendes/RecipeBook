using Marten;
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

        Projections.Snapshot<Domain.Recipes.Recipe>(SnapshotLifecycle.Inline);
    }
}
