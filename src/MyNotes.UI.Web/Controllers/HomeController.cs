namespace MyNotes.UI.Web.Controllers
{
    using System.Web.Mvc;
    using MyNotes.UI.Web.EchoServiceRef;
    using Microsoft.Practices.Unity;
    using MyNotes.UI.Web.Setup.Helper;
    using MyNotes.UI.Web.ViewModels.Shared;
    using MyNotes.UI.Web.Setup;
    using MyNotes.UI.Web.Setup.ActionApi;

    public partial class HomeController : MyNotesControllerBase
    {
        [Dependency]
        public IEchoService EchoService { get; set; }

        IServiceAction _serviceAction;
        IServiceNewAction _serviceNewAction;

        public HomeController()
        {
            _serviceNewAction = WebDependencyBuilder.Container.Resolve<IServiceNewAction>(new ResolverOverride[]
                                   {
                                       new ParameterOverride("controller", this)
                                   });
        }

        public virtual ActionResult Index()
        {
            ViewBag.Value = EchoService.Ping();

            return View();
        }

        [HttpGet]
        public virtual ActionResult TestLink()
        {
            var a = 10;
            var b = 15;
            return _serviceNewAction.Put()
                        .WithCommand<int>(() => a + b)
                        .AsJsonResult();
        }
    }
}
