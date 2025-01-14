namespace PlanShare.Domain.Repositories.User;
public interface IUserUpdateOnlyRepository
{
    Task<Entities.User> GetById(Guid id);
    void Update(Entities.User user);
}
