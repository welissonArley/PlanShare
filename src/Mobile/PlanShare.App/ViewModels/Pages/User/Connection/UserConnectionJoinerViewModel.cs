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

        _connection.On("OnCancelled", OnCancelled);
        _connection.On("OnConnectionConfirmed", OnConnectionConfirmed);
        _connection.On("OnUserDisconnected", OnUserDisconnected);
        _connection.On("ConnectionErrorOccurred", ConnectionErrorOccurred);
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

            return;
        }

        await _connection.StopAsync();

        await _navigationService.ShowFailureFeedback(result.ErrorMessage);

        if (result.ErrorCode == Communication.Enums.UserConnectionErrorCode.InvalidCode)
            await _navigationService.GoToAsync($"../{RoutePages.USER_CODE_CONNECTION_PAGE}");
        else
            await _navigationService.ClosePage();
    }

    private void OnCancelled()
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await _connection.StopAsync();

            await _navigationService.ClosePage();

            await _navigationService.ShowFailureFeedback(string.Format(ResourceTexts.USER_WHO_GENERATED_CODE_CANCELED_CONNECTION, GeneratedBy));
        });
    }

    private void OnConnectionConfirmed()
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await _connection.StopAsync();

            await _navigationService.ClosePage();

            await _navigationService.ShowSuccessFeedback(string.Format(ResourceTexts.USER_WHO_GENERATED_CODE_APPROVED_CONNECTION, GeneratedBy));
        });
    }

    private void OnUserDisconnected()
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await _connection.StopAsync();

            await _navigationService.ClosePage();

            await _navigationService.ShowFailureFeedback(string.Format(ResourceTexts.THE_USER_WHO_GENERATED_CODE_LOST_CONNECTION, GeneratedBy));
        });
    }

    private void ConnectionErrorOccurred()
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await _connection.StopAsync();

            await _navigationService.ClosePage();

            await _navigationService.ShowFailureFeedback(string.Format(ResourceTexts.CONNECTION_UNEXPECTED_ERROR_WITH_USER, GeneratedBy));
        });
    }
}