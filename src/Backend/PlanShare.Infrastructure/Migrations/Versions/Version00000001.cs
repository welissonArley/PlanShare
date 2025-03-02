using FluentMigrator;

namespace PlanShare.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_REGISTER_USER, "Create table to save the user's information")]
public class Version00000001 : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table("Users")
            .WithColumn("Id").AsGuid().PrimaryKey().NotNullable()
            .WithColumn("Active").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("CreatedOn").AsDateTime().NotNullable()
            .WithColumn("Name").AsString(255).NotNullable()
            .WithColumn("Email").AsString(255).NotNullable()
            .WithColumn("Password").AsString(2000).NotNullable();
    }
}
