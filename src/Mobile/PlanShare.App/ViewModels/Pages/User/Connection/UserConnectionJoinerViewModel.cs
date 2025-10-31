using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.SignalR.Client;
using PlanShare.App.Data.Network.Api;
using PlanShare.App.Models;
using PlanShare.App.Navigation;
using PlanShare.App.UseCases.Authentication.Refresh;
using PlanShare.Communication.Responses;

namespace PlanShare.App.ViewModels.Pages.User.Connection;

[QueryProperty(nameof(Code), "Code")]
public partial class UserConnectionJoinerViewModel : ViewModelBase
{
    [ObservableProperty]
    public new ConnectionByCodeStatusPage statusPage;

    [ObservableProperty]
    public string generatedBy;

    public string Code { get; set; } = string.Empty;

    private readonly HubConnection _connection;

    // Codigo temporario
    private readonly IUseRefreshTokenUseCase _useRefreshTokenUseCase;

    public UserConnectionJoinerViewModel(
        IUseRefreshTokenUseCase useRefreshTokenUseCase,

        IUserConnectionByCodeClient userConnectionByCodeClient,
        INavigationService navigationService) : base(navigationService)
    {
        _useRefreshTokenUseCase = useRefreshTokenUseCase;
        _connection = userConnectionByCodeClient.CreateClient();
    }

    [RelayCommand]
    public async Task Initialize()
    {
        StatusPage = ConnectionByCodeStatusPage.WaitingForJoiner;

        // Codigo Temporario
        await _useRefreshTokenUseCase.Execute();

        await _connection.StartAsync();

        var result = await _connection.InvokeAsync<HubOperationResult<string>>("JoinWithCode", Code);
        if (result.IsSuccess)
        {
            GeneratedBy = result.Response!;

            StatusPage = ConnectionByCodeStatusPage.JoinerConnectedPendingApproval;
        }
        else
        {
            await _connection.StopAsync();

            await _navigationService.ClosePage();

            await _navigationService.ShowFailureFeedback(result.ErrorMessage);
        }
    }
}