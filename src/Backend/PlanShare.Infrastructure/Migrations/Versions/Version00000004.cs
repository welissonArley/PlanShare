using FluentMigrator;

namespace PlanShare.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.USER_CONNECTION_TABLE, "Create table for user connections.")]
public class Version00000004 : VersionBase
{
    public override void Up()
    {
        CreateTable("UserConnections")
            .WithColumn("UserId").AsGuid().NotNullable().ForeignKey("FK_UserConnections_User_Id", "Users", "Id")
            .WithColumn("ConnectedUserId").AsGuid().NotNullable().ForeignKey("FK_UserConnections_Connected_User_Id", "Users", "Id");
    }
}
