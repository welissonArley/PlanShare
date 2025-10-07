using PlanShare.Communication.Responses;
using PlanShare.Domain.Dtos;

namespace PlanShare.Application.UseCases.User.Connection.JoinWithCode;
public interface IJoinWithCodeUseCase
{
    Task<HubOperationResult<ConnectionUsers>> Execute(Guid generatedById);
}
