namespace Demo.Application.Models
{
    public class GetAuthenticationTokenDto
    {
        public Guid Id { get; set; }
        public string AccessToken { get; set; }
        public string TokenType { get; set; }
        public long ExpiresIn { get; set; }
        public string Scope { get; set; }
        public DateTimeOffset CreationDate { get; set; }
    }
}
