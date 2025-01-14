using System.Net;

namespace PlanShare.Exceptions.ExceptionsBase;

public class RefreshTokenExpiredException : PlanShareException
{
    public RefreshTokenExpiredException() : base(ResourceMessagesException.EXPIRED_SESSION) { }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Forbidden;
}
