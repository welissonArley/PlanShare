using PlanShare.Domain.Dtos;
using PlanShare.Domain.Entities;

namespace PlanShare.Application.Services.Authentication;
public interface ITokenService
{
    Task<TokensDto> GenerateTokens(User user);
}
