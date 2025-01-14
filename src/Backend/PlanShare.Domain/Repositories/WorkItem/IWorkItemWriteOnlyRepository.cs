namespace PlanShare.Domain.Repositories.WorkItem;
public interface IWorkItemWriteOnlyRepository
{
    Task Add(Entities.WorkItem workItem);
    Task Delete(Guid id);
}
