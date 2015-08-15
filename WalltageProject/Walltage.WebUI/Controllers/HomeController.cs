using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Walltage.Domain.Entities;
using Walltage.Domain.Repositories;
using Walltage.WebUI.Models;

namespace Walltage.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IWallpaperRepository _wallpaperRepo;

        public HomeController(IWallpaperRepository wallpaperRepo)
        {
            _wallpaperRepo = wallpaperRepo;
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
            var result = _wallpaperRepo.GetAll().Where(x => x.Tags.Contains(q) || x.Name.Contains(q)).ToList();

            ViewBag.Count = result.Count();

            WallpaperViewModel model = new WallpaperViewModel
            {
                WallpaperList = result
            };

            return View(model);
        }


        public JsonResult GetWallpapers()
        { 
            var wallpaperList = _wallpaperRepo.GetAll().Take(12);
            return Json(wallpaperList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FindWallpaperById(string id)
        {
            int Id = Convert.ToInt32(id);
            var findWallpaperById = _wallpaperRepo.FindById(Id);
            return Json(findWallpaperById, JsonRequestBehavior.AllowGet);
        }

        // TODO: It will be detail page after testing..
        public ActionResult Wallpaper(int id)
        {
            var getWallpaper = _wallpaperRepo.FindById(id);

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
                WallpaperId = getWallpaper.WallpaperId
            };

            _wallpaperRepo.IncreaseToViewCount(getWallpaper.WallpaperId);

            return View(model);
        }

        public ActionResult Category(int id)
        {
            var getCategory = _wallpaperRepo.GetWallpapersByCategoryId(id);

            WallpaperViewModel model = new WallpaperViewModel
            {
                WallpaperList = getCategory
            };

            return View(model);
        }
    }
}
