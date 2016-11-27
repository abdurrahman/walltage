using System.Data.Entity;
using Walltage.Domain.Entities;

namespace Walltage.Domain.Repositories
{
    public class CategoryRepository : Repository<Category>
    {
        public CategoryRepository(DbContext context)
            : base (context)
        {
        }
    }
}
