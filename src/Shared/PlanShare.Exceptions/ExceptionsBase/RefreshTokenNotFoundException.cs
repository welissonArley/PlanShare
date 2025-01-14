using System.Net;

namespace PlanShare.Exceptions.ExceptionsBase;

public class RefreshTokenNotFoundException : PlanShareException
{
    public RefreshTokenNotFoundException() : base(ResourceMessagesException.INVALID_SESSION) { }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}
