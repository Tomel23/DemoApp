using Demo.Application.Models;
using Demo.Infrastructure.Database;
using Demo.Infrastructure.Database.Entries;
using Demo.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace Demo.Test
{
    public class AuthenticationTokenRepositoryTest
    {
        [Fact]
        public async void CreateOrUpdateTokenAsync_ShouldAddNewIfListIsEmpntyOrReplaceExistedToken()
        {
            var token = new CreateAuthenticationTokenDto()
            {
                AccessToken = "Sapmle_token",
                ExpiresIn = 3600,
                Scope = "userapi",
                TokenType = "jwt_token"
            };

            using (DemoContext context = new DemoContext())
            {
                var _logger = new Mock<ILogger<AuthenticationTokenRepository>>();

                var _authenticationTokenRepository = new AuthenticationTokenRepository(context, _logger.Object);

                var tokensCount = context.DbTokens.Count();
                await _authenticationTokenRepository.CreateOrUpdateTokenAsync(token);
                int newTokenCount = context.DbTokens.Count();

                var result = await context.DbTokens.FirstAsync();

                if (tokensCount == 0)
                    Assert.Equal(tokensCount + 1, newTokenCount);

                Assert.Equal(1, newTokenCount);
                Assert.NotNull(result);
            }
        }
        [Fact]
        public async void GetTokenAsync_ShouldReturnTokenOrNull()
        {
            using (DemoContext context = new DemoContext())
            {
                var _logger = new Mock<ILogger<AuthenticationTokenRepository>>();
                var _authenticationTokenRepository = new AuthenticationTokenRepository(context, _logger.Object);

                if (context.DbTokens.Any())
                {
                    var result = await _authenticationTokenRepository.GetTokenAsync();
                    Assert.NotNull(result);
                }
                else
                {
                    context.DbTokens.Add(new AuthenticationToken()
                    {
                        Id = Guid.NewGuid(),
                        AccessToken = "Sapmle_token",
                        ExpiresIn = 3600,
                        Scope = "userapi",
                        TokenType = "jwt_token",
                        CreationDate = DateTimeOffset.UtcNow
                    });

                    var result = await _authenticationTokenRepository.GetTokenAsync();
                    Assert.NotNull(result);
                }
            }
        }
    }
}
