using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walltage.Domain.Entities;

namespace Walltage.Service
{
    public class HomeService : IHomeService
    {
        public List<Wallpaper> GetSearchResult(string q)
        {
            throw new NotImplementedException();
        }

        public List<Domain.Entities.Wallpaper> GetLastUploads(int count)
        {
            throw new NotImplementedException();
        }

        public List<Domain.Entities.Wallpaper> GetHomePageUploads(int count)
        {
            throw new NotImplementedException();
        }

        public List<Domain.Entities.Wallpaper> TopImagesThisWeek(int count)
        {
            throw new NotImplementedException();
        }
    }
}
