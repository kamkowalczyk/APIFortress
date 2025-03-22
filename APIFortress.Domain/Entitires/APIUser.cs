using System.Collections.Generic;

namespace ApiFortress.Domain.Entities
{
    public class APIUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PublicKey { get; set; }
        public List<Role> Roles { get; set; } = new List<Role>();
        public List<Permission> Permissions { get; set; } = new List<Permission>();
    }
}