using System.Collections.Concurrent;

namespace ApiFortress.Infrastructure.Providers
{
    public class RateLimiter
    {
        private readonly int _limit;
        private readonly TimeSpan _interval;
        private readonly ConcurrentDictionary<string, (int count, DateTime resetTime)> _requests
            = new ConcurrentDictionary<string, (int, DateTime)>();

        public RateLimiter(int limit, TimeSpan interval)
        {
            _limit = limit;
            _interval = interval;
        }

        public bool IsRequestAllowed(string ipAddress)
        {
            var now = DateTime.UtcNow;
            var entry = _requests.GetOrAdd(ipAddress, _ => (0, now.Add(_interval)));

            if (now > entry.resetTime)
            {
                entry = (0, now.Add(_interval));
            }

            if (entry.count < _limit)
            {
                _requests[ipAddress] = (entry.count + 1, entry.resetTime);
                return true;
            }

            return false;
        }
    }
}