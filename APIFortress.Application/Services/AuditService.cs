using ApiFortress.Application.DTOs;
using ApiFortress.Application.Interfaces;
using ApiFortress.Domain.Entities;
using ApiFortress.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiFortress.Application.Services
{
    public class AuditService : IAuditService
    {
        private readonly ApiFortressDbContext _context;

        public AuditService(ApiFortressDbContext context)
        {
            _context = context;
        }

        public async Task LogEventAsync(int userId, string eventDescription)
        {
            var log = new AuditDetails
            {
                UserId = userId,
                EventDescription = eventDescription,
                Timestamp = DateTime.UtcNow
            };

            _context.AuditDetails.Add(log);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AuditLogDTO>> GetAuditLogsAsync()
        {
            var logs = await _context.AuditDetails.OrderByDescending(l => l.Timestamp).ToListAsync();
            return logs.Select(l => new AuditLogDTO
            {
                Id = l.Id,
                UserId = l.UserId,
                EventDescription = l.EventDescription,
                Timestamp = l.Timestamp
            });
        }
    }
}