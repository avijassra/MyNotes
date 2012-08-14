namespace MyNotes.UI.Web.Setup.StartupTasks.Component.MinifyBundles
{
    using MvcBase.WebHelper;
    using Microsoft.Web.Optimization;

    public class JsBundles : IIncludeComponent
    {
        public void Setup()
        {
            var mainJsBundle = new Bundle("~/Include/Cache/main.jsbundle.script", typeof(YuiJsMinify));
            mainJsBundle.AddFile("~/Include/Scripts/jQuery/jquery-ui-1.8.11.min.js");
            mainJsBundle.AddFile("~/Include/Scripts/jQuery/jquery.metadata.js");
            mainJsBundle.AddFile("~/Include/Scripts/jQuery/jquery.blockUI.js");
            //mainJsBundle.AddFile("~/Include/Scripts/jQuery/jquery.ba-bbq.min.js");
            mainJsBundle.AddFile("~/Include/Scripts/jQuery/jquery.alerts.js");
            mainJsBundle.AddFile("~/Include/Scripts/jQuery/jsrender.js");
            mainJsBundle.AddFile("~/Include/Scripts/jQuery/jquery.validate.modified.js");
            mainJsBundle.AddFile("~/Include/Scripts/jQuery/jquery.validate.unobtrusive.modified.js");
            mainJsBundle.AddFile("~/Include/Scripts/misc/knockout-2.0.0.js");
            mainJsBundle.AddFile("~/Include/Scripts/misc/modernizr-2.5.3.js");
            mainJsBundle.AddFile("~/Include/Scripts/misc/bootstrap.min.js");
            mainJsBundle.AddFile("~/Include/Scripts/base/core.shared.js");
            mainJsBundle.AddFile("~/Include/Scripts/base/core.handler.js");
            mainJsBundle.AddFile("~/Include/Scripts/base/core.ajax.js");
            mainJsBundle.AddFile("~/Include/Scripts/base/core.address.js");
            mainJsBundle.AddFile("~/Include/Scripts/base/core.forms.js");
            mainJsBundle.AddFile("~/Include/Scripts/app/handler.common.js");
            BundleTable.Bundles.Add(mainJsBundle);

            var login = new Bundle("~/Include/Cache/login.script", typeof(YuiJsMinify));
            login.AddFile("~/Include/Scripts/app/login.js");
            BundleTable.Bundles.Add(login);

            var admin = new Bundle("~/Include/Cache/admin.script", typeof(YuiJsMinify));
            admin.AddFile("~/Include/Scripts/app/handler.admin.js");
            BundleTable.Bundles.Add(admin);
        }
    }
}