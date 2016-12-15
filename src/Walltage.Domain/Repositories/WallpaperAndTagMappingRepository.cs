using System.Data.Entity;
using Walltage.Domain.Entities;

namespace Walltage.Domain.Repositories
{
    public class WallpaperAndTagMappingRepository : Repository<WallpaperAndTagMapping>
    {
        public WallpaperAndTagMappingRepository(DbContext context)
            : base (context)
        {

        }
    }
}
