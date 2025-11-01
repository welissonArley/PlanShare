using PlanShare.App.Data.Network.Api;
using PlanShare.App.Data.Storage.Preferences.User;
using PlanShare.App.Extensions;
using PlanShare.App.Models.ValueObjects;
using System.Collections.ObjectModel;

namespace PlanShare.App.UseCases.Dashboard;
public class GetDashboardUseCase : IGetDashboardUseCase
{
    private readonly IDashboardApi _dashboardApi;
    private readonly IUserStorage _userStorage;

    public GetDashboardUseCase(IDashboardApi dashboardApi, IUserStorage userStorage)
    {
        _dashboardApi = dashboardApi;
        _userStorage = userStorage;
    }

    public async Task<Result<Models.Dashboard>> Execute()
    {
        var response = await _dashboardApi.GetDashboard();

        if (response.IsSuccessful)
        {
            var model = new Models.Dashboard
            {
                UserName = response.Content.UserName,
                ConnectedUsers = new ObservableCollection<Models.ConnectedUser>(response.Content.ConnectedUsers.Select(user => new Models.ConnectedUser
                {
                    Id = user.Id,
                    Name = user.Name,
                    ProfilePhotoUrl = user.ProfilePhotoUrl,
                }))
            };

            var user = _userStorage.Get();

            user = user with { Name = response.Content.UserName };

            _userStorage.Save(user);

            return Result<Models.Dashboard>.Success(model);
        }

        var errorResponse = await response.Error.GetResponseError();

        return Result<Models.Dashboard>.Failure(errorResponse.Errors);
    }
}
