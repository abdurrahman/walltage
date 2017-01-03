using Walltage.Domain.Entities;

namespace Walltage.Domain.Mappings
{
    public class WallpaperMap : BaseEntityMap<Wallpaper>
    {
        public WallpaperMap()
        {
            Property(t => t.Size).IsRequired();
            Property(t => t.ViewCount).IsOptional();
            Property(t => t.ImgPath).IsRequired().HasMaxLength(500);
            Property(t => t.UploaderId).IsRequired();
            Property(t => t.ResolutionId).IsRequired();

            //HasRequired(t => t.Category).WithMany(t => t.WallpaperList).HasForeignKey(t => t.CategoryId).WillCascadeOnDelete(false);
            //HasRequired(t => t.User).WithMany(t => t.WallpaperList).HasForeignKey(t => t.UploaderId).WillCascadeOnDelete(true);
            //HasMany(t => t.TagList).WithMany(t => t.WallpaperList).Map(t => t.MapLeftKey("TagId").MapRightKey("WallpaperId").ToTable("WallpaperAndTagMapping"));

            ToTable("Wallpaper");
        }
    }
}
