using PlanShare.Communication.Enums;
using PlanShare.Communication.Responses;
using PlanShare.Domain.Dtos;
using PlanShare.Domain.Repositories.Connection;
using PlanShare.Domain.Repositories.User;
using PlanShare.Domain.Services.LoggedUser;
using PlanShare.Exceptions;

namespace PlanShare.Application.UseCases.User.Connection.JoinWithCode;
public class JoinWithCodeUseCase : IJoinWithCodeUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserReadOnlyRepository _userRepository;
    private readonly IUserConnectionReadOnlyRepository _userConnectionRepository;

    public JoinWithCodeUseCase(
        ILoggedUser loggedUser,
        IUserConnectionReadOnlyRepository userConnectionRepository,
        IUserReadOnlyRepository userRepository)
    {
        _loggedUser = loggedUser;
        _userConnectionRepository = userConnectionRepository;
        _userRepository = userRepository;
    }

    public async Task<HubOperationResult<ConnectionUsers>> Execute(Guid generatedById)
    {
        var joinerUser = await _loggedUser.Get();
        if(joinerUser.Id == generatedById)
        {
            return HubOperationResult<ConnectionUsers>.Failure(ResourceMessagesException.SAME_USER_CANNOT_CONNECT_THEMSELVE, UserConnectionErrorCode.ConnectingToSelf);
        }

        var codeOwner = await _userRepository.GetById(generatedById);
        if(codeOwner is null)
        {
            return HubOperationResult<ConnectionUsers>.Failure(ResourceMessagesException.USER_NOT_FOUND, UserConnectionErrorCode.UserNotFound);
        }

        var existingConnection = await _userConnectionRepository.AreUsersConnected(joinerUser, codeOwner);
        if(existingConnection)
        {
            var message = string.Format(ResourceMessagesException.YOU_ARE_ALREADY_CONNECTED_WITH, codeOwner.Name);
            return HubOperationResult<ConnectionUsers>.Failure(message, UserConnectionErrorCode.ConnectionAlreadyExists);
        }

        var generator = new UserDto(codeOwner.Id, codeOwner.Name, string.Empty);
        var connector = new UserDto(joinerUser.Id, joinerUser.Name, string.Empty);

        return HubOperationResult<ConnectionUsers>.Success(new ConnectionUsers(generator, connector));
    }
}