namespace MvcBase.WebHelper
{
    using System;
    using Microsoft.Web.Optimization;
    using Yahoo.Yui.Compressor;

    public class YuiJsMinify : IBundleTransform
    {
        public void Process(BundleResponse bundle)
        {
            if (bundle == null)
            {
                throw new ArgumentNullException("bundle");
            }

#if DEBUG
            bundle.Content = bundle.Content;
#else
            bundle.Content = JavaScriptCompressor.Compress(bundle.Content);
#endif
            bundle.ContentType = "text/javascript";
        }
    }
}
