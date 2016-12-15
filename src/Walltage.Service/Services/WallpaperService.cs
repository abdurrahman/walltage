using log4net;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walltage.Domain;
using Walltage.Domain.Entities;
using Walltage.Service.Helpers;
using Walltage.Service.Models;
using Walltage.Service.Wrappers;

namespace Walltage.Service.Services
{
    public class WallpaperService : IWallpaperService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILog _logger;
        private readonly ISessionWrapper _sessionWrapper;
        private readonly IWebHelper _webHelper;

        public WallpaperService(ILog logger,
            IUnitOfWork unitOfWork,
            ISessionWrapper sessionWrapper,
            IWebHelper webHelper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _sessionWrapper = sessionWrapper;
            _webHelper = webHelper;
        }

        public List<Wallpaper> GetSearchResult(string q, string resolution)
        {
            //var query = _unitOfWork.WallpaperAndTagMappingRepository.Table()
            //    .Include(x => x.Wallpaper)
            //    .Include(x => x.Tag)
            //    .Where(x => x.Wallpaper.Name.Contains(q) || x.Tag.Name.Contains(q))
            //    .Select(x => x.Wallpaper);

            //var realQuery = _unitOfWork.WallpaperRepository.Table()
            //    .SelectMany(x => x.TagList.Where(t=> t.Tag.Name))

            var query = _unitOfWork.WallpaperRepository.Table()
                .Include(x => x.Resolution)
                .Include(x => x.TagList)
                .Where(x => x.Name.Contains(q) ||
                            x.Resolution.Name == resolution);

            var searchResult = query.ToList();

            var queryTag = _unitOfWork.TagRepository.Table()
                .Include(x => x.WallpaperList)
                .Where(x => x.Name.Contains(q))
                .Select(x => x.WallpaperList);

            var queryTagList = queryTag.ToList();
            //var query = _unitOfWork.WallpaperRepository.Table()
            //    .Include(x => x.TagList)
            //    .Where(x => x.Name.Contains(q))
            //    .SelectMany(x => x.TagList.Where(t => t.Tag.Name.Contains(q)))
                //.Select(x => x);
                //.Include(x => x.TagList);

            //var query = from c in _unitOfWork.WallpaperRepository.Table()
            //            orderby c.Id
            //            where c.Name.Contains(q) || c.ta
            //            select c;
            return searchResult;
        }

        public List<Domain.Entities.Wallpaper> GetLastUploads(int count)
        {
            var query = _unitOfWork.WallpaperRepository.Table()
                .OrderByDescending(x => x.AddedDate)
                .Take(count);

            var lastUploads = query.ToList();
            return lastUploads;
        }

        public List<Domain.Entities.Wallpaper> GetHomePageUploads(int count)
        {
            var query = _unitOfWork.WallpaperRepository.Table()
                .OrderByDescending(x => x.ViewCount)
                //.Where(x => x.AddedDate >= DateTime.Now.AddMonths(-2))
                .Take(count);

            var homePageUploads = query.ToList();
            return homePageUploads;
        }

        public List<Domain.Entities.Wallpaper> TopImagesThisWeek(int count)
        {
            var query = _unitOfWork.WallpaperRepository.Table()
                .OrderByDescending(x => x.ViewCount)
                .Where(x => x.AddedDate.Date >= DateTime.Now.AddDays(-7).Date)
                .Take(count);

            var topImagesThisWeek = query.ToList();
            return topImagesThisWeek;
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
            var allowedExtensions = new List<string> { ".jpg", ".jpeg", ".tiff", ".png" };
            if (!string.IsNullOrWhiteSpace(fileExtension))
                fileExtension = fileExtension.ToLowerInvariant();
            else
            {
                result.AddError("The file extension not provided. Please try again");
                return result;
            }

            if (!allowedExtensions.Contains(fileExtension))
            {
                result.AddError("The file extension not supported, allowed extension is \"*.jpg\", \"*.jpeg\", \"*.tiff\"");
                return result;
            }

            if (!System.IO.Directory.Exists(System.Web.Hosting.HostingEnvironment.MapPath("/Uploads")))
                System.IO.Directory.CreateDirectory(System.Web.Hosting.HostingEnvironment.MapPath("/Uploads"));

            var lastWallpaper = _unitOfWork.WallpaperRepository.Table().OrderByDescending(x => x.Id).FirstOrDefault();
            string fileName = lastWallpaper == null ? "walltage-1" : "walltage-" + (lastWallpaper.Id + 1);
            model.ImgPath = string.Format("{0}{1}", fileName, fileExtension);
            string filePath = System.IO.Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Uploads"), model.ImgPath);

            var tagList = PrepareTagList(model.Tags);

            model.Name = System.IO.Path.GetFileNameWithoutExtension(model.file.FileName);
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
                TagList = tagList
            });
            _unitOfWork.Save(true);

            if (!System.IO.File.Exists(filePath))
            {
                model.file.SaveAs(filePath);

                if (!System.IO.Directory.Exists(System.Web.Hosting.HostingEnvironment.MapPath("/Uploads/Thumbs")))
                    System.IO.Directory.CreateDirectory(System.Web.Hosting.HostingEnvironment.MapPath("/Uploads/Thumbs"));

                System.Drawing.Image image = System.Drawing.Image.FromFile(filePath);
                image = _webHelper.CreateThumbnail(image, new System.Drawing.Size(256, 250), true);
                image.Save(System.Web.Hosting.HostingEnvironment.MapPath("~/Uploads/Thumbs/") + fileName);
                image.Dispose();           
            }

            return result;
        }

        private List<WallpaperAndTagMapping> PrepareTagList(string tags)
        {
            if (string.IsNullOrEmpty(tags)) return null;

            var tagList = new List<WallpaperAndTagMapping>(5);
            foreach (var tag in tags.Split(','))
            {
                var tagName = tag.Trim();
                if (string.IsNullOrWhiteSpace(tagName))
                    continue;

                var isTagExist = _unitOfWork.TagRepository.Table()
                    .FirstOrDefault(x => x.Name == tag);

                if (isTagExist == null)
                {
                    _unitOfWork.TagRepository.Insert(new Tag
                    {
                        AddedBy = _sessionWrapper.UserName,
                        Name = tagName,
                        AddedDate = DateTime.Now,
                        UserId = _sessionWrapper.UserId,
                    });
                    _unitOfWork.Save();

                    var lastTag = _unitOfWork.TagRepository.Table().OrderByDescending(x => x.Id).FirstOrDefault();
                    tagList.Add(new WallpaperAndTagMapping { TagId = lastTag.Id });
                    continue;
                }
                tagList.Add(new WallpaperAndTagMapping { TagId = isTagExist.Id });
            }
            return tagList;
        }
    }
}
