using FileTypeChecker.Extensions;
using FileTypeChecker.Types;
using Microsoft.AspNetCore.Http;
using PlanShare.Domain.Extensions;
using PlanShare.Exceptions;
using PlanShare.Exceptions.ExceptionsBase;

namespace PlanShare.Application.UseCases.User.Photo;
public class ChangeUserPhotoUseCase : IChangeUserPhotoUseCase
{
    public async Task Execute(IFormFile file)
    {
        var photo = file.OpenReadStream();

        var isImage = photo.Is<JointPhotographicExpertsGroup>() || photo.Is<PortableNetworkGraphic>();
        if (isImage.IsFalse())
        {
            throw new ErrorOnValidationException(new List<string> { ResourceMessagesException.ONLY_IMAGES_ACCEPTED });
        }
    }
}
