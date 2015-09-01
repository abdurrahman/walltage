using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Walltage.Domain.Entities;
using Walltage.Domain.Repositories;
using Walltage.WebUI.Infrastructures;
using Walltage.WebUI.Models;

namespace Walltage.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IWallpaperRepository _wallpaperRepo;
        private ICategoryRepository _categoryRepo;
        private IResolutionRepository _resolutionRepo;

        
        public HomeController(IWallpaperRepository wallpaperRepo, ICategoryRepository categoryRepo, IResolutionRepository resolutionRepo)
        {
            _wallpaperRepo = wallpaperRepo;
            _categoryRepo = categoryRepo;
            _resolutionRepo = resolutionRepo;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ViewResult Rules()
        {
            return View();
        }

        public ViewResult Faq()
        {
            return View();
        }

        public ViewResult Random()
        {
            return View();
        }


        public ViewResult MostViewed()
        {
            WallpaperViewModel model = new WallpaperViewModel
            {
                WallpaperList = _wallpaperRepo.GetWallpapersMostViewed()
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Search(string q)
        {
            var result = _wallpaperRepo.GetAll().Where(x => x.Tags.Contains(q.ToLower()) || x.Name.Contains(q.ToLower())).ToList();

            ViewBag.Count = result.Count();

            WallpaperViewModel model = new WallpaperViewModel
            {
                WallpaperList = result
            };

            return View(model);
        }

        //public JsonResult Search(string q)
        //{
        //    var result = _wallpaperRepo.GetAll().Where(x => x.Tags.Contains(q.ToLower()) || x.Name.Contains(q.ToLower())).ToList();

        //    ViewBag.Count = result.Count();

        //    WallpaperViewModel model = new WallpaperViewModel
        //    {
        //        WallpaperList = result
        //    };

        //    return Json(model);
        //}

        #region JsonResults for Angular queries


        public JsonResult GetWallpapers()
        {
            return Json( _wallpaperRepo.GetAll(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult FindWallpaperById(string id)
        {
            int Id = Convert.ToInt32(id);
            var findWallpaperById = _wallpaperRepo.FindById(Id);
            return Json(findWallpaperById, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCategories()
        {
            return Json(_categoryRepo.GetAll(), JsonRequestBehavior.AllowGet);
        }

        #endregion

        // TODO: It will be detail page after testing..
        public ActionResult Wallpaper(int id)
        {
            var getWallpaper = _wallpaperRepo.FindById(id);
           // var name =_usermanager.Users.Where(x => x.Id == getWallpaper.UploaderId).Select(x => x.UserName);

            //ViewBag.Username = name;
            WallpaperViewModel model = new WallpaperViewModel
            {
                Name = getWallpaper.Name,
                CategoryId = getWallpaper.CategoryId,
                ImgPath = getWallpaper.ImgPath,
                ResolutionId = getWallpaper.ResolutionId,
                Size = getWallpaper.Size,
                Tags = getWallpaper.Tags,
                UploaderId = getWallpaper.UploaderId,
                UploadDate = getWallpaper.UploadDate,
                ViewCount = getWallpaper.ViewCount,
                WallpaperId = getWallpaper.WallpaperId,
                Category = getWallpaper.categories,
                Resolution = getWallpaper.resolutions,                 
                //Category = _categoryRepo.FindById(getWallpaper.CategoryId),
                //Resolution = _resolutionRepo.FindById(getWallpaper.ResolutionId)
            };

            _wallpaperRepo.IncreaseToViewCount(getWallpaper.WallpaperId);

            return View(model);
        }

        public ActionResult Category(int id)
        {
            var getCategory = _wallpaperRepo.GetWallpapersByCategoryId(id);

            WallpaperViewModel model = new WallpaperViewModel
            {
                WallpaperList = getCategory,
                Category = _categoryRepo.FindById(id)
            };
            return View(model);
        }
    }
}
