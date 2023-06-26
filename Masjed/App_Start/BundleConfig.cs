using System.Web;
using System.Web.Optimization;

namespace Masjed
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/myFarhangicss").Include(
     "~/Areas/Farhangi/assets/css/bootstrap-rtl.min.css",
    "~/Areas/Farhangi/assets/css/core.css",
    "~/Areas/Farhangi/assets/css/components.css",
     "~/Areas/Farhangi/assets/css/icons.css",
    "~/Areas/Farhangi/assets/css/pages.css",
     "~/Areas/Farhangi/assets/css/menu.css",
     "~/Areas/Farhangi/assets/css/responsive.css"

     ));
            bundles.Add(new ScriptBundle("~/Content/myFarhangijs").Include(
             "~/Areas/Farhangi/assets/js/jquery.min.js",
             "~/Areas/Farhangi/assets/js/bootstrap-rtl.min.js",
             "~/Areas/Farhangi/assets/js/detect.js",
             "~/Areas/Farhangi/assets/js/fastclick.js",
             "~/Areas/Farhangi/assets/js/jquery.slimscroll.js",
             "~/Areas/Farhangi/assets/js/jquery.blockUI.js",
             "~/Areas/Farhangi/assets/js/waves.js",
             "~/Areas/Farhangi/assets/js/jquery.nicescroll.js",
             "~/Areas/Farhangi/assets/js/jquery.scrollTo.min.js",
             "~/Areas/Farhangi/assets/js/jquery.core.js",
             "~/Areas/Farhangi/assets/js/jquery.app.js"

         ));
            bundles.Add(new ScriptBundle("~/Content/myFarhangiDatePersianjs").Include(
        "~/Areas/Farhangi/assets/MdBootstrapPersianDateTimePicker/jquery.Bootstrap-PersianDateTimePicker.js",
        "~/Areas/Farhangi/assets/MdBootstrapPersianDateTimePicker/jalaali.js"

        ));
            bundles.Add(new StyleBundle("~/Content/myFarhangiDatePersiancss").Include(
              "~/Areas/Farhangi/assets/MdBootstrapPersianDateTimePicker/jquery.Bootstrap-PersianDateTimePicker.css"

            ));
        }
    }
}
