using Microsoft.EntityFrameworkCore;
using PlanShare.Domain.Entities;
using PlanShare.Domain.Repositories.RefreshToken;

namespace PlanShare.Infrastructure.DataAccess.Repositories;
internal class RefreshTokenRepository : IRefreshTokenReadOnlyRepository, IRefreshTokenWriteOnlyRepository
{
    private readonly PlanShareDbContext _context;
    public RefreshTokenRepository(PlanShareDbContext context)
    {
        _context = context;
    }

    public async Task Add(RefreshToken refreshToken)
    {
        await _context.RefreshTokens
            .Where(token => token.UserId == refreshToken.UserId)
            .ExecuteDeleteAsync();

        await _context.RefreshTokens.AddAsync(refreshToken);
    }

    public async Task<RefreshToken?> Get(string token)
    {
        return await _context.RefreshTokens
            .AsNoTracking()
            .Include(refreshToken => refreshToken.User)
            .FirstOrDefaultAsync(refreshToken => refreshToken.Token.Equals(token));
    }

    public async Task<bool> HasRefreshTokenAssociated(User user, Guid accessTokenId)
    {
        return await _context
            .RefreshTokens
            .AnyAsync(refreshToken => refreshToken.UserId == user.Id && refreshToken.AccessTokenId == accessTokenId);
    }
}
