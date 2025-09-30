using PlanShare.Domain.Dtos;
using PlanShare.Domain.Services.LoggedUser;

namespace PlanShare.Application.UseCases.User.Connection.JoinWithCode;
public class JoinWithCodeUseCase : IJoinWithCodeUseCase
{
    private readonly ILoggedUser _loggedUser;

    public JoinWithCodeUseCase(ILoggedUser loggedUser)
    {
        _loggedUser = loggedUser;
    }

    public async Task<ConnectingUserDto> Execute(Guid generatedById)
    {
        var loggedUser = await _loggedUser.Get();

        return new ConnectingUserDto(loggedUser.Id, loggedUser.Name, string.Empty);
    }
}