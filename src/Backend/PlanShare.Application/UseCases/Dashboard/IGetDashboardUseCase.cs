using PlanShare.Communication.Responses;

namespace PlanShare.Application.UseCases.Dashboard;
public interface IGetDashboardUseCase
{
    Task<ResponseDashboardJson> Execute();
}
