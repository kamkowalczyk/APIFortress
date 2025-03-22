namespace ApiFortress.Domain.Services
{
    public class SecurityPolicyService
    {
        public bool ValidateRequest(string requestContent)
        {
            if (string.IsNullOrWhiteSpace(requestContent))
                return false;
            return !requestContent.Contains("'") && !requestContent.Contains("--");
        }

        public bool VerifyRateLimits(int requestCount, int limit)
        {
            return requestCount <= limit;
        }
    }
}
