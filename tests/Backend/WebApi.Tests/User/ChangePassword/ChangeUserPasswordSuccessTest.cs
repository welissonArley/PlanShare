using CommonTestUtilities.Requests;
using Shouldly;
using System.Net;
using WebApi.Tests.Resources;

namespace WebApi.Tests.User.ChangePassword;
public class ChangeUserPasswordSuccessTest : CustomClassFixture
{
    private const string BaseUrl = "/users/change-password";

    private readonly UserIdentityManager _user;

    public ChangeUserPasswordSuccessTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _user = factory.User;
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestChangePasswordBuilder.Build();
        request.Password = _user.GetPassword();

        var response = await DoPut(BaseUrl, request, _user.GetAccessToken());

        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }
}
