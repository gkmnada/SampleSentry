namespace SampleSentry.API.Models
{
    public class JwtTokenRequest
    {
        public string UserID { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<string> Role { get; set; } = new List<string>();
    }
}
