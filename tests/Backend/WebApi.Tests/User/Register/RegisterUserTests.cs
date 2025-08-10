using CommonTestUtilities.Requests;
using Shouldly;
using System.Net;
using System.Net.Http.Json;

namespace WebApi.Tests.User.Register;
public class RegisterUserTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;

    public RegisterUserTests(CustomWebApplicationFactory factory)
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
