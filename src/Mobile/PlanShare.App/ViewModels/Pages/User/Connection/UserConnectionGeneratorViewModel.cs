using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.SignalR.Client;
using PlanShare.App.Data.Network.Api;
using PlanShare.App.Models;
using PlanShare.App.Navigation;
using PlanShare.App.Resources;
using PlanShare.App.UseCases.Authentication.Refresh;
using PlanShare.Communication.Responses;

namespace PlanShare.App.ViewModels.Pages.User.Connection;

public partial class UserConnectionGeneratorViewModel : ViewModelBase
{
    [ObservableProperty]
    public new ConnectionByCodeStatusPage statusPage;

    [ObservableProperty]
    public string connectionCode;

    [ObservableProperty]
    public JoinerUser joinerUser;

    private readonly HubConnection _connection;

    // Codigo temporario
    private readonly IUseRefreshTokenUseCase _useRefreshTokenUseCase;

    public UserConnectionGeneratorViewModel(
        IUseRefreshTokenUseCase useRefreshTokenUseCase,

        IUserConnectionByCodeClient userConnectionByCodeClient,
        INavigationService navigationService) : base(navigationService)
    {
        _connection = userConnectionByCodeClient.CreateClient();
        _useRefreshTokenUseCase = useRefreshTokenUseCase;

        _connection.On<ResponseConnectingUserJson>("OnUserJoined", OnUserJoined);
    }

    [RelayCommand]
    public async Task Initialize()
    {
        StatusPage = ConnectionByCodeStatusPage.GeneratingCode;

        // Codigo Temporario
        await _useRefreshTokenUseCase.Execute();

        await _connection.StartAsync();

        var result = await _connection.InvokeAsync<HubOperationResult<string>>("GenerateCode");
        if (result.IsSuccess)
        {
            ConnectionCode = string.Join(' ', result.Response!.ToCharArray());

            StatusPage = ConnectionByCodeStatusPage.WaitingForJoiner;
        }
        else
        {
            await _connection.StopAsync();

            await _navigationService.ClosePage();

            await _navigationService.ShowFailureFeedback(result.ErrorMessage);
        }
    }

    [RelayCommand]
    public async Task Cancel()
    {
        await _connection.InvokeAsync("Cancel", ConnectionCode.Replace(" ", string.Empty));

        await _connection.StopAsync();

        await _navigationService.ClosePage();
    }

    [RelayCommand]
    public async Task Approve()
    {
        var result = await _connection.InvokeAsync<HubOperationResult<string>>("ConfirmCodeJoin", ConnectionCode.Replace(" ", string.Empty));
        if (result.IsSuccess)
            await _navigationService.ShowSuccessFeedback(string.Format(ResourceTexts.USER_JOINED_SUCCESSFULLY, JoinerUser.Name));
        else
            await _navigationService.ShowFailureFeedback(result.ErrorMessage);

        await _connection.StopAsync();

        await _navigationService.ClosePage();
    }

    private void OnUserJoined(ResponseConnectingUserJson response)
    {
        JoinerUser = new JoinerUser
        {
            Name = response.Name
        };

        StatusPage = ConnectionByCodeStatusPage.JoinerConnectedPendingApproval;
    }
}
