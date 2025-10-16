using PlanShare.Communication.Enums;
using PlanShare.Communication.Responses;
using PlanShare.Domain.Dtos;
using PlanShare.Domain.Services.LoggedUser;
using PlanShare.Exceptions;

namespace PlanShare.Application.UseCases.User.Connection.CancelCode;
public class CancelCodeUserConnectionUseCase : ICancelCodeUserConnectionUseCase
{
    private readonly ILoggedUser _loggedUser;

    public CancelCodeUserConnectionUseCase(ILoggedUser loggedUser)
    {
        _loggedUser = loggedUser;
    }

    public async Task<HubOperationResult<string>> Execute(ConnectionByCode connectionByCode)
    {
        var loggedUser = await _loggedUser.Get();
        if (loggedUser.Id != connectionByCode.Generator.Id)
            return HubOperationResult<string>.Failure(ResourceMessagesException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE, UserConnectionErrorCode.NotAuthorized);

        return HubOperationResult<string>.Success(string.Empty);
    }
}
