
using PlanShare.App.Data.Network.Api;
using PlanShare.Communication.Requests;

namespace PlanShare.App.UseCases.Login.DoLogin;

public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly ILoginApi _loginApi;

    public DoLoginUseCase(ILoginApi loginApi)
    {
        _loginApi = loginApi;
    }

    public async Task Execute(Models.Login login)
    {
        var request = new RequestLoginJson
        {
            Email = login.Email,
            Password = login.Password
        };

        var result = await _loginApi.Login(request);
    }
}
