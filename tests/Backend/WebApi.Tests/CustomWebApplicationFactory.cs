using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlanShare.Application.Services.Authentication;
using PlanShare.Domain.Repositories.RefreshToken;
using PlanShare.Infrastructure.DataAccess;
using WebApi.Tests.Resources;

namespace WebApi.Tests;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    public UserIdentityManager User = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Tests")
            .ConfigureServices(services =>
            {
                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContext<PlanShareDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseInternalServiceProvider(provider);
                });

                var mockRefreshTokenRepository = RefreshTokenWriteOnlyRepositoryBuilder.Build();

                services.AddScoped<IRefreshTokenWriteOnlyRepository>(config => mockRefreshTokenRepository);

                using var scope = services.BuildServiceProvider().CreateScope();
                
                var dbContext = scope.ServiceProvider.GetRequiredService<PlanShareDbContext>();
                dbContext.Database.EnsureDeleted();

                var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();

                StartDatabase(dbContext, tokenService);
            });
    }

    private void StartDatabase(PlanShareDbContext dbContext, ITokenService tokenService)
    {
        (var user, var password) = UserBuilder.Build();

        var tokensDto = tokenService.GenerateTokens(user);

        dbContext.Users.Add(user);
        
        dbContext.RefreshTokens.Add(new PlanShare.Domain.Entities.RefreshToken
        {
            Token = tokensDto.Refresh,
            AccessTokenId = tokensDto.AccessTokenId,
            UserId = user.Id
        });

        dbContext.SaveChanges();

        User = new UserIdentityManager(user, password, tokensDto);
    }
}
