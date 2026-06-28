namespace PlanShare.App.Models;
public class WorkItemDetail
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
    public List<ConnectedUser> Assignees { get; set; } = [];
}
