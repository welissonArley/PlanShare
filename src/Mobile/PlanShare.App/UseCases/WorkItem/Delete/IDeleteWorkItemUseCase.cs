using PlanShare.App.Models.ValueObjects;

namespace PlanShare.App.UseCases.WorkItem.Delete;
public interface IDeleteWorkItemUseCase
{
    Task<Result> Execute(Guid id);
}
