namespace PlanShare.Domain.Entities;
public class Assignee : EntityBase
{
    public Guid WorkItemId { get; set; }
    public Guid UserId { get; set; }
}
