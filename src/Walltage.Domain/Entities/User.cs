using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Walltage.Domain.Entities
{
    public class User : AuditableEntity
    {        
        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public DateTime LastActivity { get; set; }
        
        public string IPAddress { get; set; }

        public int UserRoleId { get; set; }

        [ForeignKey("UserRoleId")]
        public virtual UserRole UserRole { get; set; }

        public virtual ICollection<Tag> TagList { get; set; }

        public virtual ICollection<Wallpaper> WallpaperList { get; set; }

        public virtual ICollection<UserAndFavoriteMapping> FavoritedList { get; set; }

        public virtual ICollection<UserAndLikeMapping> LikedList { get; set; }
    }
}
