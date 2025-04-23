using System.Globalization;
using System.Net.Http.Headers;

namespace PlanShare.App.Data.Network;
public class PlanShareHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var culture = CultureInfo.CurrentCulture.Name;

        request.Headers.AcceptLanguage.Clear();
        request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue(culture));

        return base.SendAsync(request, cancellationToken);
    }
}
