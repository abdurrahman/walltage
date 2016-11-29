using log4net;
using System.Web.Mvc;
using Walltage.Service.Wrappers;
using Walltage.Web.Models.Common;

namespace Walltage.Web.Controllers
{
    public class CommonController : Controller
    {        
        private readonly ISessionWrapper _sessionWrapper;
        private readonly ICookieWrapper _cookieWrapper;
        private readonly ILog _logger;

        public CommonController(ILog logger,
            ISessionWrapper sessionWrapper,
            ICookieWrapper cookieWrapper)
        {
            _logger = logger;
            _cookieWrapper = cookieWrapper;
            _sessionWrapper = sessionWrapper;
        }

        [ChildActionOnly]
        public ActionResult HeaderLinks()
        {
            var model = new HeaderLinksModel();
            model.IsAuthenticated = false;
            if (!string.IsNullOrEmpty(_sessionWrapper.UserName))
            {
                model.IsAuthenticated = true;
                model.Username = _sessionWrapper.UserName;
            }
            return PartialView(model);
        }
    }
}