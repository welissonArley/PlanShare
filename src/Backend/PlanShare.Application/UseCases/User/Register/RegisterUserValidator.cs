using FluentValidation;
using PlanShare.Application.SharedValidators;
using PlanShare.Communication.Requests;
using PlanShare.Domain.Extensions;
using PlanShare.Exceptions;

namespace PlanShare.Application.UseCases.User.Register;
public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(request => request.Name).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);
        RuleFor(request => request.Email).NotEmpty().WithMessage(ResourceMessagesException.EMAIL_EMPTY);
        RuleFor(request => request.Password).SetValidator(new PasswordValidator<RequestRegisterUserJson>());
        When(request => request.Email.NotEmpty(), () =>
        {
            RuleFor(request => request.Email).EmailAddress().WithMessage(ResourceMessagesException.EMAIL_INVALID);
        });
    }
}