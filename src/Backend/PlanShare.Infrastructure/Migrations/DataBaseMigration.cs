using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace PlanShare.Infrastructure.Migrations;

public static class DataBaseMigration
{
    public static void MigrateDatabase(IServiceProvider serviceProvider)
    {
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

        runner.ListMigrations();

        runner.MigrateUp();
    }
}
