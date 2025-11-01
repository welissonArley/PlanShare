namespace PlanShare.App.Models;
public class ConnectedUser
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? ProfilePhotoUrl { get; set; }
}
