namespace PlanShare.Domain.Dtos;
public class UserConnectionsDto
{
    /// <summary>
    /// The user id who sent the invitation.
    /// </summary>
    public required Guid UserId { get; set; }

    /// <summary>
    /// The connection id of the user who sent the invitation.
    /// </summary>
    public required string UserConnectionId { get; set; } = string.Empty;

    /// <summary>
    /// The user id who received the invitation.
    /// </summary>
    public Guid? ConnectingUserId { get; set; }

    /// <summary>
    /// The connection id of the user who received the invitation.
    /// </summary>
    public string? ConnectingUserConnectionId { get; set; }
}
