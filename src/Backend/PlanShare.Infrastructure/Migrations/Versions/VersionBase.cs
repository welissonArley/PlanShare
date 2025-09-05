using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace PlanShare.Infrastructure.Migrations.Versions;
public abstract class VersionBase : ForwardOnlyMigration
{
    protected ICreateTableColumnOptionOrWithColumnSyntax CreateTable(string tableName)
    {
        return Create.Table(tableName)
            .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
            .WithColumn("Active").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("CreatedAt").AsDateTime().NotNullable();
    }
}
