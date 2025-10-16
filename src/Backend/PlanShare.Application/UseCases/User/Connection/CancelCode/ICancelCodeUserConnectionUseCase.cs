using PlanShare.Communication.Responses;
using PlanShare.Domain.Dtos;

namespace PlanShare.Application.UseCases.User.Connection.CancelCode;
public interface ICancelCodeUserConnectionUseCase
{
    Task<HubOperationResult<string>> Execute(ConnectionByCode connectionByCode);
}
