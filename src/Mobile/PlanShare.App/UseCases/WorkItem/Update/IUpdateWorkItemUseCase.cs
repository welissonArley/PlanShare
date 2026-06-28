using PlanShare.App.Models.ValueObjects;

namespace PlanShare.App.UseCases.WorkItem.Update;
public interface IUpdateWorkItemUseCase
{
    Task<Result> Execute(Guid id, Models.WorkItem workItem);
}
