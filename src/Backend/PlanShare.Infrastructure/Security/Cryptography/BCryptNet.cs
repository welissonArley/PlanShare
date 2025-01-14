using PlanShare.Domain.Security.Cryptography;

namespace PlanShare.Infrastructure.Security.Cryptography;
internal sealed class BCryptNet : IPasswordEncripter
{
    public string Encrypt(string password) => BCrypt.Net.BCrypt.HashPassword(password);

    public bool IsValid(string password, string passwordHash) => BCrypt.Net.BCrypt.Verify(password, passwordHash);
}