using PlanShare.Domain.Security.Cryptography;
using PlanShare.Infrastructure.Security.Cryptography;

namespace CommonTestUtilities.Security.Cryptography;
public class PasswordEncripterBuilder
{
    public static IPasswordEncripter Build() => new BCryptNet();
}
