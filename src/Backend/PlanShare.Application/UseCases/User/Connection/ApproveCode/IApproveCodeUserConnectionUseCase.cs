using PlanShare.Communication.Responses;
using PlanShare.Domain.Dtos;

namespace PlanShare.Application.UseCases.User.Connection.ApproveCode;
public interface IApproveCodeUserConnectionUseCase
{
    Task<HubOperationResult<string>> Execute(ConnectionByCode connectionByCode);
}
