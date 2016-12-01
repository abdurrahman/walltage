using System;
using System.Linq;
using System.Web;

namespace Walltage.Service.Wrappers
{
    public class CookieWrapper : ICookieWrapper
    {
        private const string RememberMeCookieKey = "AuthEmail";
        private readonly HttpContextBase _context;

        public CookieWrapper(HttpContextBase context)
        {
            _context = context;
        }

        public string RememberMe
        {
            get{ return GetCookie(RememberMeCookieKey);}
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    RemoveCookie(RememberMeCookieKey);
                else
                    SetCookie(RememberMeCookieKey, value, DateTime.Now.AddDays(10));
            }
        }

        private string GetCookie(string name)
        {
            var value = string.Empty;
            if (_context.Request.Cookies.AllKeys.Contains(name))
            {
                value = _context.Request.Cookies[name].Value.ToString();
            }
            else
            {
                if (_context.Response.Cookies.AllKeys.Contains(name))
                {
                    value = _context.Response.Cookies[name].Value.ToString();
                }
            }
            return value;
        }

        private void SetCookie(string name, string value, DateTime? expires = null)
        {
            var cookie = new HttpCookie(name, value);
            if (expires.HasValue)
                cookie.Expires = expires.Value;
            _context.Response.Cookies.Add(cookie);
        }

        private void RemoveCookie(string name)
        {
            var cookie = new HttpCookie(name) { Expires = DateTime.Now.AddYears(-10) };
            _context.Response.Cookies.Add(cookie);
        }
    }
}
