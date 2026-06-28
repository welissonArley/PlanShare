using PlanShare.App.Data.Network.Api;
using PlanShare.App.Extensions;
using PlanShare.App.Models.ValueObjects;
using PlanShare.Communication.Requests;

namespace PlanShare.App.UseCases.WorkItem.Register;
public class RegisterWorkItemUseCase : IRegisterWorkItemUseCase
{
    private readonly IWorkItemApi _workItemApi;

    public RegisterWorkItemUseCase(IWorkItemApi workItemApi)
    {
        _workItemApi = workItemApi;
    }

    public async Task<Result> Execute(Models.WorkItem workItem)
    {
        var request = new RequestRegisterWorkItemJson
        {
            Title = workItem.Title,
            Description = workItem.Description ?? string.Empty,
            DueDate = workItem.DueDate,
            Assignees = workItem.Assignees.Where(assignee => assignee.IsSelected).Select(assignee => assignee.Id).ToList()
        };

        var response = await _workItemApi.Register(
            request.Title,
            request.Description,
            request.DueDate.ToString("o"),
            request.Assignees.Select(assigneeId => assigneeId.ToString()));

        if (response.IsSuccessful)
            return Result.Success();

        var errorResponse = await response.Error.GetResponseError();

        return Result.Failure(errorResponse.Errors);
    }
}
