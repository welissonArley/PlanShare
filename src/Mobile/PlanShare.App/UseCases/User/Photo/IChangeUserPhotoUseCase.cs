using PlanShare.App.Models.ValueObjects;

namespace PlanShare.App.UseCases.User.Photo;
public interface IChangeUserPhotoUseCase
{
    Task<Result> Execute(FileResult file);
}
