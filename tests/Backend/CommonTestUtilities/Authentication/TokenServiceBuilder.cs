using Moq;
using PlanShare.Application.Services.Authentication;
using PlanShare.Domain.Entities;

namespace CommonTestUtilities.Authentication;
public class TokenServiceBuilder
{
    public static ITokenService Build()
    {
        var mock = new Mock<ITokenService>();

        mock.Setup(tokenService => tokenService.GenerateTokens(It.IsAny<User>())).ReturnsAsync(new PlanShare.Domain.Dtos.TokensDto
        {
            Access = "mocked_access_token",
            Refresh = "mocked_refresh_token"
        });

        return mock.Object;
    }
}
