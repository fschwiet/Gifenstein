using System.Web.Optimization;

namespace Frontpage.BundleUtil
{
    public class NontestBundle : WrappingBundle
    {
        public NontestBundle(string virtualPath, ScriptBundle wrapped) : base(virtualPath, wrapped)
        {
        }

        public override bool IncludeFilename(string name)
        {
            return !name.ToLower().Contains(".test");
        }
    }
}