namespace PlanShare.Communication.Responses;
public class ResponseDashboardJson
{
    public List<ResponseAssigneeJson> Friends { get; set; } = [];
    public List<ResponseShortWorkItemJson> WorkItems { get; set; } = [];
}
