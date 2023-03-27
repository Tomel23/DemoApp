using Demo.Application.Interfaces;
using Demo.Application.Models;
using Demo.Infrastructure.Database.Entries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Demo.Infrastructure.Database.Repositories
{
    public class AuthenticationTokenRepository : IAuthenticationTokenRepository
    {
        private readonly DemoContext _context;
        private readonly ILogger<AuthenticationTokenRepository> _logger;

        public AuthenticationTokenRepository(DemoContext context, ILogger<AuthenticationTokenRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CreateOrUpdateTokenAsync(CreateAuthenticationTokenDto token)
        {
            try
            {
                var dbToken = await _context.DbTokens.FirstOrDefaultAsync();

                if (dbToken is not null)
                {
                    _context.DbTokens.Remove(dbToken);
                }

                await _context.DbTokens.AddAsync(new AuthenticationToken()
                {
                    Id = Guid.NewGuid(),
                    AccessToken = token.AccessToken,
                    ExpiresIn = token.ExpiresIn,
                    Scope = token.Scope,
                    TokenType = token.TokenType,
                    CreationDate=DateTimeOffset.UtcNow
                });

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during {nameof(GetTokenAsync)} operation: {ex.Message}", ex);
                throw;
            }
        }

        public async Task<GetAuthenticationTokenDto> GetTokenAsync()
        {
            try
            {
                var dbToken = await _context.DbTokens.FirstOrDefaultAsync();

                if (dbToken is null)
                    return null;
                return new GetAuthenticationTokenDto()
                {
                    Id= dbToken.Id,
                    AccessToken = dbToken.AccessToken,
                    ExpiresIn = dbToken.ExpiresIn,
                    Scope = dbToken.Scope,
                    TokenType = dbToken.TokenType,
                    CreationDate=dbToken.CreationDate
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during {nameof (GetTokenAsync)} operation: {ex.Message}", ex);
                throw;
            }
        }
    }
}
