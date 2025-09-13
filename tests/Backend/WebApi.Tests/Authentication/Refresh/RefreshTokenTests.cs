using PlanShare.Communication.Requests;
using PlanShare.Domain.Extensions;
using PlanShare.Exceptions;
using Shouldly;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Tests.InlineData;
using WebApi.Tests.Resources;

namespace WebApi.Tests.Authentication.Refresh;
public class RefreshTokenTests : CustomClassFixture
{
    private const string BaseUrl = "/authentication/refresh";

    private readonly UserIdentityManager _user;

    public RefreshTokenTests(CustomWebApplicationFactory factory) : base(factory)
    {
        _user = factory.User;
    }

    [Fact]
    public async Task Success()
    {
        var request = new RequestNewTokenJson
        {
            RefreshToken = _user.GetRefreshToken(),
            AccessToken = _user.GetAccessToken(),
        };

        var response = await DoPost(BaseUrl, request);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        using var responseBody = await response.Content.ReadAsStreamAsync();

        var document = await JsonDocument.ParseAsync(responseBody);

        document.RootElement.GetProperty("accessToken").GetString().ShouldNotBeNullOrEmpty();
        document.RootElement.GetProperty("refreshToken").GetString().ShouldNotBeNullOrEmpty();
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_RefreshToken_Invalid(string culture)
    {
        var request = new RequestNewTokenJson
        {
            RefreshToken = "invalidRefreshToken",
            AccessToken = _user.GetAccessToken(),
        };

        var response = await DoPost(BaseUrl, request, culture);

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);

        using var responseBody = await response.Content.ReadAsStreamAsync();

        var document = await JsonDocument.ParseAsync(responseBody);

        var errors = document.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessagesException.ResourceManager.GetString("INVALID_SESSION", new CultureInfo(culture));

        errors.ShouldSatisfyAllConditions(erros =>
        {
            erros.Count().ShouldBe(1);
            erros.ShouldContain(error => error.GetString().NotEmpty() && error.GetString()!.Equals(expectedMessage));
        });
    }
}
