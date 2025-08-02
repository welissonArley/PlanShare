using CommonTestUtilities.Repositories;
using PlanShare.Application.UseCases.User.Register;
using PlanShare.Domain.Extensions;

namespace UseCases.Tests.User.Register;
public class RegisterUserUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        var useCase = CreateUseCase();
    }

    private RegisterUserUseCase CreateUseCase(string? emailAlreadyExist = null)
    {
        var userReadOnlyRepository = new UserReadOnlyRepositoryBuilder();
        if(emailAlreadyExist.NotEmpty())
            userReadOnlyRepository.ExistActiveUserWithEmail(emailAlreadyExist);

        return new RegisterUserUseCase(null, null, userReadOnlyRepository.Build(), null, null);
    }
}
