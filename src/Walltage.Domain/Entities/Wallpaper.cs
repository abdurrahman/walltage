using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Walltage.Domain.Entities
{
    public class Wallpaper : AuditableEntity
    {        
        /// <summary>
        /// Wallpaper file size
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Wallpaper view count
        /// </summary>
        public int ViewCount { get; set; }

        /// <summary>
        /// Wallpaper file path
        /// </summary>
        public string ImgPath { get; set; }

        /// <summary>
        /// Wallpaper uploaded by who
        /// </summary>
        public int UploaderId { get; set; }

        /// <summary>
        /// Wallpaper resolution
        /// </summary>
        public int ResolutionId { get; set; }
        
        /// <summary>
        /// Wallpaper category
        /// </summary>
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [ForeignKey("UploaderId")]
        public virtual User User { get; set; }

        [ForeignKey("ResolutionId")]
        public virtual Resolution Resolution { get; set; }

        public virtual ICollection<WallpaperAndTagMapping> TagList { get; set; }
    }
}
