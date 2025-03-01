using Microsoft.Extensions.Configuration;
using PlanShare.Domain.Enums;

namespace PlanShare.Infrastructure.Extensions;
public static class ConfigurationExtensions
{
    public static string ConnectionString(this IConfiguration configuration)
    {
        var databaseType = configuration.GetDatabaseType();

        if(databaseType == DatabaseType.MySQL)
        {
            return configuration.GetConnectionString("ConnectionMySQL")!;
        }

        return configuration.GetConnectionString("ConnectionSQLServer")!;
    }

    public static DatabaseType GetDatabaseType(this IConfiguration configuration)
    {
        var databaseType = configuration.GetConnectionString("DatabaseType")!;

        return Enum.Parse<DatabaseType>(databaseType);
    }

    public static bool IsUnitTestEnviroment(this IConfiguration configuration)
    {
        _ = bool.TryParse(configuration.GetSection("InMemoryTests").Value, out bool inMemoryTests);

        return inMemoryTests;
    }
}