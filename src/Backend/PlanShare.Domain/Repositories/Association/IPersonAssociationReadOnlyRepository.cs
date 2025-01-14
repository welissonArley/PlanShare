namespace PlanShare.Domain.Repositories.Association;
public interface IPersonAssociationReadOnlyRepository
{
    Task<List<Entities.User>> GetPersonAssociationsForUser(Entities.User user);
}
