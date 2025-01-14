using AutoMapper;
using PlanShare.Communication.Responses;
using PlanShare.Domain.Repositories.WorkItem;
using PlanShare.Domain.Services.LoggedUser;
using PlanShare.Exceptions;
using PlanShare.Exceptions.ExceptionsBase;

namespace PlanShare.Application.UseCases.WorkItem.GetById;
public class GetByIdWorkItemUseCase : IGetByIdWorkItemUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;
    private readonly IWorkItemReadOnlyRepository _repository;

    public GetByIdWorkItemUseCase(
        ILoggedUser loggedUser,
        IMapper mapper,
        IWorkItemReadOnlyRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseWorkItemJson> Execute(Guid workItemId)
    {
        var loggedUser = await _loggedUser.Get();

        var workItem = await _repository.GetById(loggedUser, workItemId);
        if (workItem is null)
            throw new NotFoundException(ResourceMessagesException.WORK_ITEM_NOT_FOUND);

        return _mapper.Map<ResponseWorkItemJson>(workItem);
    }
}