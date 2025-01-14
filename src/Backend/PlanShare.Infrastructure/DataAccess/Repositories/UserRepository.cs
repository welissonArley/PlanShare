using Microsoft.EntityFrameworkCore;
using PlanShare.Domain.Entities;
using PlanShare.Domain.Repositories.User;

namespace PlanShare.Infrastructure.DataAccess.Repositories;
internal sealed class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository, IUserUpdateOnlyRepository
{
    private readonly PlanShareDbContext _dbContext;

    public UserRepository(PlanShareDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(User user) => await _dbContext.Users.AddAsync(user);

    public async Task<bool> ExistActiveUserWithEmail(string email) => await _dbContext.Users.AnyAsync(user => user.Email.Equals(email) && user.Active);

    public async Task<User> GetById(Guid id)
    {
        return await _dbContext
            .Users
            .SingleAsync(user => user.Active && user.Id == id);
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await _dbContext
            .Users
            .FirstOrDefaultAsync(user => user.Email.Equals(email) && user.Active);
    }

    public void Update(User user) => _dbContext.Users.Update(user);
}