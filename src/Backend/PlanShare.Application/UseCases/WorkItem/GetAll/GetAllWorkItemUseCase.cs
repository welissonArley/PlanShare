using AutoMapper;
using PlanShare.Communication.Responses;
using PlanShare.Domain.Repositories.WorkItem;
using PlanShare.Domain.Services.LoggedUser;

namespace PlanShare.Application.UseCases.WorkItem.GetAll;
public class GetAllWorkItemUseCase : IGetAllWorkItemUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IMapper _mapper;
    private readonly IWorkItemReadOnlyRepository _repository;

    public GetAllWorkItemUseCase(
        ILoggedUser loggedUser,
        IMapper mapper,
        IWorkItemReadOnlyRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseWorkItemsJson> Execute()
    {
        var loggedUser = await _loggedUser.Get();

        var workItem = await _repository.GetAll(loggedUser);

        return new ResponseWorkItemsJson
        {
            WorkItems = _mapper.Map<List<ResponseShortWorkItemJson>>(workItem)
        };
    }
}
