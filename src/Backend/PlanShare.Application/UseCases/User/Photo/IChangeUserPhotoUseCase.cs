using Microsoft.AspNetCore.Http;

namespace PlanShare.Application.UseCases.User.Photo;
public interface IChangeUserPhotoUseCase
{
    Task Execute(IFormFile file);
}
