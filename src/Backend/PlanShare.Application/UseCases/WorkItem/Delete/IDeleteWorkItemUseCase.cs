namespace PlanShare.Application.UseCases.WorkItem.Delete;
public interface IDeleteWorkItemUseCase
{
    Task Execute(Guid workItemId);
}
