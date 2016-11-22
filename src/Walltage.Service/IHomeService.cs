using System.Collections.Generic;
using Walltage.Domain.Entities;

namespace Walltage.Service
{
    public interface IHomeService
    {
        List<Wallpaper> GetSearchResult(string q);

        List<Wallpaper> GetLastUploads(int count);

        List<Wallpaper> GetHomePageUploads(int count);

        List<Wallpaper> TopImagesThisWeek(int count);

    }
}
