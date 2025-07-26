using Mapster;
using PlanShare.Communication.Requests;
using PlanShare.Domain.Extensions;
using PlanShare.Domain.Repositories;
using PlanShare.Domain.Repositories.WorkItem;
using PlanShare.Domain.Services.LoggedUser;
using PlanShare.Exceptions;
using PlanShare.Exceptions.ExceptionsBase;

namespace PlanShare.Application.UseCases.WorkItem.Update;
public class UpdateWorkItemUseCase : IUpdateWorkItemUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWorkItemUpdateOnlyRepository _repository;

    public UpdateWorkItemUseCase(
        ILoggedUser loggedUser,
        IUnitOfWork unitOfWork,
        IWorkItemUpdateOnlyRepository repository)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _loggedUser = loggedUser;
    }

    public async Task Execute(Guid workItemId, RequestUpdateWorkItemJson request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.Get();

        var workItem = await _repository.GetById(loggedUser, workItemId);
        if (workItem is null)
            throw new NotFoundException(ResourceMessagesException.WORK_ITEM_NOT_FOUND);

        request.Adapt(workItem);

        _repository.Update(workItem);

        await _unitOfWork.Commit();
    }

    private static void Validate(RequestUpdateWorkItemJson request)
    {
        var result = new UpdateWorkItemValidator().Validate(request);

        if (result.IsValid.IsFalse())
            throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).ToList());
    }
}