namespace ApiFortress.Infrastructure.Providers
{
    public class APIKeyProvider
    {
        private readonly List<string> _validApiKeys = new List<string>
        {
            "apikey-12345",
            "apikey-67890"
        };

        public bool IsValidApiKey(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                return false;
            return _validApiKeys.Contains(apiKey);
        }
    }
}