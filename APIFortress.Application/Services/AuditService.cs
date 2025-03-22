using ApiFortress.Application.DTOs;
using ApiFortress.Application.Interfaces;

namespace ApiFortress.Application.Services
{
    public class AuditService : IAuditService
    {
        private readonly List<AuditLogDTO> _logs = new List<AuditLogDTO>();
        private int _logIdCounter = 1;

        public async Task LogEventAsync(int userId, string eventDescription)
        {
            var log = new AuditLogDTO
            {
                Id = _logIdCounter++,
                UserId = userId,
                EventDescription = eventDescription,
                Timestamp = DateTime.UtcNow
            };

            _logs.Add(log);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<AuditLogDTO>> GetAuditLogsAsync()
        {
            return await Task.FromResult(_logs);
        }
    }
}