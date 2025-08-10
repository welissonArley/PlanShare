using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace PlanShare.Infrastructure.Extensions;
public static class WebHostEnvironmentExtensions
{
    public static bool IsTests(this IWebHostEnvironment environment) => environment.IsEnvironment("Tests");
}
