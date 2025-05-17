using Microsoft.AspNetCore.Mvc.Filters;
using PlanShare.Domain.Security.Tokens;
using PlanShare.Exceptions;
using PlanShare.Exceptions.ExceptionsBase;

namespace PlanShare.Api.Filters;

public class AuthenticatedUserFilter : IAsyncAuthorizationFilter
{
    private readonly IAccessTokenValidator _accessTokenValidator;

    public AuthenticatedUserFilter(IAccessTokenValidator accessTokenValidator)
    {
        _accessTokenValidator = accessTokenValidator;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var token = TokenOnRequest(context);

        _accessTokenValidator.Validate(token);
    }

    private string TokenOnRequest(AuthorizationFilterContext context)
    {
        var authentication = context.HttpContext.Request.Headers.Authorization.ToString();
        if(string.IsNullOrWhiteSpace(authentication))
        {
            throw new UnauthorizedException(ResourceMessagesException.NO_TOKEN);
        }

        return authentication["Bearer ".Length..].Trim();
    }
}
