using log4net;
using System;
using System.Web.Mvc;
using Walltage.Service;
using Walltage.Service.Models;

namespace Walltage.Web.Controllers
{
    public class HomeController : Controller
    {
        // ToDo : Check AntiValidate attribute

        private readonly ILog _logger;
        private readonly IWallpaperService _wallpaperService;

        public HomeController(ILog logger,
            IWallpaperService wallpaperService)
        {
            _logger = logger;
            _wallpaperService = wallpaperService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(object s)
        {
            return View(s);
        }

        public ViewResult Faq()
        {
            return View();
        }

        public ActionResult Rules()
        {
            return View();
        }

        public ActionResult Stats()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(object s)
        {
            return View(s);
        }

        public ActionResult Category(int id)
        {
            return View();
        }

        public ActionResult Wallpaper(int id)
        {

            return View();
        }

        public ActionResult Upload()
        {
            var categories = _wallpaperService.GetCategoryList();
            var resolutions = _wallpaperService.GetResolutionList();

            var model = new WallpaperViewModel
            {
                CategoryList = categories,
                ResolutionList = resolutions
            };
            return View(model);  
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(WallpaperViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            
            if (model.file != null && model.file.ContentLength > 0)
            {
                if(((model.file.ContentLength / 1024) /1024) > 2)
                {
                    ViewBag.Message = "File must be less than 2 MB";
                    return View(model);
                }

                System.Drawing.Image resolution = System.Drawing.Image.FromStream(model.file.InputStream);
                if (resolution.Width < 1024 || resolution.Height < 768)
                {
                    ViewBag.Message = "The file must be greater than 1024 pixels wide and greater than to 768 pixels tall at least.";
                    return View(model);
                }

                if (!System.IO.Directory.Exists(Server.MapPath("/App_Data/Uploads")))
                    System.IO.Directory.CreateDirectory(Server.MapPath("/App_Data/Uploads"));
                
                model.ImgPath = string.Format("walltage-{0}", System.IO.Path.GetExtension(model.file.FileName));
                string path = System.IO.Path.Combine(Server.MapPath("~/App_Data/Uploads"), model.ImgPath);

                _wallpaperService.WallpaperInsert(new Domain.Entities.Wallpaper
                    {
                        AddedBy = "abdurrahman",
                        AddedDate = DateTime.Now,
                        CategoryId = model.CategoryId,
                        ImgPath = model.ImgPath,
                        Name = model.Name,
                        ResolutionId = model.ResolutionId,
                        Size = model.file.ContentLength,
                        UploaderId = 1
                    });
                return View();

            }

            return View(model);
        }
    }
}