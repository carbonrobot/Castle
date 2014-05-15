using System.Web;
using System.Web.Optimization;

namespace Castle.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                    "~/Scripts/vendor/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                    "~/Scripts/vendor/require.js",
                    "~/Scripts/vendor/bootstrap/collapse.js",
                    "~/Scripts/vendor/bootstrap/tab.js",
                    "~/Scripts/vendor/bootstrap/transition.js",
                    "~/Scripts/vendor/angular/angular.js",
                    "~/Scripts/vendor/angular/angular-animate.js",
                    "~/Scripts/vendor/angular/angular-resource.js",
                    "~/Scripts/vendor/angular/angular-route.js",
                    "~/Scripts/vendor/angular/angular-sanitize.js", 
                    "~/Scripts/vendor/moment.js",
                    "~/Scripts/app/app.js",
                    "~/Scripts/app/services/*.js",
                    "~/Scripts/app/controllers/*.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                    "~/Content/css/bootstrap/bootstrap.css",
                    "~/Content/css/fa/font-awesome.css",
                    "~/Content/css/castle.css"));
        }
    }
}
