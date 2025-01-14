using PlanShare.Communication.Requests;
using PlanShare.Communication.Responses;

namespace PlanShare.Application.UseCases.Login.DoLogin;
public interface IDoLoginUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request);
}
