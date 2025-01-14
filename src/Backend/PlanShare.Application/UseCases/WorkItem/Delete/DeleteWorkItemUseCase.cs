using PlanShare.Domain.Repositories;
using PlanShare.Domain.Repositories.WorkItem;
using PlanShare.Domain.Services.LoggedUser;
using PlanShare.Exceptions;
using PlanShare.Exceptions.ExceptionsBase;

namespace PlanShare.Application.UseCases.WorkItem.Delete;
public class DeleteWorkItemUseCase : IDeleteWorkItemUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IWorkItemReadOnlyRepository _repositoryRead;
    private readonly IWorkItemWriteOnlyRepository _repositoryWrite;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteWorkItemUseCase(
        ILoggedUser loggedUser,
        IWorkItemReadOnlyRepository repositoryRead,
        IWorkItemWriteOnlyRepository repositoryWrite,
        IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _repositoryRead = repositoryRead;
        _repositoryWrite = repositoryWrite;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid workItemId)
    {
        var loggedUser = await _loggedUser.Get();

        var workItem = await _repositoryRead.GetById(loggedUser, workItemId);

        if(workItem is null)
            throw new NotFoundException(ResourceMessagesException.WORK_ITEM_NOT_FOUND);

        await _repositoryWrite.Delete(workItemId);

        await _unitOfWork.Commit();
    }
}
