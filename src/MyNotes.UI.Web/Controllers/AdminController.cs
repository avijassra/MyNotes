namespace MyNotes.UI.Web.Controllers
{
    using System;
    using System.Web.Mvc;
    using System.Linq;
    using System.Collections.Generic;
    using AutoMapper;
    using Microsoft.Practices.Unity;
    using MyNotes.UI.Web.GroupServiceRef;
    using MyNotes.UI.Web.UserServiceRef;
    using MyNotes.UI.Web.AccountServiceRef;
    using MyNotes.UI.Web.Setup.ActionApi;
    using MyNotes.UI.Web.Setup.Helper;
    using MyNotes.UI.Web.Setup.Extensions;
    using MyNotes.UI.Web.Setup;
    using MyNotes.UI.Web.Setup.Attributes;
    using MyNotes.UI.Web.ViewModels.Admin.Group;
    using MyNotes.UI.Web.ViewModels.Admin.User;
    using MyNotes.UI.Web.ViewModels.Admin.Account;

    public partial class AdminController : MyNotesControllerBase
    {
        IServiceAction _serviceAction;
        IGroupService _groupService;
        IUserService _userService;
        IAccountService _accountService;

        public AdminController(IGroupService groupService, IUserService userService, IAccountService accountService)
        {
            _groupService = groupService;
            _userService = userService;
            _accountService = accountService;
            _serviceAction = WebDependencyBuilder.Container.Resolve<IServiceAction>(new ResolverOverride[]
                                   {
                                       new ParameterOverride("controller", this)
                                   });
        }

        [HttpGet]
        public virtual ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AdminAuthorizeFilter]
        public virtual ActionResult Groups()
        {
            return _serviceAction.Fetch()
                        .WithContent<IList<GroupDto>, IList<GroupViewModel>>(MVC.Admin.Views._groups,
                                () =>
                                {
                                    return _groupService.GetAllGroups();
                                })
                        .AsJsonResult();
        }

       

        [HttpGet]
        public virtual ActionResult Users()
        {
            return _serviceAction.Fetch()
                        .WithContent<IList<UserDto>, IList<UserViewModel>>(MVC.Admin.Views._users,
                                () =>
                                {
                                    var loggedInUser = Session.GetValue<LoggedUserInfoDto>(SessionKey.LoggedUser);
                                    return _userService.GetAllUsers(loggedInUser.GroupId, loggedInUser.IsSysAccount);
                                })
                        .AsJsonResult();
        }
        
        [HttpGet]
        public virtual ActionResult Accounts()
        {
            return _serviceAction.Fetch()
                        .WithContent<IList<AccountViewModel>>(MVC.Admin.Views._accounts,
                                () =>
                                {
                                    var loggedInUser = Session.GetValue<LoggedUserInfoDto>(SessionKey.LoggedUser);
                                    var accounts = _accountService.GetAllAccountsInGroup(loggedInUser.GroupId);
                                    return Mapper.Map<IList<AccountViewModel>>(accounts);
                                })
                        .AsJsonResult();
        }
    }
}
