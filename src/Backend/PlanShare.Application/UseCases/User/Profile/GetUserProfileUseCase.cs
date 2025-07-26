using Mapster;
using PlanShare.Communication.Responses;
using PlanShare.Domain.Services.LoggedUser;

namespace PlanShare.Application.UseCases.User.Profile;
public class GetUserProfileUseCase : IGetUserProfileUseCase
{
    private readonly ILoggedUser _loggedUser;

    public GetUserProfileUseCase(ILoggedUser loggedUser)
    {
        _loggedUser = loggedUser;
    }

    public async Task<ResponseUserProfileJson> Execute()
    {
        var user = await _loggedUser.Get();

        return user.Adapt<ResponseUserProfileJson>();
    }
}