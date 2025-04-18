using CommunityToolkit.Mvvm.ComponentModel;
using PlanShare.App.Models;

namespace PlanShare.App.ViewModels.Pages;

public abstract partial class ViewModelBase : ObservableObject
{
    [ObservableProperty]
    public StatusPage statusPage;

    protected ViewModelBase()
    {
        StatusPage = StatusPage.Default;
    }
}
