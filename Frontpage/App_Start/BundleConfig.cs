using System.Web.Optimization;

namespace Frontpage.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var jsBundle = new ScriptBundle("~/bundles/js")
                .Include("~/Scripts/jquery-{version}.js")
                .Include(
                    "~/Scripts/jquery.ui.core.js",
                    "~/Scripts/jquery.ui.widget.js",
                    "~/Scripts/jquery.ui.mouse.js", 
                    "~/Scripts/jquery.ui.sortable.js");

            bundles.Add(jsBundle);
        }
    }
}