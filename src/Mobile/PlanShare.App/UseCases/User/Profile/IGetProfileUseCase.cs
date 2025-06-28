using PlanShare.App.Models.ValueObjects;

namespace PlanShare.App.UseCases.User.Profile;
public interface IGetProfileUseCase
{
    Task<Result<Models.User>> Execute();
}
