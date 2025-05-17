using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using PlanShare.Communication.Responses;
using PlanShare.Domain.Repositories.User;
using PlanShare.Domain.Security.Tokens;
using PlanShare.Exceptions;
using PlanShare.Exceptions.ExceptionsBase;

namespace PlanShare.Api.Filters;

public class AuthenticatedUserFilter : IAsyncAuthorizationFilter
{
    private readonly IAccessTokenValidator _accessTokenValidator;
    private readonly IUserReadOnlyRepository _repository;

    public AuthenticatedUserFilter(IAccessTokenValidator accessTokenValidator, IUserReadOnlyRepository repository)
    {
        _accessTokenValidator = accessTokenValidator;
        _repository = repository;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = TokenOnRequest(context);

            _accessTokenValidator.Validate(token);

            var userIdentifier = _accessTokenValidator.GetUserIdentifier(token);

            var user = await _repository.GetById(userIdentifier);
            if (user is null)
            {
                throw new UnauthorizedException(ResourceMessagesException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE);
            }
        }
        catch (SecurityTokenExpiredException)
        {
            var response = new ResponseErrorJson("TokenExpired")
            {
                TokenIsExpired = true
            };

            context.Result = new UnauthorizedObjectResult(response);
        }
        catch (UnauthorizedException unathorizedException)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(unathorizedException.GetErrorMessages()));
        }
        catch
        {
            context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ResourceMessagesException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE));
        }
    }

    private static string TokenOnRequest(AuthorizationFilterContext context)
    {
        var authentication = context.HttpContext.Request.Headers.Authorization.ToString();
        if(string.IsNullOrWhiteSpace(authentication))
        {
            throw new UnauthorizedException(ResourceMessagesException.NO_TOKEN);
        }

        return authentication["Bearer ".Length..].Trim();
    }
}
