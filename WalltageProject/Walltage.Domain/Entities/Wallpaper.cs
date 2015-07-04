using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Walltage.Domain.Entities
{
    public class Wallpaper
    {
        public int WallpaperId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public int ResolutionId { get; set; }
        public string Size { get; set; }
        public int Viewed { get; set; }
        public int UploaderId { get; set; }
        public DateTime UploadDate { get; set; }
        public string Tags { get; set; }
        public string FilePath { get; set; }
    }
}
