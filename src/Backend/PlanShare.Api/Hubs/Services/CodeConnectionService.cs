using PlanShare.Domain.Dtos;

namespace PlanShare.Api.Hubs.Services;

public class CodeConnectionService
{
    private readonly Dictionary<string, UserConnectionsDto> _connections;

    public CodeConnectionService()
    {
        _connections = [];
    }

    public void Start(CodeUserConnectionDto codeUser, string connectionId)
    {
        var userConnection = new UserConnectionsDto
        {
            UserId = codeUser.UserId,
            UserConnectionId = connectionId
        };

        _connections.Add(codeUser.Code, userConnection);
    }
}
