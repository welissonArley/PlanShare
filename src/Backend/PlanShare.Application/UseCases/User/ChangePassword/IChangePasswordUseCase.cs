using PlanShare.Communication.Requests;

namespace PlanShare.Application.UseCases.User.ChangePassword;
public interface IChangePasswordUseCase
{
    Task Execute(RequestChangePasswordJson request);
}
