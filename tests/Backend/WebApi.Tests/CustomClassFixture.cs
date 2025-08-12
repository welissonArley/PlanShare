using PlanShare.Domain.Extensions;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace WebApi.Tests;
public abstract class CustomClassFixture : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;

    public CustomClassFixture(CustomWebApplicationFactory factory) => _httpClient = factory.CreateClient();

    protected async Task<HttpResponseMessage> DoPost(
        string baseUrl,
        object request,
        string culture = "en",
        string token = "")
    {
        ChangeRequestCulture(culture);
        AuthorizeRequest(token);

        return await _httpClient.PostAsJsonAsync(baseUrl, request);
    }

    protected async Task<HttpResponseMessage> DoGet(
        string baseUrl,
        string token = "",
        string culture = "en")
    {
        ChangeRequestCulture(culture);
        AuthorizeRequest(token);

        return await _httpClient.GetAsync(baseUrl);
    }

    protected async Task<HttpResponseMessage> DoPut(
        string baseUrl,
        object request,
        string token,
        string culture = "en")
    {
        ChangeRequestCulture(culture);
        AuthorizeRequest(token);

        return await _httpClient.PutAsJsonAsync(baseUrl, request);
    }

    protected async Task<HttpResponseMessage> DoDelete(string baseUrl, string token, string culture = "en")
    {
        ChangeRequestCulture(culture);
        AuthorizeRequest(token);

        return await _httpClient.DeleteAsync(baseUrl);
    }

    private void ChangeRequestCulture(string culture)
    {
        _httpClient.DefaultRequestHeaders.AcceptLanguage.Clear();
        _httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue(culture));
    }

    private void AuthorizeRequest(string token)
    {
        if(token.NotEmpty())
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}