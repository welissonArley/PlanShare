using PlanShare.Domain.Entities;

namespace PlanShare.Domain.Services.LoggedUser;
public interface ILoggedUser
{
    Task<User> Get();
}