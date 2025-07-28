using Bogus;
using PlanShare.Communication.Requests;

namespace CommonTestUtilities.Requests;
public class RequestUpdateUserBuilder
{
    public static RequestUpdateUserJson Build()
    {
        return new Faker<RequestUpdateUserJson>()
            .RuleFor(user => user.Name, f => f.Person.FirstName)
            .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name));
    }
}
