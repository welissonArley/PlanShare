using CommonTestUtilities.Requests;
using Shouldly;
using System.Net;
using WebApi.Tests.Resources;

namespace WebApi.Tests.User.ChangePassword;
public class ChangeUserPasswordInvalidTokenTest : CustomClassFixture
{
    private const string BaseUrl = "/users/change-password";

    private readonly UserIdentityManager _user;

    public ChangeUserPasswordInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _user = factory.User;
    }

    [Fact]
    public async Task Error_Invalid_Token()
    {
        var request = RequestChangePasswordBuilder.Build();
        request.Password = _user.GetPassword();

        var response = await DoPut(BaseUrl, request, token: "invalidToken");

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Error_Empty_Token()
    {
        var request = RequestChangePasswordBuilder.Build();
        request.Password = _user.GetPassword();

        var response = await DoPut(BaseUrl, request, token: string.Empty);

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
}
