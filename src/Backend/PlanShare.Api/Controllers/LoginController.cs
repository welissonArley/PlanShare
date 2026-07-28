using Google.Apis.Auth.AspNetCore3;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PlanShare.Application.UseCases.Login.DoLogin;
using PlanShare.Application.UseCases.Login.WithGoogle;
using PlanShare.Communication.Requests;
using PlanShare.Communication.Responses;
using PlanShare.Domain.Extensions;

namespace PlanShare.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(
        [FromServices] IDoLoginUseCase useCase,
        [FromBody] RequestLoginJson request)
    {
        var response = await useCase.Execute(request);

        return Ok(response);
    }

    [HttpGet("google")]
    public async Task<IActionResult> LoginWithGoogle([FromServices] ILoginWithGoogleUseCase useCase)
    {
        var auth = await Request.HttpContext.AuthenticateAsync(GoogleOpenIdConnectDefaults.AuthenticationScheme);
        if (ShouldChallengeGoogle(auth))
            return Challenge(GoogleOpenIdConnectDefaults.AuthenticationScheme);

        var claims = auth.Principal!.Identities.First().Claims;

        var email = claims.First(c => c.Type == System.Security.Claims.ClaimTypes.Email).Value;
        var name = claims.First(c => c.Type == System.Security.Claims.ClaimTypes.GivenName).Value;

        var response = await useCase.Execute(new Domain.Dtos.LoginThirdPartNameEmailDto(name, email));

        var redirectUrl =
    "planshare://" +
    $"?access_token={Uri.EscapeDataString(response.Tokens.AccessToken)}" +
    $"&refresh_token={Uri.EscapeDataString(response.Tokens.RefreshToken)}" +
    $"&name={Uri.EscapeDataString(response.Name)}" +
    $"&id={Uri.EscapeDataString(response.Id.ToString())}";

        return Redirect(redirectUrl);
    }

    private bool ShouldChallengeGoogle(AuthenticateResult authenticate)
    {
        return authenticate.Succeeded.IsFalse()
            || authenticate.Principal is null
            || authenticate.Principal.Identities.Any(id => id.IsAuthenticated).IsFalse();
    }
}
