using Bogus;
using CommonTestUtilities.Security.Cryptography;
using PlanShare.Domain.Entities;

namespace CommonTestUtilities.Entities;
public class UserBuilder
{
    public static (User user, string password) Build()
    {
        var faker = new Faker();
        var passwordEncripter = PasswordEncripterBuilder.Build();

        var password = faker.Internet.Password();

        var user = new Faker<User>()
            .RuleFor(user => user.Name, f => f.Person.FirstName)
            .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name))
            .RuleFor(user => user.Password, _ => passwordEncripter.Encrypt(password));

        return (user, password);
    }
}
