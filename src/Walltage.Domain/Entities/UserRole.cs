using System.Collections.Generic;

namespace Walltage.Domain.Entities
{
    public class UserRole : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<User> UserList { get; set; }
    }
}