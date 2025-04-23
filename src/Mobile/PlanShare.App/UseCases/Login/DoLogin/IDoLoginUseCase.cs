using PlanShare.App.Models.ValueObjects;

namespace PlanShare.App.UseCases.Login.DoLogin;

public interface IDoLoginUseCase
{
    Task<Result> Execute(Models.Login model);
}
