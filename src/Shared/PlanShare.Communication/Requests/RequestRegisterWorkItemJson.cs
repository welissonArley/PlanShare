namespace PlanShare.Communication.Requests;
public class RequestRegisterWorkItemJson
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
    public List<Guid> Assignees { get; set; } = [];
}
