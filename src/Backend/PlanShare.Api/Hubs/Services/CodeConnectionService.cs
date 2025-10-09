using PlanShare.Domain.Dtos;
using System.Collections.Concurrent;

namespace PlanShare.Api.Hubs.Services;

public class CodeConnectionService
{
    private readonly ConcurrentDictionary<string, UserConnectionsDto> _connectionsByCode;
    private readonly ConcurrentDictionary<string, string> _codeByConnectionId;

    public CodeConnectionService()
    {
        _connectionsByCode = [];
        _codeByConnectionId = [];
    }

    public void Start(CodeUserConnectionDto codeUser, string connectionId)
    {
        var userConnection = new UserConnectionsDto
        {
            UserId = codeUser.UserId,
            UserConnectionId = connectionId
        };

        _connectionsByCode.TryAdd(codeUser.Code, userConnection);
        _codeByConnectionId.TryAdd(connectionId, codeUser.Code);
    }

    public UserConnectionsDto? GetConnectionByCode(string code)
    {
        _connectionsByCode.TryGetValue(code, out var userConnection);

        return userConnection;
    }

    public UserConnectionsDto? RemoveConnection(string code)
    {
        _connectionsByCode.TryRemove(code, out var userConnection);

        return userConnection;
    }

    public string? GetCodeByConnectionId(string connectionId)
    {
        _codeByConnectionId.TryGetValue(connectionId, out var code);
        return code;
    }
}
