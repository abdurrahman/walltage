using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walltage.Domain.Entities;

namespace Walltage.Domain.Repositories
{
    public interface ICRUD<T>
    {
        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        T FindById(int id);
        IEnumerable<T> GetAll();
    }

    public interface IWallpaperRepository : ICRUD<Wallpaper>
    {
        void IncreaseToViewCount(int WallpaperId);
        IEnumerable<Wallpaper> GetWallpapersMostViewed();
        IEnumerable<Wallpaper> GetWallpapersRandomly();
        IEnumerable<Wallpaper> GetWallpapersByCategoryId(int CategoryId);
        IEnumerable<Wallpaper> GetWallpapersByResolutionId(int ResolutionId);
        
    }

    public interface ICategoryRepository : ICRUD<Category>
    {
        
    }

    public interface IResolutionRepository : ICRUD<Resolution>
    {

    }

}
