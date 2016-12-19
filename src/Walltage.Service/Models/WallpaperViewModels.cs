using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Walltage.Domain.Entities;

namespace Walltage.Service.Models
{
    public class WallpaperViewModel
    {
        public int WallpaperId { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        public int Size { get; set; }
        public int ViewCount { get; set; }
        public int FavoriteCount { get; set; }

        [Display(Name = "Tags")]
        public string Tags { get; set; }
        public string ImgPath { get; set; }

        public DateTime UploadDate { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        [Display(Name = "Resolution")]
        public int ResolutionId { get; set; }
        public string ResolutionName { get; set; }

        public int UploaderId { get; set; }
        public string UploaderName { get; set; }

        public HttpPostedFileBase file { get; set; }

        public Wallpaper Wallpaper { get; set; }
        public List<Category> CategoryList { get; set; }
        public List<Resolution> ResolutionList { get; set; }

        public IEnumerable<Wallpaper> WallpaperList { get; set; }
    }
}
