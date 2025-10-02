using PlanShare.Domain.Dtos;

namespace PlanShare.Application.UseCases.User.Connection.ApproveCode;
public interface IApproveCodeUserConnectionUseCase
{
    Task Execute(UserConnectionsDto userConnections);
}
