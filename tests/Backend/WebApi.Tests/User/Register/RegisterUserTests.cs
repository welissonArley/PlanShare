using CommonTestUtilities.Requests;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;
using System.Net;
using System.Net.Http.Json;

namespace WebApi.Tests.User.Register;
public class RegisterUserTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;

    public RegisterUserTests(WebApplicationFactory<Program> factory)
    {
        _httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserBuilder.Build();

        var response = await _httpClient.PostAsJsonAsync("/users", request);

        response.StatusCode.ShouldBe(HttpStatusCode.Created);
    }
}
