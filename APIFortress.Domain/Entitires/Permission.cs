namespace ApiFortress.Domain.Entities
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Role> Roles { get; set; } = new List<Role>();
    }
}