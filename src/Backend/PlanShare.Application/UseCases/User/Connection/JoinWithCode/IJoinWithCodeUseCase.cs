using PlanShare.Domain.Dtos;

namespace PlanShare.Application.UseCases.User.Connection.JoinWithCode;
public interface IJoinWithCodeUseCase
{
    Task<ConnectingUserDto> Execute(Guid generatedById);
}
