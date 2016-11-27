using log4net;
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
    }
}