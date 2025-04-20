using PlanShare.App.Models;

namespace PlanShare.App.UseCases.User.Register;
public interface IRegisterUserUseCase
{
    Task Execute(UserRegisterAccount model);
}
