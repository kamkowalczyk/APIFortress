namespace ApiFortress.Domain.Events
{
    public class UserAuthenticatedEvent
    {
        public int UserId { get; }
        public DateTime Timestamp { get; }

        public UserAuthenticatedEvent(int userId)
        {
            UserId = userId;
            Timestamp = DateTime.UtcNow;
        }
    }
}
