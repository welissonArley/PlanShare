namespace PlanShare.Domain.Entities;

/// <summary>
/// If a record exists between two users, they are connected.
/// </summary>
public class UserConnection : EntityBase
{
    /// <summary>
    /// The user id who sent the invitation.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// The user id who received the invitation.
    /// </summary>
    public Guid ConnectedUserId { get; set; }

    /// <summary>
    /// The user who sent the invitation.
    /// </summary>
    public User User { get; set; } = default!;

    /// <summary>
    /// The user who received the invitation.
    /// </summary>
    public User ConnectedUser { get; set; } = default!;
}
