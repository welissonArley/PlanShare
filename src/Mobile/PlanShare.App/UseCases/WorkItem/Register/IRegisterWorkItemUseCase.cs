using PlanShare.App.Models.ValueObjects;

namespace PlanShare.App.UseCases.WorkItem.Register;
public interface IRegisterWorkItemUseCase
{
    Task<Result> Execute(Models.WorkItem workItem);
}
