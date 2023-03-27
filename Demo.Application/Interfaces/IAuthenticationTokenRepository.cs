using Demo.Application.Models;

namespace Demo.Application.Interfaces
{
    public interface IAuthenticationTokenRepository
    {
       public Task<GetAuthenticationTokenDto> GetTokenAsync();
       public Task CreateOrUpdateTokenAsync(CreateAuthenticationTokenDto token); 
    }
}
