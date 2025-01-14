namespace PlanShare.Domain.Repositories.WorkItem;
public interface IWorkItemUpdateOnlyRepository
{
    Task<Entities.WorkItem?> GetById(Entities.User user, Guid id);
    void Update(Entities.WorkItem workItem);
}
