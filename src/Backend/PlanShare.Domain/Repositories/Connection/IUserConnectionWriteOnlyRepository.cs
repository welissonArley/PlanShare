using PlanShare.Domain.Entities;

namespace PlanShare.Domain.Repositories.Connection;
public interface IUserConnectionWriteOnlyRepository
{
    Task Add(UserConnection userConnection);
}
