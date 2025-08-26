using Bogus;

namespace CommonTestUtilities.Models;
public class UserBuilder
{
    public static PlanShare.App.Models.User Build()
    {
        return new Faker<PlanShare.App.Models.User>()
            .RuleFor(user => user.Name, f => f.Person.FirstName)
            .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name));
    }
}
