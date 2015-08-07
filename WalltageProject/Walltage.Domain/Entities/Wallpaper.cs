using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Walltage.Domain.Entities
{
    public class Wallpaper
    {
        public int WallpaperId { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public int ViewCount { get; set; }
        public string Tags { get; set; }
        public string ImgPath { get; set; }
        public DateTime UploadDate { get; set; }
        public string UploaderId { get; set; }
        public int ResolutionId { get; set; }
        public int CategoryId { get; set; }
    }

    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public DateTime Creation { get; set; }

        //public virtual ICollection<Wallpaper> wallpaperlist { get; set; }
    }

    public class Resolution
    { 
        public int ResolutionId { get; set; }
        public string Name { get; set; }
        public DateTime Creation { get; set; }

        //public virtual ICollection<Wallpaper> wallpaperlist { get; set; }
    }
}
