namespace MyNotes.UI.Web.Controllers
{
    using System.Web.Mvc;
    using MyNotes.UI.Web.EchoServiceRef;
    using Microsoft.Practices.Unity;
    using MyNotes.UI.Web.Setup.Helper;
    using MyNotes.UI.Web.ViewModels.Shared;

    public partial class HomeController : MyNotesControllerBase
    {
        [Dependency]
        public IEchoService EchoService { get; set; }

        public virtual ActionResult Index()
        {
            ViewBag.Value = EchoService.Ping();

            return View();
        }
    }
}
