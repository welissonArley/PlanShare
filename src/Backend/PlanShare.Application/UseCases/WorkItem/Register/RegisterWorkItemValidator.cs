using FluentValidation;
using PlanShare.Communication.Requests;
using PlanShare.Exceptions;

namespace PlanShare.Application.UseCases.WorkItem.Register;
public class RegisterWorkItemValidator : AbstractValidator<RequestRegisterWorkItemJson>
{
    public RegisterWorkItemValidator()
    {
        RuleFor(request => request.Title).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);
        RuleFor(request => request.DueDate.Date).GreaterThanOrEqualTo(DateTime.UtcNow.Date);
        RuleFor(request => request.Assignees.Count).GreaterThan(0);
    }
}
