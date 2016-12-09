using System;
using System.Drawing;
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


        public System.Drawing.Image CreateThumbnail(Image image, System.Drawing.Size thumbnailSize, bool needToFill)
        {
            #region много арифметики
            int sourceWidth = image.Width;
            int sourceHeight = image.Height;
            int sourceX = 0;
            int sourceY = 0;
            double destX = 0;
            double destY = 0;

            double nScale = 0;
            double nScaleW = 0;
            double nScaleH = 0;

            nScaleW = ((double)thumbnailSize.Width / (double)sourceWidth);
            nScaleH = ((double)thumbnailSize.Height / (double)sourceHeight);
            if (!needToFill)
            {
                nScale = Math.Min(nScaleH, nScaleW);
            }
            else
            {
                nScale = Math.Max(nScaleH, nScaleW);
                destY = (thumbnailSize.Height - sourceHeight * nScale) / 2;
                destX = (thumbnailSize.Width - sourceWidth * nScale) / 2;
            }

            if (nScale > 1)
                nScale = 1;

            int destWidth = (int)Math.Round(sourceWidth * nScale);
            int destHeight = (int)Math.Round(sourceHeight * nScale);
            #endregion

            Image thumbnail = null;
            try
            {
                thumbnail = new System.Drawing.Bitmap(destWidth + (int)Math.Round(2 * destX), destHeight + (int)Math.Round(2 * destY));
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("destWidth:{0}, destX:{1}, destHeight:{2}, desxtY:{3}, Width:{4}, Height:{5}",
                    destWidth, destX, destHeight, destY, thumbnailSize.Width, thumbnailSize.Height), ex);
            }
            using (System.Drawing.Graphics grPhoto = System.Drawing.Graphics.FromImage(thumbnail))
            {
                grPhoto.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                grPhoto.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                grPhoto.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                Rectangle to = new System.Drawing.Rectangle((int)Math.Round(destX), (int)Math.Round(destY), destWidth, destHeight);
                Rectangle from = new System.Drawing.Rectangle(sourceX, sourceY, sourceWidth, sourceHeight);
                grPhoto.DrawImage(image, to, from, System.Drawing.GraphicsUnit.Pixel);
            }

            return thumbnail;
        }

        public static string TimeAgo(DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;
            if (span.Days > 365)
            {
                int years = (span.Days / 365);
                if (span.Days % 365 != 0)
                    years += 1;
                return String.Format("{0} {1} ago", years, years == 1 ? "year" : "years");
            }
            if (span.Days > 30)
            {
                int months = (span.Days / 30);
                if (span.Days % 31 != 0)
                    months += 1;
                return String.Format("{0} {1} ago", months, months == 1 ? "month" : "months");
            }
            if (span.Days > 0)
                return String.Format("{0} {1} ago", span.Days, span.Days == 1 ? "day" : "days");
            if (span.Hours > 0)
                return String.Format("{0} {1} ago", span.Hours, span.Hours == 1 ? "hour" : "hours");
            if (span.Minutes > 0)
                return String.Format("{0} {1} ago", span.Minutes, span.Minutes == 1 ? "minute" : "minutes");
            if (span.Seconds > 5)
                return String.Format("{0} seconds ago", span.Seconds);
            if (span.Seconds <= 5)
                return "just now";
            return string.Empty;
        }
    }
}
