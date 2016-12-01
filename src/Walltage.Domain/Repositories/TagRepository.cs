using System.Data.Entity;
using Walltage.Domain.Entities;

namespace Walltage.Domain.Repositories
{
    public class TagRepository : Repository<Tag>
    {
        public TagRepository(DbContext context)
            : base(context)
        {
        }
    }
}
