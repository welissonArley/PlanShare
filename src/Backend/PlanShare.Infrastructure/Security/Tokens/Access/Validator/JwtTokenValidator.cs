using Microsoft.IdentityModel.Tokens;
using PlanShare.Domain.Security.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace PlanShare.Infrastructure.Security.Tokens.Access.Validator;

internal sealed class JwtTokenValidator : JwtTokenHandler, IAccessTokenValidator
{
    private readonly string _signingKey;

    public JwtTokenValidator(string signingKey) => _signingKey = signingKey;

    public Guid GetAccessTokenIdentifier(string token)
    {
        var identifier = GetClaimValue(token, JwtRegisteredClaimNames.Jti);

        return Guid.Parse(identifier);
    }

    public Guid GetUserIdentifier(string token)
    {
        var identifier = GetClaimValue(token, JwtRegisteredClaimNames.NameId);

        return Guid.Parse(identifier);
    }

    public void Validate(string token)
    {
        var validationParameters = new TokenValidationParameters()
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = SecurityKey(_signingKey),
            ClockSkew = new TimeSpan(0)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        tokenHandler.ValidateToken(token, validationParameters, out _);
    }

    private static string GetClaimValue(string token, string claimType)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtToken = tokenHandler.ReadJwtToken(token);

        return jwtToken.Claims.First(claim => claim.Type == claimType).Value;
    }
}
