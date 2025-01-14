namespace PlanShare.Domain.Repositories.User;
public interface IUserReadOnlyRepository
{
    Task<bool> ExistActiveUserWithEmail(string email);
    Task<Domain.Entities.User?> GetUserByEmail(string email);
}
