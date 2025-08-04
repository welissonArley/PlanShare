using Bogus;
using PlanShare.Domain.Entities;

namespace CommonTestUtilities.Entities;
public class UserBuilder
{
    public static User Build()
    {
        return new Faker<User>()
            .RuleFor(user => user.Name, f => f.Person.FirstName)
            .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name))
            .RuleFor(user => user.Password, f => f.Internet.Password());
    }
}
