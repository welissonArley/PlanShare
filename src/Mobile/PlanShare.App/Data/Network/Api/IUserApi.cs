﻿using PlanShare.Communication.Requests;
using PlanShare.Communication.Responses;
using Refit;

namespace PlanShare.App.Data.Network.Api;

public interface IUserApi
{
    [Post("/users")]
    Task<IApiResponse<ResponseRegisteredUserJson>> Register([Body] RequestRegisterUserJson request);

    [Get("/users")]
    Task<IApiResponse<ResponseUserProfileJson>> GetProfile();

    [Put("/users")]
    Task<IApiResponse> UpdateProfile([Body] RequestUpdateUserJson request);

    [Put("/users/change-password")]
    Task<IApiResponse> ChangePassword([Body] RequestChangePasswordJson request);

    [Multipart]
    [Put("/users/change-photo")]
    Task<IApiResponse> ChangeProfilePhoto(StreamPart file);
}