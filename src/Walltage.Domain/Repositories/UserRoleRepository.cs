using System.Data.Entity;
using Walltage.Domain.Entities;

namespace Walltage.Domain.Repositories
{
    public class UserRoleRepository : Repository<UserRole>
    {
        public UserRoleRepository(DbContext context)
            : base(context)
        {
        }
    }
}
