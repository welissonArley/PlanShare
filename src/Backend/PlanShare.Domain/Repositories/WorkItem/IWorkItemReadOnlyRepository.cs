namespace PlanShare.Domain.Repositories.WorkItem;
public interface IWorkItemReadOnlyRepository
{
    Task<Entities.WorkItem?> GetById(Entities.User user, Guid id);
    Task<List<Entities.WorkItem>> GetAll(Entities.User user);
}
