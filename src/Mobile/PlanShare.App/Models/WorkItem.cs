using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace PlanShare.App.Models;
public partial class WorkItem : ObservableObject
{
    public string Title { get; set; } = string.Empty;
    public DateTime DueDate { get; set; } = DateTime.Now;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DescriptionCharacterCount))]
    public string? description;

    public ObservableCollection<AssigneeSelection> Assignees { get; } = [];

    public string DescriptionCharacterCount => $"{Description?.Length ?? 0}/200";
}
