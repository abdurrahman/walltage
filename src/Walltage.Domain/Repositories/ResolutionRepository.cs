using System.Data.Entity;
using Walltage.Domain.Entities;

namespace Walltage.Domain.Repositories
{
    public class ResolutionRepository : Repository<Resolution>
    {
        public ResolutionRepository(DbContext context)
            : base(context)
        {
        }
    }
}
