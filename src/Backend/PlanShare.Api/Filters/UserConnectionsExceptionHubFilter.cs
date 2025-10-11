using Microsoft.AspNetCore.SignalR;
using PlanShare.Api.Hubs.Services;
using PlanShare.Communication.Enums;
using PlanShare.Communication.Responses;
using PlanShare.Domain.Extensions;
using PlanShare.Exceptions;

namespace PlanShare.Api.Filters;

public class UserConnectionsExceptionHubFilter : IHubFilter
{
    private readonly CodeConnectionService _codeConnectionService;

    public UserConnectionsExceptionHubFilter(CodeConnectionService codeConnectionService)
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

            var code = _codeConnectionService.GetCodeByConnectionId(connectionId);
            if (code.NotEmpty())
            {
                var connection = _codeConnectionService.RemoveConnection(code);
                if (connection is not null && connection.ConnectingUserConnectionId.NotEmpty())
                {
                    await invocationContext.Hub.Clients.Client(connection.ConnectingUserConnectionId).SendAsync("ConnectionErrorOccurred");
                }
            }

            return HubOperationResult<string>.Failure(ResourceMessagesException.UNKNOWN_ERROR, UserConnectionErrorCode.Unknown);
        }
    }
}
