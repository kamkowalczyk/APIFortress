namespace ApiFortress.Domain.Entities
{
    public class AuditDetails
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string EventDescription { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
