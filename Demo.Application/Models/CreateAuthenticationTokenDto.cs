using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Demo.Application.Models
{
    public class CreateAuthenticationTokenDto
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }
    }
}
