using PlanShare.Communication.Requests;
using PlanShare.Communication.Responses;
using Refit;

namespace PlanShare.App.Data.Network.Api;

public interface ILoginApi
{
    [Post("/login")]
    Task<IApiResponse<ResponseRegisteredUserJson>> Login([Body] RequestLoginJson request);
}
