using PlanShare.Communication.Enums;
using PlanShare.Communication.Responses;
using PlanShare.Domain.Dtos;
using PlanShare.Domain.Repositories;
using PlanShare.Domain.Repositories.Connection;
using PlanShare.Domain.Repositories.User;
using PlanShare.Domain.Services.LoggedUser;
using PlanShare.Exceptions;

namespace PlanShare.Application.UseCases.User.Connection.ApproveCode;
public class ApproveCodeUserConnectionUseCase : IApproveCodeUserConnectionUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserConnectionReadOnlyRepository _userConnectionReadOnlyRepository;
    private readonly IUserConnectionWriteOnlyRepository _userConnectionWriteOnlyRepository;
    private readonly IUserReadOnlyRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ApproveCodeUserConnectionUseCase(
        ILoggedUser loggedUser,
        IUserConnectionReadOnlyRepository userConnectionReadOnlyRepository,
        IUserConnectionWriteOnlyRepository userConnectionWriteOnlyRepository,
        IUserReadOnlyRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _userConnectionReadOnlyRepository = userConnectionReadOnlyRepository;
        _userConnectionWriteOnlyRepository = userConnectionWriteOnlyRepository;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
        _userRepository = userRepository;
    }

    public async Task<HubOperationResult<string>> Execute(ConnectionByCode connectionByCode)
    {
        var codeOwner = await _loggedUser.Get();
        if (codeOwner.Id != connectionByCode.Generator.Id)
            return HubOperationResult<string>.Failure(ResourceMessagesException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE, UserConnectionErrorCode.NotAuthorized);

        var joinerUser = connectionByCode.Joiner is not null ? await _userRepository.GetById(connectionByCode.Joiner.Id) : null;
        if (joinerUser is null)
            return HubOperationResult<string>.Failure(ResourceMessagesException.NO_USER_CONNECTED_WITH_CODE, UserConnectionErrorCode.UserNotFound);

        if (joinerUser.Id == codeOwner.Id)
            return HubOperationResult<string>.Failure(ResourceMessagesException.SAME_USER_CANNOT_CONNECT_THEMSELVE, UserConnectionErrorCode.ConnectingToSelf);

        var existingConnection = await _userConnectionReadOnlyRepository.AreUsersConnected(joinerUser, codeOwner);
        if (existingConnection)
        {
            var message = string.Format(ResourceMessagesException.YOU_ARE_ALREADY_CONNECTED_WITH, joinerUser.Name);
            return HubOperationResult<string>.Failure(message, UserConnectionErrorCode.ConnectionAlreadyExists);
        }

        var connection = new Domain.Entities.UserConnection
        {
            UserId = connectionByCode.Generator.Id,
            ConnectedUserId = connectionByCode.Joiner!.Id,
        };

        await _userConnectionWriteOnlyRepository.Add(connection);

        await _unitOfWork.Commit();

        return HubOperationResult<string>.Success(string.Empty);
    }
}
