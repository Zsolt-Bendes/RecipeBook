using Marten;
using Marten.Events.Projections;

namespace Home.Recipes.Api.Infrastructure;

internal sealed class MartenConfigurations : StoreOptions
{
    public MartenConfigurations(string connectionString)
    {
        Connection(connectionString);
        UseSystemTextJsonForSerialization();

        Schema.For<Domain.Recipes.Recipe>();

        Projections.Snapshot<Domain.Recipes.Recipe>(SnapshotLifecycle.Inline);
    }
}
