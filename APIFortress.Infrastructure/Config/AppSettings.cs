namespace ApiFortress.Infrastructure.Config
{
    public class AppSettings
    {
        public string JwtSecret { get; set; }
        public string JwtIssuer { get; set; }
        public string JwtAudience { get; set; }
        public int JwtExpirationInMinutes { get; set; }
        public int RateLimit { get; set; }
        public int RateLimitIntervalInSeconds { get; set; }
        public string EncryptionKey { get; set; }
        public string EncryptionIV { get; set; }
    }
}
