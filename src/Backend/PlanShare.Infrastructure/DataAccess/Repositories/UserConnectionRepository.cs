using Microsoft.EntityFrameworkCore;
using PlanShare.Domain.Entities;
using PlanShare.Domain.Repositories.Connection;

namespace PlanShare.Infrastructure.DataAccess.Repositories;
internal sealed class UserConnectionRepository : IUserConnectionReadOnlyRepository, IUserConnectionWriteOnlyRepository
{
    private readonly PlanShareDbContext _dbContext;

    public UserConnectionRepository(PlanShareDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(UserConnection userConnection) => await _dbContext.UserConnections.AddAsync(userConnection);

    public async Task<bool> AreUsersConnected(User user1, User user2)
    {
        return await _dbContext
            .UserConnections
            .AsNoTracking()
            .AnyAsync(connection =>
                (connection.UserId == user1.Id && connection.ConnectedUserId == user2.Id) ||
                (connection.UserId == user2.Id && connection.ConnectedUserId == user1.Id));
    }

    public async Task<List<User>> GetConnectionsForUser(User user)
    {
        var connections = await _dbContext.UserConnections
            .AsNoTracking()
            .Include(connection => connection.User)
            .Include(connection => connection.ConnectedUser)
            .Where(connection => connection.UserId == user.Id || connection.ConnectedUserId == user.Id)
            .ToListAsync();

        var response = new List<User>();

        foreach (var connection in connections)
        {
            if(connection.UserId == user.Id)
                response.Add(connection.ConnectedUser);
            else
                response.Add(connection.User);
        }

        return response;
    }
}
