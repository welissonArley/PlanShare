namespace PlanShare.App.UseCases.Login.DoLogin;

public interface IDoLoginUseCase
{
    Task Execute(Models.Login model);
}
