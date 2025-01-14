namespace PlanShare.Communication.Requests;
public class RequestUpdateWorkItemJson
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
}
