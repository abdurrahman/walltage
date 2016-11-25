using System.Web;
using System.Web.Optimization;

namespace Walltage.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            string jQueryCdnPath = "//cdnjs.cloudflare.com/ajax/libs/jquery/1.11.3/jquery.min.js";

            bundles.Add(new ScriptBundle("~/bundles/jquery", jQueryCdnPath).Include(
                "~/assets/js/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/assets/js/jquery.validate*"));

            string angularCdnPath = "//ajax.googleapis.com/ajax/libs/angularjs/1.4.3/angular.min.js";

            bundles.Add(new ScriptBundle("~/bundles/angular", angularCdnPath).Include(
                "~/assets/js/angular.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularlib").Include(
                "~/assets/js/ui-bootstrap-tpls.js",
                "~/assets/js/angular-sanitize.js"));

            bundles.Add(new ScriptBundle("~/bundles/dropzonejs").Include(
                "~/assets/js/dropzone.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/assets/js/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/assets/js/bootstrap.js",
                "~/assets/js/jquery.lazyload.js",
                "~/assets/js/jquery.scrollstop.js",
                "~/assets/js/respond.js",
                "~/assets/js/walltage.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/assets/css/bootstrap.css",
                "~/assets/css/dropzone.css",
                "~/assets/css/basic.css",
                "~/assets/css/style.css"));

            BundleTable.Bundles.UseCdn = true;
            BundleTable.EnableOptimizations = true;
        }
    }
}