using Microsoft.Extensions.DependencyInjection;
using PlanShare.Application.Services.Authentication;
using PlanShare.Application.Services.AutoMapper;
using PlanShare.Application.UseCases.Dashboard;
using PlanShare.Application.UseCases.Login.DoLogin;
using PlanShare.Application.UseCases.User.ChangePassword;
using PlanShare.Application.UseCases.User.Photo;
using PlanShare.Application.UseCases.User.Profile;
using PlanShare.Application.UseCases.User.Register;
using PlanShare.Application.UseCases.User.Update;
using PlanShare.Application.UseCases.WorkItem.Delete;
using PlanShare.Application.UseCases.WorkItem.GetAll;
using PlanShare.Application.UseCases.WorkItem.GetById;
using PlanShare.Application.UseCases.WorkItem.Register;
using PlanShare.Application.UseCases.WorkItem.Update;

namespace PlanShare.Application;
public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCases(services);
        AddTokenService(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
        services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
        services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
        services.AddScoped<IChangeUserPhotoUseCase, ChangeUserPhotoUseCase>();

        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();

        services.AddScoped<IRegisterWorkItemUseCase, RegisterWorkItemUseCase>();
        services.AddScoped<IDeleteWorkItemUseCase, DeleteWorkItemUseCase>();
        services.AddScoped<IUpdateWorkItemUseCase, UpdateWorkItemUseCase>();
        services.AddScoped<IGetByIdWorkItemUseCase, GetByIdWorkItemUseCase>();
        services.AddScoped<IGetAllWorkItemUseCase, GetAllWorkItemUseCase>();

        services.AddScoped<IGetDashboardUseCase, GetDashboardUseCase>();
    }

    private static void AddTokenService(IServiceCollection services) => services.AddScoped<ITokenService, TokenService>();
}
