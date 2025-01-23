namespace SampleSentry.API.Models
{
    public class JwtTokenDefaults
    {
        public string ValidAudience { get; set; } = string.Empty;
        public string ValidIssuer { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
        public int AccessTokenExpires { get; set; }
        public int RefreshTokenExpires { get; set; }
    }
}
