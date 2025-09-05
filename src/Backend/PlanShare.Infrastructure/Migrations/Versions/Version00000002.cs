using FluentMigrator;

namespace PlanShare.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.TABLE_REFRESH_TOKEN, "Create user refresh tokens table")]
public class Version00000002 : VersionBase
{
    public override void Up()
    {
        CreateTable("RefreshTokens")
            .WithColumn("Token").AsString(1000).NotNullable()
            .WithColumn("AccessTokenId").AsGuid().NotNullable()
            .WithColumn("UserId").AsGuid().NotNullable().ForeignKey("FK_RefreshTokens_User_Id", "Users", "Id");
    }
}
