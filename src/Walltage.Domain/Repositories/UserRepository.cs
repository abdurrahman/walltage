using System.Data.Entity;
using Walltage.Domain.Entities;

namespace Walltage.Domain.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(DbContext context)
            : base(context)
        {
        }
    }
}
