using System.Web.Optimization;

namespace Frontpage.BundleUtil
{
    public class WrappingBundle : ScriptBundle
    {
        private readonly ScriptBundle _wrapped;

        public WrappingBundle(string virtualPath, ScriptBundle wrapped) : base(virtualPath)
        {
            _wrapped = wrapped;
        }

        public virtual bool IncludeFilename(string name)
        {
            return true;
        }

        public override System.Collections.Generic.IEnumerable<System.IO.FileInfo> EnumerateFiles(BundleContext context)
        {
            foreach (var file in _wrapped.EnumerateFiles(context))
            {
                if (IncludeFilename(file.Name))
                    yield return file;
            }

            foreach (var file in base.EnumerateFiles(context))
            {
                yield return file;
            }
        }
    }
}