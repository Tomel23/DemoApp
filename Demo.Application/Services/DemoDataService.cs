using Microsoft.Extensions.Logging;
using RestSharp.Authenticators;
using RestSharp;
using Microsoft.Extensions.Configuration;
using System.Xml.Linq;

namespace Demo.Application.Services
{
    public interface IDemoDataService
    {
        public Task<ICollection<string>> GetDemoPath();
    }
    public class DemoDataService : IDemoDataService
    {
        private readonly ILogger<DemoDataService> _logger;
        private readonly IAuthenticatonService _authenticatonService;
        private readonly IConfiguration _configuration;


        public DemoDataService(IConfiguration configuration,
                               IAuthenticatonService authenticatonService,
                               ILogger<DemoDataService> logger)
        {
            _configuration = configuration;
            _authenticatonService = authenticatonService;
            _logger = logger;
        }

        public async Task<ICollection<string>> GetDemoPath()
        {
            try
            {
                string token = await GetAccessToken();

                string wadlData = GetWadlData(token);

                HashSet<string> result = new HashSet<string>();

                XDocument doc = XDocument.Parse(wadlData);
                doc.Descendants().Where(p => p.Name.LocalName == "resource").ToList().ForEach(element => result.Add(element.Attribute("path")?.Value));

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("error during retriving demo data");
                throw;
            }
        }

        private string GetWadlData(string token)
        {
            var options = new RestClientOptions(@"https://portalcloudapi-test.assecobs.pl/?wadl&DBC=rest")
            {
                Authenticator = new JwtAuthenticator(token)
            };
            var client = new RestClient(options);
            var request = new RestRequest() { Method = Method.Get };

            var response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                _logger.LogError($"Authorization token request failed with the following error: {response.Content}");
                throw new Exception(response.Content);
            }

            return response .Content;
        }

        private async Task<string> GetAccessToken()
        {
            var authConfig = _configuration.GetSection("Authentication");

            if (authConfig is null)
            {
                _logger.LogError($"Configurtion error {nameof(authConfig)} value cannot be null");
                throw new ArgumentNullException("Configuration error vaule cannot be null");
            }

            var wadlUrl = _configuration.GetSection("WADLUrl");

            if (wadlUrl is null)
            {
                _logger.LogError($"Configurtion error {nameof(wadlUrl)} value cannot be null");
                throw new ArgumentNullException("Configuration error vaule cannot be null");
            }

            var token = await _authenticatonService.GetAccessToken(authConfig["User"], authConfig["Password"], authConfig["TokenUrl"]);
            
            if (string.IsNullOrEmpty(token))
            {
                _logger.LogError($"Configurtion error {nameof(token)} value cannot be null");
                throw new ArgumentNullException("Configuration error vaule cannot be null");
            }
            return token;
        }
    }
}
