using System;
using System.Text.Json.Serialization;

namespace Demo.Infrastructure.Database.Entries
{
    public class AuthenticationToken
    {
        public Guid Id { get; set; }
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public long ExpiresIn { get; set; }
        public string Scope { get; set; }
        public DateTimeOffset CreationDate { get; set; }
    }
}