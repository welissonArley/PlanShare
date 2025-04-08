using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace PlanShare.App.ViewModels.Pages.Login.DoLogin;

public partial class DoLoginViewModel : ObservableObject
{
    [ObservableProperty]
    public string texto;

    [RelayCommand]
    public async Task DoLogin()
    {
        var textoDigitado = Texto;

        Texto = "Welisson Arley";
    }
}
