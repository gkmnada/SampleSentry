namespace SampleSentry.API.Models
{
    public class JwtTokenResponse
    {
        public JwtTokenResponse(string accessToken, DateTime expires)
        {
            AccessToken = accessToken;
            Expires = expires;
        }

        public string AccessToken { get; set; }
        public DateTime Expires { get; set; }
    }
}
