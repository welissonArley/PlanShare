using Microsoft.EntityFrameworkCore;
using PlanShare.Domain.Entities;
using PlanShare.Domain.Repositories.Association;

namespace PlanShare.Infrastructure.DataAccess.Repositories;
internal sealed class PersonAssociationRepository : IPersonAssociationReadOnlyRepository
{
    private readonly PlanShareDbContext _dbContext;

    public PersonAssociationRepository(PlanShareDbContext dbContext) => _dbContext = dbContext;

    public async Task<List<User>> GetPersonAssociationsForUser(User user)
    {
        var associations = await _dbContext.PersonAssociations
            .AsNoTracking()
            .Include(association => association.AssociatedPerson)
            .Include(association => association.Person)
            .Where(association => association.PersonId == user.Id || association.AssociatedPersonId == user.Id)
            .ToListAsync();

        var response = new List<User>();

        foreach (var association in associations)
        {
            if(association.PersonId == user.Id)
                response.Add(association.AssociatedPerson);
            else
                response.Add(association.Person);
        }

        return response;
    }
}
