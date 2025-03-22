namespace ApiFortress.Application.DTOs
{
    public class AuditLogDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string EventDescription { get; set; }
        public DateTime Timestamp { get; set; }
    }
}