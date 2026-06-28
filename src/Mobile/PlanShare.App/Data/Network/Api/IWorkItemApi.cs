using PlanShare.Communication.Requests;
using PlanShare.Communication.Responses;
using Refit;

namespace PlanShare.App.Data.Network.Api;

public interface IWorkItemApi
{
    [Multipart]
    [Post("/workitem")]
    Task<IApiResponse<ResponseRegisteredWorkItemJson>> Register(
        [AliasAs("Title")] string title,
        [AliasAs("Description")] string description,
        [AliasAs("DueDate")] string dueDate,
        [AliasAs("Assignees")] IEnumerable<string> assignees);

    [Get("/workitem/{id}")]
    Task<IApiResponse<ResponseWorkItemJson>> GetById(Guid id);

    [Delete("/workitem/{id}")]
    Task<IApiResponse> Delete(Guid id);

    [Put("/workitem/{id}")]
    Task<IApiResponse> Update(Guid id, [Body] RequestUpdateWorkItemJson request);
}
