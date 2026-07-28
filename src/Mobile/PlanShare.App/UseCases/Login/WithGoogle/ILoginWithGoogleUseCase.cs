using PlanShare.App.Models.ValueObjects;

namespace PlanShare.App.UseCases.Login.WithGoogle;

public interface ILoginWithGoogleUseCase
{
    Task<Result> Execute();
}
