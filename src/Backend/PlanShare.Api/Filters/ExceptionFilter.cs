using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using PlanShare.Communication.Responses;
using PlanShare.Exceptions;
using PlanShare.Exceptions.ExceptionsBase;

namespace PlanShare.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is PlanShareException planShareException)
            HandleProjectException(planShareException, context);
        else
            ThrowUnknowError(context);
    }

    private static void HandleProjectException(PlanShareException planShareException, ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)planShareException.GetStatusCode();
        context.Result = new ObjectResult(new ResponseErrorJson(planShareException.GetErrorMessages()));
    }

    private static void ThrowUnknowError(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessagesException.UNKNOWN_ERROR));
    }
}