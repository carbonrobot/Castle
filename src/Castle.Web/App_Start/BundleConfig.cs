using System.Web;
using System.Web.Optimization;

namespace Castle.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                    "~/Scripts/vendor/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                    "~/Scripts/vendor/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                    "~/Scripts/vendor/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                    "~/Scripts/vendor/require.js",
                    "~/Scripts/vendor/angular.js",
                    "~/Scripts/vendor/angular-route.js",
                    "~/Scripts/vendor/angular-resource.js",
                    "~/Scripts/vendor/angular-animate.js",
                    "~/Scripts/vendor/angular-sanitize.js",
                    "~/Scripts/vendor/moment.js",
                    "~/Scripts/app/app.js",
                    "~/Scripts/app/services/*.js",
                    "~/Scripts/app/controllers/*.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                    "~/Content/css/bootstrap/bootstrap.css",
                    "~/Content/css/castle.css",
                    "~/Content/css/castle-loader.css"));
        }
    }
}
