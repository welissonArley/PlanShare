using System.Collections.ObjectModel;

namespace PlanShare.App.Models;
public class Dashboard
{
    public string UserName { get; set; } = string.Empty;
    public ObservableCollection<ConnectedUser> ConnectedUsers { get; set; } = [];
    public ObservableCollection<ShortWorkItem> WorkItems { get; set; } = [];
}
