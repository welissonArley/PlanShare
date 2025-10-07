using PlanShare.Communication.Enums;

namespace PlanShare.Communication.Responses;
public class HubOperationResult<TResponse>
{
    public bool IsSuccess { get; init; }
    public TResponse? Response { get; init; }
    public string ErrorMessage { get; init; } = string.Empty;
    public UserConnectionErrorCode? ErrorCode { get; init; }

    public static HubOperationResult<TResponse> Success(TResponse response) => new() { IsSuccess = true, Response = response };
    public static HubOperationResult<TResponse> Failure(string errorMessage, UserConnectionErrorCode errorCode) => new() { IsSuccess = false, ErrorCode = errorCode, ErrorMessage = errorMessage };
}
