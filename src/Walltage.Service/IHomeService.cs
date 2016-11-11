using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
