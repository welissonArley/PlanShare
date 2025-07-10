using PlanShare.App.Models.ValueObjects;

namespace PlanShare.App.UseCases.User.Update;
public interface IUpdateUserUseCase
{
    Task<Result> Execute(Models.User model);
}
