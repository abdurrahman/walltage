using System.Web;
using System.Web.Optimization;

namespace Walltage.WebUI
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/assets/js/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/assets/js/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/assets/js/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/assets/js/bootstrap.js",
                "~/assets/js/respond.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/assets/css/normalize.css",
                "~/assets/css/bootstrap.css",
                "~/assets/css/style.css"));

            BundleTable.EnableOptimizations = true;
        }
    }
}