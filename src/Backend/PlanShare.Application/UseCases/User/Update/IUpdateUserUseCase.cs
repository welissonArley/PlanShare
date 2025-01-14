using PlanShare.Communication.Requests;

namespace PlanShare.Application.UseCases.User.Update;
public interface IUpdateUserUseCase
{
    Task Execute(RequestUpdateUserJson request);
}
