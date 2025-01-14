using Microsoft.EntityFrameworkCore;
using PlanShare.Domain.Entities;
using PlanShare.Domain.Repositories.WorkItem;

namespace PlanShare.Infrastructure.DataAccess.Repositories;
internal sealed class WorkItemRepository : IWorkItemWriteOnlyRepository, IWorkItemReadOnlyRepository, IWorkItemUpdateOnlyRepository
{
    private readonly PlanShareDbContext _dbContext;

    public WorkItemRepository(PlanShareDbContext dbContext) => _dbContext = dbContext;

    public async Task Add(WorkItem workItem) => await _dbContext.WorkItems.AddAsync(workItem);

    public async Task Delete(Guid id)
    {
        var workItem = await _dbContext.WorkItems.FindAsync(id);

        _dbContext.WorkItems.Remove(workItem!);
    }

    async Task<WorkItem?> IWorkItemUpdateOnlyRepository.GetById(User user, Guid id)
    {
        return await _dbContext
            .WorkItems
            .SingleOrDefaultAsync(workItem => workItem.Id == id && workItem.Assignees.Any(assignee => assignee.UserId == user.Id));
    }

    async Task<WorkItem?> IWorkItemReadOnlyRepository.GetById(User user, Guid id)
    {
        return await _dbContext
            .WorkItems
            .AsNoTracking()
            .SingleOrDefaultAsync(workItem => workItem.Id == id && workItem.Assignees.Any(assignee => assignee.UserId == user.Id));
    }

    public void Update(WorkItem workItem) => _dbContext.WorkItems.Update(workItem);

    public async Task<List<WorkItem>> GetAll(User user)
    {
        return await _dbContext
            .WorkItems
            .AsNoTracking()
            .Where(workItem => workItem.Assignees.Any(assignee => assignee.UserId == user.Id))
            .ToListAsync();
    }
}
