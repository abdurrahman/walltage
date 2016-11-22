using System.Security.Cryptography;
using System.Text;

namespace Walltage.Service.Helpers
{
    public class WebHelper : IWebHelper
    {
        public virtual string GetCurrentIpAddress()
        {
            const string url = "http://checkip.dyndns.org/";
            System.Net.WebRequest request = System.Net.WebRequest.Create(url);
            System.Net.WebResponse response = request.GetResponse();
            System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream());
            string result = sr.ReadToEnd().Trim();
            string[] beforeIp = result.Split(':');
            string getIp = beforeIp[1].Substring(1);
            string[] ip = getIp.Split('<');
            return ip[0];
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
    }
}
