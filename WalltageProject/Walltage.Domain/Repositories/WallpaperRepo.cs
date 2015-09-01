using System;
using System.Collections.Generic;
using System.Linq;
using Walltage.Domain.Entities;

namespace Walltage.Domain.Repositories
{
    public class WallpaperRepo : IWallpaperRepository, IDisposable
    {
        private static WallpaperRepo _instance;
        public static WallpaperRepo Instance
        {
            get { return _instance ?? (_instance = new WallpaperRepo()); }
        }

        public bool Insert(Wallpaper entity)
        {
            using (var db = new WalltageDbContext())
            {
                db.Wallpapers.Add(entity);
                return db.SaveChanges() > 0 ? true : false;
            }
            return false;
        }

        public bool Update(Wallpaper entity)
        {
            using (var db = new WalltageDbContext())
            {
                var getWallpaper = db.Wallpapers.Find(entity.WallpaperId);
                
                getWallpaper.Name = entity.Name;
                getWallpaper.Size = entity.Size;
                getWallpaper.ImgPath = entity.ImgPath;
                getWallpaper.Tags = entity.Tags;

                return db.SaveChanges() > 0 ? true : false;
            }
        }

        public bool Delete(Wallpaper entity)
        {
            using (var db = new WalltageDbContext())
            {
                var getWallpaper = db.Wallpapers.Find(entity.WallpaperId);

                db.Wallpapers.Remove(getWallpaper);
                return db.SaveChanges() > 0 ? true : false;
            }
        }

        public Wallpaper FindById(int id)
        {
            using (var db = new WalltageDbContext())
            {
                return db.Wallpapers.Include("categories").Include("resolutions").Where(x => x.WallpaperId == id).FirstOrDefault();
            }
        }

        public IEnumerable<Wallpaper> GetAll()
        {
            using (var db = new WalltageDbContext())
            {
                return db.Wallpapers.ToList();
            }
        }

        public IEnumerable<Wallpaper> GetWallpapersMostViewed()
        {
            using (var db = new WalltageDbContext())
            {
                return db.Wallpapers.OrderByDescending(x => x.ViewCount).ToList();
            }
        }

        public IEnumerable<Wallpaper> GetWallpapersRandomly()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Wallpaper> GetWallpapersByCategoryId(int CategoryId)
        {
            using (var db = new WalltageDbContext())
            {
                return db.Wallpapers.Include("categories").Where(x => x.CategoryId == CategoryId).ToList();
            }
        }

        public IEnumerable<Wallpaper> GetWallpapersByResolutionId(int ResolutionId)
        {
            throw new NotImplementedException();
        }

        public void IncreaseToViewCount(int WallpaperId)
        {
            using (var db = new WalltageDbContext())
            {
                var getWallpaper = db.Wallpapers.Find(WallpaperId);
                getWallpaper.ViewCount = getWallpaper.ViewCount + 1;
                db.SaveChanges();
            }
        }

        

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
