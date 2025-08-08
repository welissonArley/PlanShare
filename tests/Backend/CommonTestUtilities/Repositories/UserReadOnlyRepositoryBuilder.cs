using Moq;
using PlanShare.Domain.Entities;
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

    public void GetUserByEmail(User user)
    {
        _mockRepository.Setup(repository => repository.GetUserByEmail(user.Email))
            .ReturnsAsync(user);
    }

    public IUserReadOnlyRepository Build() => _mockRepository.Object;
}
