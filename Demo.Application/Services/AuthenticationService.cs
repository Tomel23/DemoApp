using RestSharp.Authenticators;
using RestSharp;
using Microsoft.Extensions.Logging;
using Demo.Application.Models;
using Newtonsoft.Json;
using Demo.Application.Interfaces;

namespace Demo.Application.Services
{
    public interface IAuthenticatonService
    {
        public Task<string> GetAccessToken(string userId, string userPassword, string baseUrl);
    }
    public class AuthenticationService : IAuthenticatonService
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IAuthenticationTokenRepository _authenticationTokenRepository;

        public AuthenticationService(ILogger<AuthenticationService> logger, IAuthenticationTokenRepository authenticationTokenRepository)
        {
            _logger = logger;
            _authenticationTokenRepository = authenticationTokenRepository;
        }

        public async Task<string> GetAccessToken(string userId, string userPassword, string baseUrl)
        {
            try
            {
                var token = await _authenticationTokenRepository.GetTokenAsync();
                DateTimeOffset curretDate = DateTimeOffset.UtcNow;
                if (token is null ||
                    curretDate >= token.CreationDate.AddSeconds(token.ExpiresIn))
                {
                    var newToken = await RetriveAccessToken(userId, userPassword, baseUrl);
                    await _authenticationTokenRepository.CreateOrUpdateTokenAsync(newToken);
                    token = await _authenticationTokenRepository.GetTokenAsync();
                }

                return token.AccessToken;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<CreateAuthenticationTokenDto> RetriveAccessToken(string userId, string userPassword, string baseUrl)
        {
            var options = new RestClientOptions(baseUrl)
            {
                Authenticator = new HttpBasicAuthenticator(userId, userPassword),
            };
            var client = new RestClient(options);
            var request = new RestRequest() { Method = Method.Post };
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("scope", "USERAPI");

            var response = await client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                _logger.LogError($"Authorization token request failed with the following error: {response.Content}");
                throw new Exception(response.Content);
            }
            var result = JsonConvert.DeserializeObject<CreateAuthenticationTokenDto>(response.Content);

            if (result is null)
            {
                _logger.LogError("Retrived access token canot be null");
                throw new ArgumentNullException(nameof(result));
            }

            return result;
        }

    }
}
