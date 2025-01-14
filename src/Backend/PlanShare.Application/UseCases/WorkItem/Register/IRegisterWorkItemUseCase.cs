using PlanShare.Communication.Requests;
using PlanShare.Communication.Responses;

namespace PlanShare.Application.UseCases.WorkItem.Register;
public interface IRegisterWorkItemUseCase
{
    Task<ResponseRegisteredWorkItemJson> Execute(RequestRegisterWorkItemJson request);
}
