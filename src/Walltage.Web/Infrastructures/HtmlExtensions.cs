using System.IO;
using System.Web.Mvc;
using System.Web.UI;

namespace Walltage.Web.Infrastructures
{
    public static class HtmlExtensions
    {
        private const string SiteKey = "6LdQAw0UAAAAAHushNyFwhoxj6K_slutkHaj5nZ1";
        private const string SecretKey = "6LdQAw0UAAAAAJAmnlqQGQLPzQIUynOmwUpEkQVu";

        public static string GenerateCaptcha(this HtmlHelper helper)
        {
            var captchaControl = new Recaptcha.RecaptchaControl
            {
                ID = "recaptcha",
                Theme = "white",
                Language = "tr",
                PublicKey = SiteKey,
                PrivateKey = SecretKey
            };

            var htmlWriter = new HtmlTextWriter(new StringWriter());

            captchaControl.RenderControl(htmlWriter);

            return htmlWriter.InnerWriter.ToString();
        }
    }
}
