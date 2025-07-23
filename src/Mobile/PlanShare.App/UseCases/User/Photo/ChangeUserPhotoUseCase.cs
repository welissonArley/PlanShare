using PlanShare.App.Data.Network.Api;
using PlanShare.App.Extensions;
using PlanShare.App.Models.ValueObjects;
using Refit;

namespace PlanShare.App.UseCases.User.Photo;
public class ChangeUserPhotoUseCase : IChangeUserPhotoUseCase
{
    private readonly IUserApi _userApi;

    public ChangeUserPhotoUseCase(IUserApi userApi)
    {
        _userApi = userApi;
    }

    public async Task<Result> Execute(FileResult file)
    {
        var photo = await file.OpenReadAsync();
        var request = new StreamPart(photo, file.FileName, file.ContentType);

        var response = await _userApi.ChangeProfilePhoto(request);
        if (response.IsSuccessful)
            return Result.Success();

        var errorResponse = await response.Error.GetResponseError();

        return Result.Failure(errorResponse.Errors);
    }
}
