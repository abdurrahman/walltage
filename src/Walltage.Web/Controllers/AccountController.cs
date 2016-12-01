using log4net;
using System.Web.Mvc;
using Walltage.Service;
using Walltage.Service.Models;
using Walltage.Service.Services;
using Walltage.Service.Wrappers;
using Walltage.Web.Infrastructures;

namespace Walltage.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISessionWrapper _sessionWrapper;
        private readonly ICookieWrapper _cookieWrapper;
        private readonly ILog _logger;

        public AccountController(ILog logger,
            IUserService userService,
            ISessionWrapper sessionWrapper,
            ICookieWrapper cookieWrapper)
        {
            _logger = logger;
            _userService = userService;
            _cookieWrapper = cookieWrapper;
            _sessionWrapper = sessionWrapper;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            var model = new LoginViewModel();
            if (!string.IsNullOrEmpty(_cookieWrapper.RememberMe))
                model.Username = _cookieWrapper.RememberMe;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                model.Username = model.Username.Trim();
                var user = _userService.ValidateUser(model.Username, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Username or password is wrong, try again !");
                    return View(model);
                }
                if (model.RememberMe)
                    _cookieWrapper.RememberMe = model.Username;

                _sessionWrapper.StartSession(user.Id, user.Username, user.Email);

                if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
                    return RedirectToAction("Index", "Home");

                return Redirect(returnUrl);
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            _sessionWrapper.EndSession();
            return RedirectToAction("Index", "Home");
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
            return View(model);
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }
    }
}