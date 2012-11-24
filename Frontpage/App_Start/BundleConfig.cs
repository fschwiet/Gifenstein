using System.Web.Optimization;

namespace Frontpage.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var jsBundle = new ScriptBundle("~/bundles/js")
                .Include("~/Scripts/lib/jquery-{version}.js")
                .Include(
                    "~/Scripts/lib/jquery.ui.core.js",
                    "~/Scripts/lib/jquery.ui.widget.js",
                    "~/Scripts/lib/jquery.ui.mouse.js",
                    "~/Scripts/lib/jquery.ui.sortable.js")
                .IncludeDirectory("~/Scripts/frontpage", "*.js");

            bundles.Add(jsBundle);
        }
    }
}