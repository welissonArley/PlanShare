using Moq;
using PlanShare.Domain.Repositories.RefreshToken;

namespace CommonTestUtilities.Repositories;
public class RefreshTokenWriteOnlyRepositoryBuilder
{
    public static IRefreshTokenWriteOnlyRepository Build()
    {
        var mock = new Mock<IRefreshTokenWriteOnlyRepository>();

        return mock.Object;
    }
}
