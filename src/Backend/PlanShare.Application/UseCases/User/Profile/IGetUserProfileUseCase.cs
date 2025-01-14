using PlanShare.Communication.Responses;

namespace PlanShare.Application.UseCases.User.Profile;
public interface IGetUserProfileUseCase
{
    Task<ResponseUserProfileJson> Execute();
}
