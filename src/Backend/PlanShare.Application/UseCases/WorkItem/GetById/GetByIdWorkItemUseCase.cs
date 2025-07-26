using Mapster;
using PlanShare.Communication.Responses;
using PlanShare.Domain.Repositories.WorkItem;
using PlanShare.Domain.Services.LoggedUser;
using PlanShare.Exceptions;
using PlanShare.Exceptions.ExceptionsBase;

namespace PlanShare.Application.UseCases.WorkItem.GetById;
public class GetByIdWorkItemUseCase : IGetByIdWorkItemUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IWorkItemReadOnlyRepository _repository;

    public GetByIdWorkItemUseCase(
        ILoggedUser loggedUser,
        IWorkItemReadOnlyRepository repository)
    {
        _repository = repository;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseWorkItemJson> Execute(Guid workItemId)
    {
        var loggedUser = await _loggedUser.Get();

        var workItem = await _repository.GetById(loggedUser, workItemId);
        if (workItem is null)
            throw new NotFoundException(ResourceMessagesException.WORK_ITEM_NOT_FOUND);

        return workItem.Adapt<ResponseWorkItemJson>();
    }
}