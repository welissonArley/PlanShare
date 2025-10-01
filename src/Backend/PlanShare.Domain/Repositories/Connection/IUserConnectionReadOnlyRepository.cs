namespace PlanShare.Domain.Repositories.Connection;
public interface IUserConnectionReadOnlyRepository
{
    Task<List<Entities.User>> GetConnectionsForUser(Entities.User user);
}
