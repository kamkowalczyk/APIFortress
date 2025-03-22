namespace ApiFortress.Domain.Events
{
    public class SecurityBreachDetectedEvent
    {
        public string Description { get; }
        public DateTime Timestamp { get; }

        public SecurityBreachDetectedEvent(string description)
        {
            Description = description;
            Timestamp = DateTime.UtcNow;
        }
    }
}