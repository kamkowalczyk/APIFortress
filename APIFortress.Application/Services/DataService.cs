using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApiFortress.Application.DTOs;
using ApiFortress.Application.Interfaces;
using ApiFortress.Domain.Entities;
using ApiFortress.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace ApiFortress.Application.Services
{
    public class DataService : IDataService
    {
        private readonly ApiFortressDbContext _context;
        private readonly IAuditService _auditService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DataService(ApiFortressDbContext context, IAuditService auditService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _auditService = auditService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<DataItemDTO>> GetAllDataAsync()
        {
            var items = await _context.DataItems.ToListAsync();
            return items.Select(item => new DataItemDTO
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description
            });
        }

        public async Task<DataItemDTO> AddDataAsync(DataItemDTO dataDto)
        {
            var newItem = new DataItem
            {
                Title = dataDto.Title,
                Description = dataDto.Description
            };

            _context.DataItems.Add(newItem);
            await _context.SaveChangesAsync();

            int userId = 0;
            var userClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)
                             ?? _httpContextAccessor.HttpContext?.User?.FindFirst("sub");
            if (userClaim != null)
            {
                int.TryParse(userClaim.Value, out userId);
            }

            await _auditService.LogEventAsync(userId, $"New data added: {newItem.Title}");

            dataDto.Id = newItem.Id;
            return dataDto;
        }
    }
}
