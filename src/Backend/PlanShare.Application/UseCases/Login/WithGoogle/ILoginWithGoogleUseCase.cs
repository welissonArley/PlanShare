using PlanShare.Communication.Responses;
using PlanShare.Domain.Dtos;

namespace PlanShare.Application.UseCases.Login.WithGoogle;

public interface ILoginWithGoogleUseCase
{
    Task<ResponseRegisteredUserJson> Execute(LoginThirdPartNameEmailDto info);
}
