using PlanShare.Domain.Dtos;

namespace PlanShare.Application.UseCases.User.Connection.GenerateCode;
public interface IGenerateCodeUserConnectionUseCase
{
    Task<CodeUserConnectionDto> Execute();
}
