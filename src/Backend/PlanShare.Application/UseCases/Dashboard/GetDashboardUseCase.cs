using Mapster;
using PlanShare.Communication.Responses;
using PlanShare.Domain.Repositories.Connection;
using PlanShare.Domain.Services.LoggedUser;

namespace PlanShare.Application.UseCases.Dashboard;
public class GetDashboardUseCase : IGetDashboardUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserConnectionReadOnlyRepository _userConnectionRepository;

    public GetDashboardUseCase(ILoggedUser loggedUser, IUserConnectionReadOnlyRepository userConnectionRepository)
    {
        _loggedUser = loggedUser;
        _userConnectionRepository = userConnectionRepository;
    }

    public async Task<ResponseDashboardJson> Execute()
    {
        var loggedUser = await _loggedUser.Get();

        var associations = await _userConnectionRepository.GetConnectionsForUser(loggedUser);

        return new ResponseDashboardJson
        {
            UserName = loggedUser.Name,
            ConnectedUsers = associations.Adapt<List<ResponseAssigneeJson>>()
        };
    }
}
