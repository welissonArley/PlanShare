namespace PlanShare.Communication.Responses;
public class ResponseAssigneeJson
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? ProfilePhotoUrl { get; set; }
}
