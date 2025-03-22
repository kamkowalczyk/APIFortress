using System.Collections.Concurrent;

namespace ApiFortress.Infrastructure.Providers
{
    public class IPBlocker
    {
        private readonly ConcurrentDictionary<string, bool> _blockedIPs = new ConcurrentDictionary<string, bool>();

        public void BlockIP(string ipAddress)
        {
            _blockedIPs[ipAddress] = true;
        }

        public bool IsIPBlocked(string ipAddress)
        {
            return _blockedIPs.ContainsKey(ipAddress);
        }

        public void UnblockIP(string ipAddress)
        {
            _blockedIPs.TryRemove(ipAddress, out _);
        }
    }
}
