using Mapster;
using PlanShare.Communication.Responses;
using PlanShare.Domain.Repositories.Connection;
using PlanShare.Domain.Repositories.WorkItem;
using PlanShare.Domain.Services.LoggedUser;

namespace PlanShare.Application.UseCases.Dashboard;
public class GetDashboardUseCase : IGetDashboardUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IWorkItemReadOnlyRepository _workItemRepository;
    private readonly IUserConnectionReadOnlyRepository _personAssociationRepository;

    public GetDashboardUseCase(ILoggedUser loggedUser, IWorkItemReadOnlyRepository workItemRepository, IUserConnectionReadOnlyRepository personAssociationRepository)
    {
        _loggedUser = loggedUser;
        _workItemRepository = workItemRepository;
        _personAssociationRepository = personAssociationRepository;
    }

    public async Task<ResponseDashboardJson> Execute()
    {
        var loggedUser = await _loggedUser.Get();

        var workItems = await _workItemRepository.GetAll(loggedUser);
        var associations = await _personAssociationRepository.GetConnectionsForUser(loggedUser);

        return new ResponseDashboardJson
        {
            WorkItems = workItems.Adapt<List<ResponseShortWorkItemJson>>(),
            Friends = associations.Adapt<List<ResponseAssigneeJson>>()
        };
    }
}
