namespace PlanShare.Domain.Entities;
public abstract class EntityBase
{
    public Guid Id { get; set; }
    public bool Active { get; set; } = true;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
