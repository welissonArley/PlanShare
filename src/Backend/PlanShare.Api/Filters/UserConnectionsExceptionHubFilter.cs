using Microsoft.AspNetCore.SignalR;
using PlanShare.Api.Hubs.Services;
using PlanShare.Communication.Enums;
using PlanShare.Communication.Responses;
using PlanShare.Domain.Extensions;
using PlanShare.Exceptions;

namespace PlanShare.Api.Filters;

public class UserConnectionsExceptionHubFilter : IHubFilter
{
    private readonly UserConnectionsService _codeConnectionService;

    public UserConnectionsExceptionHubFilter(UserConnectionsService codeConnectionService)
    {
        _codeConnectionService = codeConnectionService;
    }

    public async ValueTask<object?> InvokeMethodAsync(HubInvocationContext invocationContext, Func<HubInvocationContext, ValueTask<object?>> next)
    {
        try
        {
            return await next(invocationContext);
        }
        catch
        {
            var connectionId = invocationContext.Hub.Context.ConnectionId;

            var code = _codeConnectionService.RemoveCodeByConnectionId(connectionId);
            if (code.NotEmpty())
            {
                var connection = _codeConnectionService.RemoveConnectionByCode(code);
                if (connection is not null && connection.JoinerConnectionId.NotEmpty())
                    await invocationContext.Hub.Clients.Client(connection.JoinerConnectionId).SendAsync("ConnectionErrorOccurred");
            }

            return HubOperationResult<string>.Failure(ResourceMessagesException.UNKNOWN_ERROR, UserConnectionErrorCode.Unknown);
        }
    }
}
