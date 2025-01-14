namespace PlanShare.Domain.Entities;
public class PersonAssociation : EntityBase
{
    /// <summary>
    /// The ID of the person initiating the association.
    /// </summary>
    public Guid PersonId { get; set; }

    /// <summary>
    /// The ID of the associated person.
    /// </summary>
    public Guid AssociatedPersonId { get; set; }

    public User Person { get; set; } = default!;
    public User AssociatedPerson { get; set; } = default!;
}
