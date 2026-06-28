using CommunityToolkit.Mvvm.ComponentModel;

namespace PlanShare.App.Models;
public partial class AssigneeSelection : ObservableObject
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    [ObservableProperty]
    public bool isSelected;
}
