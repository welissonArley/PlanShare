using Microsoft.AspNetCore.Authorization;
using PlanShare.Api.Handlers.Requirements;
using PlanShare.Domain.Extensions;
using PlanShare.Domain.Repositories.RefreshToken;
using PlanShare.Domain.Repositories.User;
using PlanShare.Domain.Security.Tokens;

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
        try
        {
            var token = TokenOnConnection(context);
            if (string.IsNullOrWhiteSpace(token))
            {
                context.Fail();
                return;
            }

            _accessTokenValidator.Validate(token);

            var userIdentifier = _accessTokenValidator.GetUserIdentifier(token);

            var user = await _repository.GetById(userIdentifier);
            if (user is null)
            {
                context.Fail();
                return;
            }

            var accessTokenId = _accessTokenValidator.GetAccessTokenIdentifier(token);
            var existRefreshTokenAssociated = await _refreshTokenRepository.HasRefreshTokenAssociated(user, accessTokenId);
            if (existRefreshTokenAssociated.IsFalse())
            {
                context.Fail();
                return;
            }

            context.Succeed(requirement);
        }
        catch
        {
            context.Fail();
        }
    }

    private static string TokenOnConnection(AuthorizationHandlerContext context)
    {
        var defaultHttpContext = context.Resource as DefaultHttpContext;

        var authentication = defaultHttpContext?.Request.Headers.Authorization.ToString();
        if (string.IsNullOrWhiteSpace(authentication))
            return string.Empty;

        return authentication["Bearer ".Length..].Trim();
    }
}
