using PlanShare.App.Models.ValueObjects;

namespace PlanShare.App.UseCases.WorkItem.GetById;
public interface IGetWorkItemByIdUseCase
{
    Task<Result<Models.WorkItemDetail>> Execute(Guid id);
}
