using CommonTestUtilities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlanShare.Domain.Dtos;
using PlanShare.Domain.Security.Tokens;
using PlanShare.Infrastructure.DataAccess;
using WebApi.Tests.Resources;

namespace WebApi.Tests;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    public UserIdentityManager User;

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

                using var scope = services.BuildServiceProvider().CreateScope();
                
                var dbContext = scope.ServiceProvider.GetRequiredService<PlanShareDbContext>();
                dbContext.Database.EnsureDeleted();

                var accessTokenGenerator = scope.ServiceProvider.GetRequiredService<IAccessTokenGenerator>();

                StartDatabase(dbContext, accessTokenGenerator);
            });
    }

    private void StartDatabase(PlanShareDbContext dbContext, IAccessTokenGenerator accessTokenGenerator)
    {
        (var user, var password) = UserBuilder.Build();

        dbContext.Users.Add(user);
        dbContext.SaveChanges();

        var tokensDto = new TokensDto()
        {
            Access = accessTokenGenerator.Generate(user).token
        };

        User = new UserIdentityManager(user, password, tokensDto);
    }
}
