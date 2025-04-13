using PlanShare.Communication.Requests;
using PlanShare.Communication.Responses;
using Refit;

namespace PlanShare.App.Data.Network.Api;

public interface IUserApiClient
{
    [Post("/users")]
    Task<ResponseRegisteredUserJson> Register([Body] RequestRegisterUserJson request);
}
