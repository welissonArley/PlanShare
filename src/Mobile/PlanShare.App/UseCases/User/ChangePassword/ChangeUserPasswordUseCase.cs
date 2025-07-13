using PlanShare.App.Data.Network.Api;
using PlanShare.App.Extensions;
using PlanShare.App.Models.ValueObjects;
using PlanShare.Communication.Requests;

namespace PlanShare.App.UseCases.User.ChangePassword;
public class ChangeUserPasswordUseCase : IChangeUserPasswordUseCase
{
    private readonly IUserApi _userApi;

    public ChangeUserPasswordUseCase(IUserApi userApi)
    {
        _userApi = userApi;
    }

    public async Task<Result> Execute(Models.ChangePassword model)
    {
        var request = new RequestChangePasswordJson
        {
            Password = model.CurrentPassword,
            NewPassword = model.NewPassword,
        };

        var response = await _userApi.ChangePassword(request);

        if (response.IsSuccessful)
            return Result.Success();

        var errorResponse = await response.Error.GetResponseError();

        return Result.Failure(errorResponse.Errors);
    }
}
