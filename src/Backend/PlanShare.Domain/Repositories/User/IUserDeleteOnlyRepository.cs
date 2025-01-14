namespace PlanShare.Domain.Repositories.User;
public interface IUserDeleteOnlyRepository
{
    void DeleteAccount(Guid userIdentifier);
}
