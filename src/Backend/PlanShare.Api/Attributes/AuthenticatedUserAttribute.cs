using Microsoft.AspNetCore.Mvc;
using PlanShare.Api.Filters;

namespace PlanShare.Api.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class AuthenticatedUserAttribute : TypeFilterAttribute<AuthenticatedUserFilter>
{
}
