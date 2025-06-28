using PlanShare.App.Data.Storage.SecureStorage.Tokens;
using System.Globalization;
using System.Net.Http.Headers;

namespace PlanShare.App.Data.Network;
public class PlanShareHandler : DelegatingHandler
{
    private readonly ITokensStorage _tokensStorage;

    public PlanShareHandler(ITokensStorage tokensStorage)
    {
        _tokensStorage = tokensStorage;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        ChangeRequestCulture(request);

        var tokens = await _tokensStorage.Get();

        if(string.IsNullOrWhiteSpace(tokens.AccessToken) == false)
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);

        return await base.SendAsync(request, cancellationToken);
    }

    private static void ChangeRequestCulture(HttpRequestMessage request)
    {
        var culture = CultureInfo.CurrentCulture.Name;

        request.Headers.AcceptLanguage.Clear();
        request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue(culture));
    }
}
