using Moq;
using PlanShare.Domain.Entities;
using PlanShare.Domain.Services.LoggedUser;

namespace CommonTestUtilities.Services.LoggedUser;
public class LoggedUserBuilder
{
    public static ILoggedUser Build(User user)
    {
        var mock = new Mock<ILoggedUser>();

        mock.Setup(loggedUser => loggedUser.Get()).ReturnsAsync(user);

        return mock.Object;
    }
}
