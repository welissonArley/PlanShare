using Moq;
using PlanShare.Domain.Security.Cryptography;

namespace CommonTestUtilities.Security.Cryptography;
public class PasswordEncripterBuilder
{
    private readonly Mock<IPasswordEncripter> _passwordEncripter;

    public PasswordEncripterBuilder()
    {
        _passwordEncripter = new Mock<IPasswordEncripter>();

        _passwordEncripter.Setup(encrypter => encrypter.Encrypt(It.IsAny<string>()))
            .Returns("passwordEncrypted");
    }

    public IPasswordEncripter Build() => _passwordEncripter.Object;
}
