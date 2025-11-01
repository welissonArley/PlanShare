namespace PlanShare.Communication.Responses;
public class ResponseDashboardJson
{
    public string UserName { get; set; } = string.Empty;
    public List<ResponseAssigneeJson> ConnectedUsers { get; set; } = [];
    public List<ResponseShortWorkItemJson> WorkItems { get; set; } = [];
}
