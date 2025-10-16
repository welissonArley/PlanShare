namespace PlanShare.Domain.Dtos;
public class ConnectionByCode
{
    public required string Code { get; init; }

    /// <summary>
    /// User information of the code owner (generator).
    /// </summary>
    public required UserDto Generator { get; init; }

    /// <summary>
    /// The connection ID of the user who generated the code.
    /// </summary>
    public required string GeneratorConnectionId { get; init; }

    /// <summary>
    /// User information of the code joiner (the user who used the code).
    /// </summary>
    public UserDto? Joiner { get; set; }

    /// <summary>
    /// Joiner’s connection ID (the user who used the code).
    /// </summary>
    public string? JoinerConnectionId { get; set; }
}
