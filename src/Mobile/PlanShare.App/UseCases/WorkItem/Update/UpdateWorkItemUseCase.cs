using PlanShare.App.Data.Network.Api;
using PlanShare.App.Extensions;
using PlanShare.App.Models.ValueObjects;
using PlanShare.Communication.Requests;

namespace PlanShare.App.UseCases.WorkItem.Update;
public class UpdateWorkItemUseCase : IUpdateWorkItemUseCase
{
    private readonly IWorkItemApi _workItemApi;

    public UpdateWorkItemUseCase(IWorkItemApi workItemApi)
    {
        _workItemApi = workItemApi;
    }

    public async Task<Result> Execute(Guid id, Models.WorkItem workItem)
    {
        var request = new RequestUpdateWorkItemJson
        {
            Title = workItem.Title,
            Description = workItem.Description ?? string.Empty,
            DueDate = workItem.DueDate,
        };

        var response = await _workItemApi.Update(id, request);

        if (response.IsSuccessful)
            return Result.Success();

        var errorResponse = await response.Error.GetResponseError();

        return Result.Failure(errorResponse.Errors);
    }
}
