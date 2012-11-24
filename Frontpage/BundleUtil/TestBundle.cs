using System.Web.Optimization;

namespace Frontpage.BundleUtil
{
    public class TestBundle : NontestBundle
    {
        public TestBundle(string virtualPath, ScriptBundle wrapped) : base(virtualPath, wrapped)
        {
        }

        public override bool IncludeFilename(string name)
        {
            return !base.IncludeFilename(name);
        }
    }
}