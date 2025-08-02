using CommonTestUtilities.Authentication;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Security.Cryptography;
using PlanShare.Application.UseCases.User.Register;
using PlanShare.Domain.Extensions;
using Shouldly;

namespace UseCases.Tests.User.Register;
public class RegisterUserUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        var request = RequestRegisterUserBuilder.Build();

        var useCase = CreateUseCase();

        var result = await useCase.Execute(request);

        result.ShouldNotBeNull();
        result.Name.ShouldBe(request.Name);
    }

    private RegisterUserUseCase CreateUseCase(string? emailAlreadyExist = null)
    {
        var unitOfWork = UnitOfWorkBuilder.Build();
        var userWriteOnlyRepository = UserWriteOnlyRepositoryBuilder.Build();
        var passwordEncripter = new PasswordEncripterBuilder().Build();
        var tokenService = TokenServiceBuilder.Build();

        var userReadOnlyRepository = new UserReadOnlyRepositoryBuilder();
        if(emailAlreadyExist.NotEmpty())
            userReadOnlyRepository.ExistActiveUserWithEmail(emailAlreadyExist);

        return new RegisterUserUseCase(unitOfWork, userWriteOnlyRepository, userReadOnlyRepository.Build(), passwordEncripter, tokenService);
    }
}
