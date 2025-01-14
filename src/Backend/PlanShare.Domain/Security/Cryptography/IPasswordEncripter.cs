namespace PlanShare.Domain.Security.Cryptography;
public interface IPasswordEncripter
{
    string Encrypt(string password);
    bool IsValid(string password, string passwordHash);
}
