using System.Web.Optimization;
using Frontpage.BundleUtil;

namespace Frontpage.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var frontpageAll = new ScriptBundle("~/bundles/js");

            frontpageAll.Include("~/Scripts/lib/jquery-{version}.js")
                .Include(
                    "~/Scripts/lib/jquery.ui.core.js",
                    "~/Scripts/lib/jquery.ui.widget.js",
                    "~/Scripts/lib/jquery.ui.mouse.js",
                    "~/Scripts/lib/jquery.ui.sortable.js")
                .IncludeDirectory("~/Scripts/frontpage", "*.js");


            var frontpage = new NontestBundle("~/bundles/frontpage", frontpageAll);
            var frontpageTest = new TestBundle("~/bundles/frontpage-test", frontpageAll);

            bundles.Add(frontpage);
            bundles.Add(frontpageTest);
        }
    }
}