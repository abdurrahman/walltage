using System.Collections.Generic;

namespace Walltage.Domain.Entities
{
    public class Resolution : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Wallpaper> WallpaperList { get; set; }
    }
}
