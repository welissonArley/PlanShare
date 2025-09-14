using PlanShare.App.Models.ValueObjects;

namespace PlanShare.App.UseCases.Authentication.Refresh;
public interface IUseRefreshTokenUseCase
{
    Task<Result<Tokens>> Execute();
}
