using Moq;
using PlanShare.Domain.Repositories.User;

namespace CommonTestUtilities.Repositories;
public class UserReadOnlyRepositoryBuilder
{
    private readonly Mock<IUserReadOnlyRepository> _mockRepository;

    public UserReadOnlyRepositoryBuilder()
    {
        _mockRepository = new Mock<IUserReadOnlyRepository>();
    }

    public void ExistActiveUserWithEmail(string email)
    {
        _mockRepository.Setup(repository => repository.ExistActiveUserWithEmail(email))
            .ReturnsAsync(true);
    }

    public IUserReadOnlyRepository Build() => _mockRepository.Object;
}
