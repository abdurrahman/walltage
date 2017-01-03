using System.Collections.Generic;

namespace Walltage.Domain.Entities
{
    public class Category : AuditableEntity
    {
        /// <summary>
        /// Define category name
        /// </summary>
        public string Name { get; set; }

        public virtual ICollection<Wallpaper> WallpaperList { get; set; }
    }
}
