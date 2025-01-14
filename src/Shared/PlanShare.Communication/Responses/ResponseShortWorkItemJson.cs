namespace PlanShare.Communication.Responses;
public class ResponseShortWorkItemJson
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
}
