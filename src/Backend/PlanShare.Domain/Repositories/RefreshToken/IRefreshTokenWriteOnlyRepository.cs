namespace PlanShare.Domain.Repositories.RefreshToken;
public interface IRefreshTokenWriteOnlyRepository
{
    Task Add(Entities.RefreshToken refreshToken);
}
