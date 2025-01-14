using Microsoft.EntityFrameworkCore;
using PlanShare.Domain.Entities;
using PlanShare.Domain.Security.Tokens;
using PlanShare.Domain.Services.LoggedUser;
using PlanShare.Infrastructure.DataAccess;
using System.IdentityModel.Tokens.Jwt;

namespace PlanShare.Infrastructure.Services.LoggedUser;
internal sealed class LoggedUser : ILoggedUser
{
    private readonly PlanShareDbContext _dbContext;
    private readonly ITokenProvider _tokenValue;

    public LoggedUser(PlanShareDbContext dbContext, ITokenProvider tokenValue)
    {
        _dbContext = dbContext;
        _tokenValue = tokenValue;
    }

    public async Task<User> Get()
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(_tokenValue.Value());

        var identifier = jwtSecurityToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.NameId).Value;

        return await _dbContext
            .Users
            .AsNoTracking()
            .FirstAsync(user => user.Active && user.Id == Guid.Parse(identifier));
    }
}