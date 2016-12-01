using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walltage.Domain;
using Walltage.Domain.Entities;

namespace Walltage.Service.Services
{
    public class WallpaperService : IWallpaperService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILog _logger;

        public WallpaperService(ILog logger,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

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


        public List<Category> GetCategoryList()
        {
            var query = _unitOfWork.CategoryRepository.Table();
            var categoryList = query.ToList();
            return categoryList;
        }

        public List<Resolution> GetResolutionList()
        {
            var query = _unitOfWork.ResolutionRepository.Table();
            var resolutionList = query.ToList();
            return resolutionList;
        }


        public void WallpaperInsert(Wallpaper entity)
        {
            _unitOfWork.WallpaperRepository.Insert(entity);
            _unitOfWork.Save();
        }
    }
}
