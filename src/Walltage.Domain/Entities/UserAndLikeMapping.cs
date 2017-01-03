using System.ComponentModel.DataAnnotations.Schema;

namespace Walltage.Domain.Entities
{
    public class UserAndLikeMapping : BaseEntity
    {
        public int UserId { get; set; }

        public int WallpaperId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("WallpaperId")]
        public virtual Wallpaper Wallpaper { get; set; }
    }
}
