namespace PlanShare.Domain.Repositories.RefreshToken;
public interface IRefreshTokenReadOnlyRepository
{
    Task<Entities.RefreshToken?> Get(string token);
    Task<bool> HasRefreshTokenAssociated(Entities.User user, Guid accessTokenId);
}
