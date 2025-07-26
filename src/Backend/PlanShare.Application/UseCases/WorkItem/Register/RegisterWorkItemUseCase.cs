using Mapster;
using PlanShare.Communication.Requests;
using PlanShare.Communication.Responses;
using PlanShare.Domain.Extensions;
using PlanShare.Domain.Repositories;
using PlanShare.Domain.Repositories.WorkItem;
using PlanShare.Exceptions.ExceptionsBase;

namespace PlanShare.Application.UseCases.WorkItem.Register;
public class RegisterWorkItemUseCase : IRegisterWorkItemUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWorkItemWriteOnlyRepository _repository;

    public RegisterWorkItemUseCase(IUnitOfWork unitOfWork, IWorkItemWriteOnlyRepository repository)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
    }

    public async Task<ResponseRegisteredWorkItemJson> Execute(RequestRegisterWorkItemJson request)
    {
        await Validate(request);

        var entity = request.Adapt<Domain.Entities.WorkItem>();

        await _repository.Add(entity);

        await _unitOfWork.Commit();

        return new()
        {
            Id = entity.Id,
            Title = entity.Title,
        };
    }

    private async Task Validate(RequestRegisterWorkItemJson request)
    {
        var result = new RegisterWorkItemValidator().Validate(request);

        if (result.IsValid.IsFalse())
            throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).ToList());
    }
}
