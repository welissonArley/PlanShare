using Microsoft.AspNetCore.Mvc;
using PlanShare.Application.UseCases.Dashboard;
using PlanShare.Communication.Responses;

namespace PlanShare.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class DashboardController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDashboardJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromServices] IGetDashboardUseCase useCase)
    {
        var response = await useCase.Execute();

        return Ok(response);
    }
}
