using PlanShare.Communication.Requests;
using PlanShare.Communication.Responses;

namespace PlanShare.App.Data.Network.Api;

public interface IUserApiClient
{
    Task<ResponseRegisteredUserJson> Register(RequestRegisterUserJson request);
}
