using CommonTestUtilities.Requests;
using PlanShare.Domain.Extensions;
using PlanShare.Exceptions;
using Shouldly;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Tests.InlineData;

namespace WebApi.Tests.User.Register;
public class RegisterUserTests : CustomClassFixture
{
    private const string BaseUrl = "/users";

    public RegisterUserTests(CustomWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserBuilder.Build();

        var response = await DoPost(BaseUrl, request);

        response.StatusCode.ShouldBe(HttpStatusCode.Created);

        using var responseBody = await response.Content.ReadAsStreamAsync();

        var document = await JsonDocument.ParseAsync(responseBody);

        document.RootElement.GetProperty("id").GetGuid().ShouldNotBe(Guid.Empty);
        document.RootElement.GetProperty("name").GetString().ShouldBe(request.Name);
        document.RootElement.GetProperty("tokens").GetProperty("accessToken").GetString().ShouldNotBeNullOrEmpty();
        document.RootElement.GetProperty("tokens").GetProperty("refreshToken").GetString().ShouldNotBeNullOrEmpty();
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Name_Empty(string culture)
    {
        var request = RequestRegisterUserBuilder.Build();
        request.Name = string.Empty;

        var response = await DoPost(BaseUrl, request, culture);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        using var responseBody = await response.Content.ReadAsStreamAsync();

        var document = await JsonDocument.ParseAsync(responseBody);

        var errors = document.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessagesException.ResourceManager.GetString("NAME_EMPTY", new CultureInfo(culture));

        errors.ShouldSatisfyAllConditions(erros =>
        {
            erros.Count().ShouldBe(1);
            erros.ShouldContain(error => error.GetString().NotEmpty() && error.GetString()!.Equals(expectedMessage));
        });
    }
}
