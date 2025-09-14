using PlanShare.App.Data.Storage.SecureStorage.Tokens;
using PlanShare.App.UseCases.Authentication.Refresh;
using PlanShare.Communication.Responses;
using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace PlanShare.App.Data.Network;
public class PlanShareHandler : DelegatingHandler
{
    private readonly ITokensStorage _tokensStorage;
    private readonly IUseRefreshTokenUseCase _useRefreshTokenUseCase;

    public PlanShareHandler(ITokensStorage tokensStorage, IUseRefreshTokenUseCase useRefreshTokenUseCase)
    {
        _tokensStorage = tokensStorage;
        _useRefreshTokenUseCase = useRefreshTokenUseCase;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        ChangeRequestCulture(request);

        var tokens = await _tokensStorage.Get();

        if(string.IsNullOrWhiteSpace(tokens.AccessToken) == false)
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);

        var response = await base.SendAsync(request, cancellationToken);
        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            await response.Content.LoadIntoBufferAsync(cancellationToken);

            var error = await response.Content.ReadFromJsonAsync<ResponseErrorJson>(cancellationToken);
            if (error!.TokenIsExpired)
            {
                var result = await _useRefreshTokenUseCase.Execute();

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.Response.AccessToken);
                response = await base.SendAsync(request, cancellationToken);
            }
        }

        return response;
    }

    private static void ChangeRequestCulture(HttpRequestMessage request)
    {
        var culture = CultureInfo.CurrentCulture.Name;

        request.Headers.AcceptLanguage.Clear();
        request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue(culture));
    }
}
