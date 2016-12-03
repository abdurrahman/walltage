using System.Web.Mvc;
using System.Web.Routing;
using Walltage.Service.Wrappers;

namespace Walltage.Web.Infrastructures
{
    public class UserAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        public ISessionWrapper SessionWrapper { get; set; }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) ||
                filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                // Dont check for authorization as AllowAnonymous filter is applied to the action or controller
                return;
            }

            // Check for authorization
            if (string.IsNullOrEmpty(SessionWrapper.UserName))
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        {"Controller", "Account"},
                        {"Action", "Login"}
                    });
            }
        }
    }
}