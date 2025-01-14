namespace PlanShare.Communication.Responses;
public class ResponseWorkItemJson
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
    public List<ResponseAssigneeJson> Assignees { get; set; } = [];
}
