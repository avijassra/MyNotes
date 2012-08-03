namespace MyNotes.UI.Web.Controllers
{
    using System.Web.Mvc;
    using MyNotes.UI.Web.ViewModels.Admin.User;
    using MyNotes.UI.Web.UserServiceRef;
    using MvcBase.WebHelper.Mvc.Attributes;
    using AutoMapper;
    using MyNotes.UI.Web.Setup;
    using MyNotes.UI.Web.Setup.ActionApi;
    using Microsoft.Practices.Unity;
    using MyNotes.UI.Web.Setup.Helper;
    using MyNotes.UI.Web.Setup.Extensions;

    public partial class LoginController : Controller
    {
        IUserService _userService;
        IServiceAction _serviceAction;

        public LoginController(IServiceAction serviceAction, IUserService userService)
        {
            _userService = userService;
            _serviceAction = serviceAction;
        }

        public virtual ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public virtual ActionResult ValidateCredentials(UserCredentialViewModel viewmodel)
        {
            return _serviceAction.Fetch(this)
                        .WithResult<LoggedUserInfoDto>(() =>
                        {
                                var loggedUserInfo = _userService.Authenticate(viewmodel.Username, viewmodel.Password);
                                Session.SetValue(SessionKey.LoggedUser, loggedUserInfo);
                                return loggedUserInfo;
                            })
                        .Execute();
        }

        public virtual ActionResult LogOff(UserCredentialViewModel viewmodel)
        {
            Session.RemoveValue(SessionKey.LoggedUser);
            return RedirectToAction(MVC.Login.Actions.Index());
        }
    }
}
