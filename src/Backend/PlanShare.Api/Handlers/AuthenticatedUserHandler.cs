using Microsoft.AspNetCore.Authorization;
using PlanShare.Api.Handlers.Requirements;
using PlanShare.Domain.Repositories.RefreshToken;
using PlanShare.Domain.Repositories.User;
using PlanShare.Domain.Security.Tokens;
using PlanShare.Exceptions;
using PlanShare.Exceptions.ExceptionsBase;

namespace PlanShare.Api.Handlers;

public class AuthenticatedUserHandler : AuthorizationHandler<AuthenticatedUserRequirement>
{
    private readonly IAccessTokenValidator _accessTokenValidator;
    private readonly IUserReadOnlyRepository _repository;
    private readonly IRefreshTokenReadOnlyRepository _refreshTokenRepository;

    public AuthenticatedUserHandler(
        IAccessTokenValidator accessTokenValidator,
        IUserReadOnlyRepository repository,
        IRefreshTokenReadOnlyRepository refreshTokenRepository)
    {
        _accessTokenValidator = accessTokenValidator;
        _repository = repository;
        _refreshTokenRepository = refreshTokenRepository;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        AuthenticatedUserRequirement requirement)
    {
        var token = TokenOnConnection(context);
    }

    private static string TokenOnConnection(AuthorizationHandlerContext context)
    {
        var defaultHttpContext = context.Resource as DefaultHttpContext;

        var authentication = defaultHttpContext?.Request.Headers.Authorization.ToString();
        if (string.IsNullOrWhiteSpace(authentication))
        {
            throw new UnauthorizedException(ResourceMessagesException.NO_TOKEN);
        }

        return authentication["Bearer ".Length..].Trim();
    }
}
