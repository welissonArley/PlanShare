using PlanShare.Domain.Dtos;
using System.Collections.Concurrent;

namespace PlanShare.Api.Hubs.Services;

public class UserConnectionsService
{
    private readonly ConcurrentDictionary<string, ConnectionByCode> _connectionsByCode;
    private readonly ConcurrentDictionary<string, string> _codeByConnectionId;

    public UserConnectionsService()
    {
        _connectionsByCode = [];
        _codeByConnectionId = [];
    }

    public void Start(string code, UserDto generator, string connectionId)
    {
        var userConnection = new ConnectionByCode
        {
            Code = code,
            Generator = generator,
            GeneratorConnectionId = connectionId
        };

        _connectionsByCode.TryAdd(code, userConnection);
        _codeByConnectionId.TryAdd(connectionId, code);
    }

    public ConnectionByCode? GetConnectionByCode(string code)
    {
        _connectionsByCode.TryGetValue(code, out var userConnection);

        return userConnection;
    }

    public ConnectionByCode? RemoveConnectionByCode(string code)
    {
        _connectionsByCode.TryRemove(code, out var userConnection);

        return userConnection;
    }

    public string? RemoveCodeByConnectionId(string connectionId)
    {
        _codeByConnectionId.TryRemove(connectionId, out var code);
        return code;
    }
}
