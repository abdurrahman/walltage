using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Walltage.Web.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Uploads(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Favorites(int id)
        {
            return View();
        }
    }
}