using ApiFortress.Application.DTOs;

namespace ApiFortress.Application.Interfaces
{
    public interface IAuditService
    {
        Task LogEventAsync(int userId, string eventDescription);
        Task<IEnumerable<AuditLogDTO>> GetAuditLogsAsync();
    }
}