using PlanShare.Communication.Requests;
using PlanShare.Communication.Responses;

namespace PlanShare.Application.UseCases.Token.RefreshToken;
public interface IUseRefreshTokenUseCase
{
    Task<ResponseTokensJson> Execute(RequestNewTokenJson request);
}
