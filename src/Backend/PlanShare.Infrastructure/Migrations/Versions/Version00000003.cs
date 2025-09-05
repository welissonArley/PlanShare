using FluentMigrator;

namespace PlanShare.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.FIX_CREATEDON_USER_TABLE, "Rename the CreatedOn column in the Users table to CreatedAt")]
public class Version00000003 : ForwardOnlyMigration
{
    public override void Up()
    {
        Rename.Column("CreatedOn").OnTable("Users").To("CreatedAt");
    }
}
