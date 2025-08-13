using Shouldly;
using System.Net;

namespace WebApi.Tests.User.Profile;
public class GetUserProfileInvalidTokenTest : CustomClassFixture
{
    private const string BaseUrl = "/users";

    public GetUserProfileInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Error_Invalid_Token()
    {
        var response = await DoGet(BaseUrl, token: "invalidToken");

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Error_Empty_Token()
    {
        var response = await DoGet(BaseUrl, token: string.Empty);

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
}
