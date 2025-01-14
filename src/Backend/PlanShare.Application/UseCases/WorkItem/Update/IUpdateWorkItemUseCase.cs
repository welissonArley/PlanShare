using PlanShare.Communication.Requests;

namespace PlanShare.Application.UseCases.WorkItem.Update;
public interface IUpdateWorkItemUseCase
{
    Task Execute(Guid workItemId, RequestUpdateWorkItemJson request);
}
