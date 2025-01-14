namespace PlanShare.Domain.Entities;
public class WorkItem : EntityBase
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
    public List<Assignee> Assignees { get; set; } = [];
    public Guid? AttachmentFolder {  get; set; }
}
