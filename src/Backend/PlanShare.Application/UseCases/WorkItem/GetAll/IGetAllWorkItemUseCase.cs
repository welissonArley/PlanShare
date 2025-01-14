using PlanShare.Communication.Responses;

namespace PlanShare.Application.UseCases.WorkItem.GetAll;
public interface IGetAllWorkItemUseCase
{
    Task<ResponseWorkItemsJson> Execute();
}
