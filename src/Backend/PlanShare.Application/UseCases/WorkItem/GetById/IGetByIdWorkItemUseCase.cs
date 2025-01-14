using PlanShare.Communication.Responses;

namespace PlanShare.Application.UseCases.WorkItem.GetById;
public interface IGetByIdWorkItemUseCase
{
    Task<ResponseWorkItemJson> Execute(Guid workItemId);
}
