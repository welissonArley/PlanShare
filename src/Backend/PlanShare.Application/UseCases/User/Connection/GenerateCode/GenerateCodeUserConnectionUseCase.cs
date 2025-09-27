using PlanShare.Domain.Dtos;
using PlanShare.Domain.Services.LoggedUser;
using System.Security.Cryptography;

namespace PlanShare.Application.UseCases.User.Connection.GenerateCode;
public class GenerateCodeUserConnectionUseCase : IGenerateCodeUserConnectionUseCase
{
    private readonly ILoggedUser _loggedUser;

    public GenerateCodeUserConnectionUseCase(ILoggedUser loggedUser)
    {
        _loggedUser = loggedUser;
    }

    public async Task<CodeUserConnectionDto> Execute()
    {
        var loggedUser = await _loggedUser.Get();

        var code = RandomNumberGenerator.GetInt32(fromInclusive: 1, toExclusive: 1_000_000).ToString("D6");

        return new CodeUserConnectionDto(code, loggedUser.Id);
    }
}
