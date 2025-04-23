using PlanShare.App.Models;
using PlanShare.App.Models.ValueObjects;

namespace PlanShare.App.UseCases.User.Register;
public interface IRegisterUserUseCase
{
    Task<Result> Execute(UserRegisterAccount model);
}
