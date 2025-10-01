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

    public UserConnectionsDto? GetConnectionByCode(string code)
    {
        _connections.TryGetValue(code, out var userConnection);

        return userConnection;
    }

    public UserConnectionsDto? RemoveConnection(string code)
    {
        _connections.TryRemove(code, out var userConnection);

        return userConnection;
    }
}
