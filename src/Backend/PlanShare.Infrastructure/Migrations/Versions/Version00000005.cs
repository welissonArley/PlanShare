using FluentMigrator;
using System.Data;

namespace PlanShare.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.WORK_ITEM_TABLE, "Create tables for work items and their assignees.")]
public class Version00000005 : VersionBase
{
    public override void Up()
    {
        CreateTable("WorkItems")
            .WithColumn("Title").AsString(255).NotNullable()
            .WithColumn("Description").AsString(int.MaxValue).NotNullable()
            .WithColumn("DueDate").AsDateTime().NotNullable()
            .WithColumn("AttachmentFolder").AsGuid().Nullable();

        CreateTable("Assignees")
            .WithColumn("WorkItemId").AsGuid().NotNullable()
            .WithColumn("UserId").AsGuid().NotNullable().ForeignKey("FK_Assignees_User_Id", "Users", "Id");

        Create.ForeignKey("FK_Assignees_WorkItem_Id")
            .FromTable("Assignees").ForeignColumn("WorkItemId")
            .ToTable("WorkItems").PrimaryColumn("Id")
            .OnDelete(Rule.Cascade);
    }
}
