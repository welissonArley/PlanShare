using PlanShare.Domain.Dtos;
using PlanShare.Domain.Repositories;
using PlanShare.Domain.Repositories.Connection;

namespace PlanShare.Application.UseCases.User.Connection.ApproveCode;
public class ApproveCodeUserConnectionUseCase : IApproveCodeUserConnectionUseCase
{
    private readonly IUserConnectionWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public ApproveCodeUserConnectionUseCase(IUserConnectionWriteOnlyRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(UserConnectionsDto userConnections)
    {
        var connection = new Domain.Entities.UserConnection
        {
            UserId = userConnections.UserId,
            ConnectedUserId = userConnections.ConnectingUserId.Value
        };

        await _repository.Add(connection);

        await _unitOfWork.Commit();
    }
}
