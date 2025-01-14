using Microsoft.AspNetCore.Mvc;
using PlanShare.Application.UseCases.WorkItem.Delete;
using PlanShare.Application.UseCases.WorkItem.GetAll;
using PlanShare.Application.UseCases.WorkItem.GetById;
using PlanShare.Application.UseCases.WorkItem.Register;
using PlanShare.Application.UseCases.WorkItem.Update;
using PlanShare.Communication.Requests;
using PlanShare.Communication.Responses;

namespace PlanShare.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class WorkItemController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredWorkItemJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterWorkItemUseCase useCase,
        [FromForm] RequestRegisterWorkItemJson request,
        List<IFormFile> files)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromServices] IDeleteWorkItemUseCase useCase, [FromRoute] Guid id)
    {
        await useCase.Execute(id);

        return NoContent();
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseWorkItemJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromServices] IGetByIdWorkItemUseCase useCase, [FromRoute] Guid id)
    {
        var result = await useCase.Execute(id);

        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseWorkItemJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAll([FromServices] IGetAllWorkItemUseCase useCase)
    {
        var result = await useCase.Execute();
        if(result.WorkItems.Count == 0)
            return NoContent();

        return Ok(result);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromServices] IUpdateWorkItemUseCase useCase, [FromRoute] Guid id, [FromBody] RequestUpdateWorkItemJson request)
    {
        await useCase.Execute(id, request);

        return NoContent();
    }
}
