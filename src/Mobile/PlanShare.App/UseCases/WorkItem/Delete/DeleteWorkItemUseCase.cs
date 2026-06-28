using PlanShare.App.Data.Network.Api;
using PlanShare.App.Extensions;
using PlanShare.App.Models.ValueObjects;

namespace PlanShare.App.UseCases.WorkItem.Delete;
public class DeleteWorkItemUseCase : IDeleteWorkItemUseCase
{
    private readonly IWorkItemApi _workItemApi;

    public DeleteWorkItemUseCase(IWorkItemApi workItemApi)
    {
        _workItemApi = workItemApi;
    }

    public async Task<Result> Execute(Guid id)
    {
        var response = await _workItemApi.Delete(id);

        if (response.IsSuccessful)
            return Result.Success();

        var errorResponse = await response.Error.GetResponseError();

        return Result.Failure(errorResponse.Errors);
    }
}
