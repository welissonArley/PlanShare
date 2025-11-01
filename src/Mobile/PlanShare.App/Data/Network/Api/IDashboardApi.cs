using PlanShare.Communication.Responses;
using Refit;

namespace PlanShare.App.Data.Network.Api;
public interface IDashboardApi
{
    [Get("/dashboard")]
    Task<IApiResponse<ResponseDashboardJson>> GetDashboard();
}
