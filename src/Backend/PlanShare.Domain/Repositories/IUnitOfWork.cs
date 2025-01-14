namespace PlanShare.Domain.Repositories;
public interface IUnitOfWork
{
    Task Commit();
}
