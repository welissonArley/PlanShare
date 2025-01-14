using FluentValidation;
using PlanShare.Communication.Requests;
using PlanShare.Exceptions;

namespace PlanShare.Application.UseCases.WorkItem.Update;
public class UpdateWorkItemValidator : AbstractValidator<RequestUpdateWorkItemJson>
{
    public UpdateWorkItemValidator()
    {
        RuleFor(request => request.Title).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);
        RuleFor(request => request.DueDate.Date).GreaterThanOrEqualTo(DateTime.UtcNow.Date);
    }
}