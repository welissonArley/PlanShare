using CommonTestUtilities.Requests;
using Shouldly;
using System.Net;

namespace WebApi.Tests.User.Update;

public class UpdateUserInvalidTokenTest : CustomClassFixture
{
    private const string METHOD = "/users";

    public UpdateUserInvalidTokenTest(CustomWebApplicationFactory webApplication) : base(webApplication)
    {
    }

    [Fact]
    public async Task Error_Token_Invalid()
    {
        var request = RequestUpdateUserBuilder.Build();

        var response = await DoPut(METHOD, request, token: "tokenInvalid");

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Error_Without_Token()
    {
        var request = RequestUpdateUserBuilder.Build();

        var response = await DoPut(METHOD, request, token: string.Empty);

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
}