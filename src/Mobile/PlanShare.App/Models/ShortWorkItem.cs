namespace PlanShare.App.Models;
public class ShortWorkItem
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
}
