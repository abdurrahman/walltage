using System.Data.Entity;
using Walltage.Domain.Entities;

namespace Walltage.Domain.Repositories
{
    public class WallpaperRepository : Repository<Wallpaper>
    {
        public WallpaperRepository(DbContext context)
            : base (context)
        {
        }
    }
}
