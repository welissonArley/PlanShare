using PlanShare.App.Models.ValueObjects;

namespace PlanShare.App.UseCases.Dashboard;
public interface IGetDashboardUseCase
{
    Task<Result<Models.Dashboard>> Execute();
}
