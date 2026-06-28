using PlanShare.App.Data.Network.Api;
using PlanShare.App.Extensions;
using PlanShare.App.Models.ValueObjects;

namespace PlanShare.App.UseCases.WorkItem.GetById;
public class GetWorkItemByIdUseCase : IGetWorkItemByIdUseCase
{
    private readonly IWorkItemApi _workItemApi;

    public GetWorkItemByIdUseCase(IWorkItemApi workItemApi)
    {
        _workItemApi = workItemApi;
    }

    public async Task<Result<Models.WorkItemDetail>> Execute(Guid id)
    {
        var response = await _workItemApi.GetById(id);

        if (response.IsSuccessful)
        {
            var model = new Models.WorkItemDetail
            {
                Title = response.Content.Title,
                Description = response.Content.Description,
                DueDate = response.Content.DueDate,
                Assignees = response.Content.Assignees.Select(assignee => new Models.ConnectedUser
                {
                    Id = assignee.Id,
                    Name = assignee.Name,
                    ProfilePhotoUrl = assignee.ProfilePhotoUrl,
                }).ToList()
            };

            return Result<Models.WorkItemDetail>.Success(model);
        }

        var errorResponse = await response.Error.GetResponseError();

        return Result<Models.WorkItemDetail>.Failure(errorResponse.Errors);
    }
}
