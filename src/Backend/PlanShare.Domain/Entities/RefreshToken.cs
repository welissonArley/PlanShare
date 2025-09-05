namespace PlanShare.Domain.Entities;
public class RefreshToken : EntityBase
{
    public string Token { get; set; } = string.Empty;
    public Guid AccessTokenId { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
}
