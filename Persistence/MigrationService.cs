using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence;

public static class MigrationService
{
    public static IServiceCollection AddMigrationService(this IServiceCollection services, string connectionString)
    {
        services.AddFluentMigratorCore()
            .ConfigureRunner(rb =>
                rb.AddSqlServer()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations()
            );
        return services;
    }

    public static void MigrationServiceUp(this IServiceProvider provider)
    {
        var runner = provider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }

    public static void MigrationServiceDown(this IServiceProvider provider, long version)
    {
        var runner = provider.GetRequiredService<IMigrationRunner>();
        runner.MigrateDown(version);
    }
}