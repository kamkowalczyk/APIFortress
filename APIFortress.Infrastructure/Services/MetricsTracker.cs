using System;
using System.Threading;

namespace ApiFortress.Infrastructure.Services
{
    public class MetricsTracker
    {
        public DateTime AppStartTime { get; } = DateTime.UtcNow;
        private long _requestCount = 0;

        public long RequestCount => Interlocked.Read(ref _requestCount);

        public void IncrementRequestCount()
        {
            Interlocked.Increment(ref _requestCount);
        }
    }
}