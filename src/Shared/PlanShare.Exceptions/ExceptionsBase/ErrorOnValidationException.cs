using System.Net;

namespace PlanShare.Exceptions.ExceptionsBase;

public class ErrorOnValidationException(IList<string> listErrors) : PlanShareException("")
{
    private readonly IList<string> _errorMenssages = listErrors;

    public override IList<string> GetErrorMessages() => _errorMenssages.Distinct().ToList();

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
}