using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walltage.Domain;
using Walltage.Domain.Entities;
using Walltage.Service.Models;
using Walltage.Service.Wrappers;

namespace Walltage.Service.Services
{
    public class WallpaperService : IWallpaperService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILog _logger;
        private readonly ISessionWrapper _sessionWrapper;

        public WallpaperService(ILog logger,
            IUnitOfWork unitOfWork,
            ISessionWrapper sessionWrapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _sessionWrapper = sessionWrapper;
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


        public DatabaseOperationResult WallpaperInsert(WallpaperViewModel model)
        {
            var result = new DatabaseOperationResult();
            if (model.file == null && model.file.ContentLength == 0)
            {
                result.AddError("File not found!");
                return result;
            }

            if (((model.file.ContentLength / 1024) / 1024) > 2)
            {
                result.AddError("File must be less than 2 MB");
                return result;
            }

            System.Drawing.Image resolution = System.Drawing.Image.FromStream(model.file.InputStream);
            if (resolution.Width < 1024 || resolution.Height < 768)
            {
                result.AddError("The file must be greater than 1024 pixels wide and greater than to 768 pixels tall at least.");
                return result;
            }

            var fileExtension = System.IO.Path.GetExtension(model.file.FileName);
            var allowedExtensions = new List<string> { ".jpg", ".jpeg", ".tiff" };
            if (!string.IsNullOrWhiteSpace(fileExtension))
                fileExtension = fileExtension.ToLowerInvariant();
            // Todo: Check else situation
            if (!allowedExtensions.Contains(fileExtension))
            {
                result.AddError("The file extension not supported, allowed extension is \"*.jpg\", \"*.jpeg\", \"*.tiff\"");
                return result;
            }
            
            if (!System.IO.Directory.Exists(System.Web.Hosting.HostingEnvironment.MapPath("/App_Data/Uploads")))
                System.IO.Directory.CreateDirectory(System.Web.Hosting.HostingEnvironment.MapPath("/App_Data/Uploads"));
            
            model.ImgPath = string.Format("walltage-{0}{1}", model.file.FileName, System.IO.Path.GetExtension(model.file.FileName));
            string path = System.IO.Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Uploads"), model.ImgPath);

            
            //var tagList = new List<Tag>(5);
            //if (!string.IsNullOrEmpty(model.Tags))
            //{
            //    foreach (var tag in model.Tags.Split(','))
            //    {
            //        tagList.Add(new Tag {
            //         AddedBy = _sessionWrapper.UserName,
            //         Name = tag,
                     
            //        })
            //    }
		 
            //}

            _unitOfWork.WallpaperRepository.Insert(new Domain.Entities.Wallpaper
            {
                AddedBy = _sessionWrapper.UserName,
                AddedDate = DateTime.Now,
                CategoryId = model.CategoryId,
                ImgPath = model.ImgPath,
                Name = model.Name,
                ResolutionId = model.ResolutionId,
                Size = model.file.ContentLength,
                UploaderId = _sessionWrapper.UserId,
            });
            _unitOfWork.Save();
            return result;
        }
    }
}
