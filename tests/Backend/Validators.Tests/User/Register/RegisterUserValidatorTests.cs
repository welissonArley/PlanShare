using CommonTestUtilities.Requests;
using PlanShare.Application.UseCases.User.Register;
using PlanShare.Exceptions;
using Shouldly;

namespace Validators.Tests.User.Register;
public class RegisterUserValidatorTests
{
    [Fact]
    public void Success()
    {
        // Arrange
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserBuilder.Build();

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Error_Name_Empty()
    {
        // Arrange
        var validator = new RegisterUserValidator();

        var request = RequestRegisterUserBuilder.Build();
        request.Name = string.Empty;

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldSatisfyAllConditions(errors =>
        {
            errors.Count.ShouldBe(1);
            errors.ShouldContain(error => error.ErrorMessage.Equals(ResourceMessagesException.NAME_EMPTY));
        });
    }
}
