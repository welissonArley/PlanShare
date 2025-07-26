using Mapster;
using PlanShare.Communication.Responses;
using PlanShare.Domain.Repositories.WorkItem;
using PlanShare.Domain.Services.LoggedUser;

namespace PlanShare.Application.UseCases.WorkItem.GetAll;
public class GetAllWorkItemUseCase : IGetAllWorkItemUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IWorkItemReadOnlyRepository _repository;

    public GetAllWorkItemUseCase(
        ILoggedUser loggedUser,
        IWorkItemReadOnlyRepository repository)
    {
        _repository = repository;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseWorkItemsJson> Execute()
    {
        var loggedUser = await _loggedUser.Get();

        var workItem = await _repository.GetAll(loggedUser);

        return new ResponseWorkItemsJson
        {
            WorkItems = workItem.Adapt<List<ResponseShortWorkItemJson>>()
        };
    }
}
