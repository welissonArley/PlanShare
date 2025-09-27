using PlanShare.Domain.Dtos;
using System.Collections.Concurrent;

namespace PlanShare.Api.Hubs.Services;

public class CodeConnectionService
{
    private readonly ConcurrentDictionary<string, UserConnectionsDto> _connections;

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

        _connections.TryAdd(codeUser.Code, userConnection);
    }
}
