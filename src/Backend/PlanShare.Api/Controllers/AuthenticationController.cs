using Microsoft.AspNetCore.Mvc;
using PlanShare.Application.UseCases.Token.RefreshToken;
using PlanShare.Communication.Requests;
using PlanShare.Communication.Responses;

namespace PlanShare.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    [HttpPost("refresh")]
    [ProducesResponseType(typeof(ResponseTokensJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Refresh(
        [FromServices] IUseRefreshTokenUseCase useCase,
        [FromBody] RequestNewTokenJson request)
    {
        var response = await useCase.Execute(request);

        return Ok(response);
    }
}
