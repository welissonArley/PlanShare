using System.Net;

namespace PlanShare.Exceptions.ExceptionsBase;

public class InvalidLoginException : PlanShareException
{
    public InvalidLoginException() : base(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID) { }

    public override IList<string> GetErrorMessages() => [Message];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}