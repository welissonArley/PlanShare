using PlanShare.App.Data.Network.Api;
using PlanShare.App.Models;
using PlanShare.Communication.Requests;

namespace PlanShare.App.UseCases.User.Register;
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserApi _userApi;

    public RegisterUserUseCase(IUserApi userApi)
    {
        _userApi = userApi;
    }

    public async Task Execute(UserRegisterAccount user)
    {
        var request = new RequestRegisterUserJson
        {
            Name = user.Name,
            Email = user.Email,
            Password = user.Password
        };

        var response = await _userApi.Register(request);
    }
}
