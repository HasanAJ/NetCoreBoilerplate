namespace NetCoreBoilerplate.Application.User.Authenticate
{
    public class TokenResposeDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
