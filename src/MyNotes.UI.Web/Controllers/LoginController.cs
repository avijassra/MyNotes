namespace MyNotes.UI.Web.Controllers
{
    using System.Web.Mvc;
    using AutoMapper;
    using Microsoft.Practices.Unity;
    using MvcBase.WebHelper;
    using MyNotes.UI.Web.ViewModels.Admin.User;
    using MyNotes.UI.Web.UserServiceRef;
    using MyNotes.UI.Web.Setup;
    using MyNotes.UI.Web.Setup.ActionApi;
    using MyNotes.UI.Web.Setup.Helper;
    using MyNotes.UI.Web.Setup.Extensions;

    public partial class LoginController : Controller
    {
        IUserService _userService;
        IServiceAction _serviceAction;
        IServiceNewAction _serviceNewAction;

        public LoginController(IUserService userService)
        {
            _userService = userService;
            _serviceAction = WebDependencyBuilder.Container.Resolve<IServiceAction>(new ResolverOverride[]
                                   {
                                       new ParameterOverride("controller", this)
                                   });

            _serviceNewAction = WebDependencyBuilder.Container.Resolve<IServiceNewAction>(new ResolverOverride[]
                                   {
                                       new ParameterOverride("controller", this)
                                   });
        }

        public virtual ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public virtual ActionResult ValidateCredentials(UserCredentialViewModel viewmodel)
        {
            return _serviceAction.Fetch()
                        .WithResult<LoggedUserInfoDto>(() =>
                        {
                                var loggedUserInfo = _userService.Authenticate(viewmodel.Username, viewmodel.Password);
                                Session.SetValue(SessionKey.LoggedUser, loggedUserInfo);
                                return loggedUserInfo;
                            })
                        .AsJsonResult();
        }

        public virtual ActionResult LogOff(UserCredentialViewModel viewmodel)
        {
            Session.RemoveValue(SessionKey.LoggedUser);
            return RedirectToAction(MVC.Login.Actions.Index());
        }
    }
}
