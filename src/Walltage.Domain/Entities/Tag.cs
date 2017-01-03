using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Walltage.Domain.Entities
{
    public class Tag : AuditableEntity
    {
        /// <summary>
        /// Tag name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Tag creator who is
        /// </summary>
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<WallpaperAndTagMapping> WallpaperList { get; set; }
    }
}
