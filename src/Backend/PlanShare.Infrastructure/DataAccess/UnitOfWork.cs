using PlanShare.Domain.Repositories;

namespace PlanShare.Infrastructure.DataAccess;
internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly PlanShareDbContext _dbContext;

    public UnitOfWork(PlanShareDbContext dbContext) => _dbContext = dbContext;

    public async Task Commit() => await _dbContext.SaveChangesAsync();
}
