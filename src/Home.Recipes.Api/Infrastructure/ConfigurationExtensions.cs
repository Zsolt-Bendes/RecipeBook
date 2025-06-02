using Home.Recipes.Api.Infrastructure.Exceptions;

namespace Home.Recipes.Api.Infrastructure;

internal static class ConfigurationExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMarten(configuration);
        services.AddWolverineHttp();
        services.AddSingleton(TimeProvider.System);
    }

    private static IServiceCollection AddMarten(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new PostgresConnectionStringInsNullOrEmptyException();
        }

        services.AddMarten(new MartenConfigurations(connectionString))
            .IntegrateWithWolverine()
            .UseLightweightSessions();

        return services;
    }
}
