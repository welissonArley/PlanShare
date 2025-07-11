using PlanShare.App.Data.Network.Api;
using PlanShare.App.Data.Storage.Preferences.User;
using PlanShare.App.Extensions;
using PlanShare.App.Models.ValueObjects;
using PlanShare.Communication.Requests;

namespace PlanShare.App.UseCases.User.Update;
public class UpdateUserUseCase : IUpdateUserUseCase
{
    private readonly IUserApi _userApi;
    private readonly IUserStorage _userStorage;

    public UpdateUserUseCase(IUserApi userApi, IUserStorage userStorage)
    {
        _userApi = userApi;
        _userStorage = userStorage;
    }

    public async Task<Result> Execute(Models.User model)
    {
        var request = new RequestUpdateUserJson
        {
            Name = model.Name,
            Email = model.Email
        };

        var response = await _userApi.UpdateProfile(request);

        if (response.IsSuccessful)
        {
            var user = _userStorage.Get() with { Name = model.Name };

            _userStorage.Save(user);

            return Result.Success();
        }

        var errorResponse = await response.Error.GetResponseError();

        return Result.Failure(errorResponse.Errors);
    }
}
