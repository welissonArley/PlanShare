using PlanShare.App.Models.ValueObjects;

namespace PlanShare.App.UseCases.User.ChangePassword;
public interface IChangeUserPasswordUseCase
{
    Task<Result> Execute(Models.ChangePassword model);
}
