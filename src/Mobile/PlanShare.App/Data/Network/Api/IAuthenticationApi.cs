using PlanShare.Communication.Requests;
using PlanShare.Communication.Responses;
using Refit;

namespace PlanShare.App.Data.Network.Api;
public interface IAuthenticationApi
{
    [Post("/authentication/refresh")]
    Task<IApiResponse<ResponseTokensJson>> Refresh([Body] RequestNewTokenJson request);
}
