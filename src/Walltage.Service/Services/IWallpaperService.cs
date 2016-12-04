using System.Collections.Generic;
using Walltage.Domain.Entities;
using Walltage.Service.Models;

namespace Walltage.Service.Services
{
    public interface IWallpaperService
    {
        List<Wallpaper> GetSearchResult(string q);

        List<Wallpaper> GetLastUploads(int count);

        List<Wallpaper> GetHomePageUploads(int count);

        List<Wallpaper> TopImagesThisWeek(int count);

        List<Category> GetCategoryList();

        List<Resolution> GetResolutionList();

        DatabaseOperationResult WallpaperInsert(WallpaperViewModel model);
    }
}
