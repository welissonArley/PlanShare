using PlanShare.Domain.Extensions;
using System.Globalization;

namespace PlanShare.Api.Middleware;

public class CultureMiddleware
{
    readonly RequestDelegate _next;

    public CultureMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures);

        var culture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

        var cultureInfo = new CultureInfo("en");

        if (culture.NotEmpty() && supportedLanguages.Any(s => s.Name.Equals(culture)))
            cultureInfo = new CultureInfo(culture);

        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;

        await _next(context);
    }
}