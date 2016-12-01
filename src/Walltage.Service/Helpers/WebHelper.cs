using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Walltage.Service.Helpers
{
    public class WebHelper : IWebHelper
    {
        private readonly HttpContextBase _httpContext;

        public WebHelper(HttpContextBase httpContext)
        {
            _httpContext = httpContext;
        }

        public virtual string GetCurrentIpAddress()
        {
            if (!IsRequestAvailable(_httpContext))
                return string.Empty;

            var result = "";
            if (_httpContext.Request.UserHostAddress != null)
            {
                result = _httpContext.Request.UserHostAddress;
            }

            if (result == "::1")
                result = "127.0.0.1";

            // remove port
            if (!string.IsNullOrEmpty(result))
            {
                int index = result.IndexOf(":", System.StringComparison.InvariantCultureIgnoreCase);
                if (index > 0)
                    result = result.Substring(0, index);
            }

            #region Old Way 
            //// http://stackoverflow.com/questions/3253701/get-public-external-ip-address/16109156#16109156
            //const string url = "http://checkip.dyndns.org/";
            //System.Net.WebRequest request = System.Net.WebRequest.Create(url);
            //System.Net.WebResponse response = request.GetResponse();
            //System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream());
            //string result = sr.ReadToEnd().Trim();
            //string[] beforeIp = result.Split(':');
            //string getIp = beforeIp[1].Substring(1);
            //string[] ip = getIp.Split('<');
            //return ip[0];
            #endregion

            return result;
        }


        public string EncryptToMd5(string text)
        {
            var md5 = MD5.Create();
            var dataMd5 = md5.ComputeHash(Encoding.UTF8.GetBytes(text));
            var sb = new StringBuilder();
            foreach (var t in dataMd5)
                sb.AppendFormat("{0:x2}", t);
            return sb.ToString();
        }

        protected virtual bool IsRequestAvailable(HttpContextBase httpContext)
        {
            if (httpContext == null)
                return false;

            try
            {
                if (httpContext.Request == null)
                    return false;
            }
            catch (HttpException)
            {
                return false;
            }
            return true;
        }
    }
}
