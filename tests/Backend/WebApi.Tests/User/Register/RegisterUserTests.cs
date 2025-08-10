using CommonTestUtilities.Requests;
using PlanShare.Domain.Extensions;
using PlanShare.Exceptions;
using Shouldly;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace WebApi.Tests.User.Register;
public class RegisterUserTests : IClassFixture<CustomWebApplicationFactory>
{
    private const string BaseUrl = "/users";

    private readonly HttpClient _httpClient;

    public RegisterUserTests(CustomWebApplicationFactory factory)
    {
        _httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserBuilder.Build();

        var response = await _httpClient.PostAsJsonAsync(BaseUrl, request);

        response.StatusCode.ShouldBe(HttpStatusCode.Created);

        using var responseBody = await response.Content.ReadAsStreamAsync();

        var document = await JsonDocument.ParseAsync(responseBody);

        document.RootElement.GetProperty("id").GetGuid().ShouldNotBe(Guid.Empty);
        document.RootElement.GetProperty("name").GetString().ShouldBe(request.Name);
        document.RootElement.GetProperty("tokens").GetProperty("accessToken").GetString().ShouldNotBeNullOrEmpty();
    }

    [Fact]
    public async Task Error_Name_Empty()
    {
        var request = RequestRegisterUserBuilder.Build();
        request.Name = string.Empty;

        var response = await _httpClient.PostAsJsonAsync(BaseUrl, request);

        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

        using var responseBody = await response.Content.ReadAsStreamAsync();

        var document = await JsonDocument.ParseAsync(responseBody);

        var errors = document.RootElement.GetProperty("errors").EnumerateArray();

        errors.ShouldSatisfyAllConditions(erros =>
        {
            erros.Count().ShouldBe(1);
            erros.ShouldContain(error => error.GetString().NotEmpty() && error.GetString()!.Equals(ResourceMessagesException.NAME_EMPTY));
        });
    }
}
