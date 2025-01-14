using PlanShare.Communication.Requests;
using PlanShare.Communication.Responses;

namespace PlanShare.Application.UseCases.User.Register;
public interface IRegisterUserUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
}
