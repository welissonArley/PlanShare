using Moq;
using PlanShare.Domain.Repositories;

namespace CommonTestUtilities.Repositories;
public class UnitOfWorkBuilder
{
    public static IUnitOfWork Build() => new Mock<IUnitOfWork>().Object;
}
