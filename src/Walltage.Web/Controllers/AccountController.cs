using log4net;
using System.Web.Mvc;
using Walltage.Service;
using Walltage.Service.Models;
using Walltage.Service.Wrappers;
using Walltage.Web.Infrastructures;

namespace Walltage.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ISessionWrapper _sessionWrapper;
        private readonly ICookieWrapper _cookieWrapper;
        private readonly ILog _logger;

        public AccountController(ILog logger,
            IAccountService accountService,
            ISessionWrapper sessionWrapper,
            ICookieWrapper cookieWrapper)
        {
            _logger = logger;
            _accountService = accountService;
            _cookieWrapper = cookieWrapper;
            _sessionWrapper = sessionWrapper;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [CaptchaValidator]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model, bool captchaValid)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }
    }
}