using PlanShare.Communication.Requests;
using PlanShare.Domain.Extensions;
using PlanShare.Exceptions;
using Shouldly;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Tests.InlineData;
using WebApi.Tests.Resources;

namespace WebApi.Tests.User.Profile;
public class ChangeUserPasswordErrorTest : CustomClassFixture
{
    private const string BaseUrl = "/users/change-password";

    private readonly UserIdentityManager _user;

    public ChangeUserPasswordErrorTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _user = factory.User;
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_NewPassword_Empty(string culture)
    {
        var request = new RequestChangePasswordJson
        {
            NewPassword = string.Empty,
            Password = _user.GetPassword()
        };

        var response = await DoPut(BaseUrl, request, _user.GetAccessToken(), culture);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        using var responseBody = await response.Content.ReadAsStreamAsync();

        var document = await JsonDocument.ParseAsync(responseBody);

        var errors = document.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessagesException.ResourceManager.GetString("PASSWORD_EMPTY", new CultureInfo(culture));

        errors.ShouldSatisfyAllConditions(erros =>
        {
            erros.Count().ShouldBe(1);
            erros.ShouldContain(error => error.GetString().NotEmpty() && error.GetString()!.Equals(expectedMessage));
        });
    }
}
