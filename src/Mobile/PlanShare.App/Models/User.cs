using CommunityToolkit.Mvvm.ComponentModel;

namespace PlanShare.App.Models;

public partial class User : ObservableObject
{
    [ObservableProperty]
    public string name;
    public string Email { get; set; } = string.Empty;
}
