namespace NetCoreBoilerplate.Application.Common.Config
{
    public class JwtConfig
    {
        public string Secret { get; set; }
        public int AccessTokenExpiryInSeconds { get; set; }
        public int RefreshTokenExpiryInDays { get; set; }
    }
}
