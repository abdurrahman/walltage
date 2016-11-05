using System.ComponentModel.DataAnnotations.Schema;

namespace Walltage.Domain.Entities
{
    public class WallpaperAndTagMapping : BaseEntity
    {
        public int TagId { get; set; }

        public int WallpaperId { get; set; }

        [ForeignKey("TagId")]
        public virtual Tag Tag { get; set; }

        [ForeignKey("WallpaperId")]
        public virtual Wallpaper Wallpaper { get; set; }
    }
}
