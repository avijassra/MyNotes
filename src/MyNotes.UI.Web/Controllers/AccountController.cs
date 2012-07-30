namespace MyNotes.UI.Web.Controllers
{
    using System.Web.Mvc;
    using MyNotes.UI.Web.Setup.Helper;

    public partial class AccountController : MyNotesControllerBase
    {
        public virtual ActionResult Index()
        {
            return View();
        }
    }
}
