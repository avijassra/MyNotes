namespace MyNotes.UI.Web.Controllers
{
    using System;
    using System.Web.Mvc;
    using System.Linq;
    using System.Collections.Generic;
    using AutoMapper;
    using Microsoft.Practices.Unity;
    using MyNotes.UI.Web.UserServiceRef;
    using MyNotes.UI.Web.AccountServiceRef;
    using MyNotes.UI.Web.Setup.ActionApi;
    using MyNotes.UI.Web.Setup.Helper;
    using MyNotes.UI.Web.Setup.Extensions;
    using MyNotes.UI.Web.Setup;
    using MyNotes.UI.Web.Setup.Attributes;
    using MyNotes.UI.Web.ViewModels.Admin.Account;

    public partial class AccountController : MyNotesControllerBase
    {
        IServiceAction _serviceAction;
        IUserService _userService;
        IAccountService _accountService;

        public AccountController(IUserService userService, IAccountService accountService)
        {
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
                        .Execute();
        }

        [HttpGet]
        public virtual ActionResult AddAccount()
        {
            return _serviceAction.Fetch()
                        .WithPopup<SaveAccountViewModel>(MVC.Account.Views._addAccount,
                                () =>
                                {
                                    var loggedInUser = Session.GetValue<LoggedUserInfoDto>(SessionKey.LoggedUser);
                                    var users = (from user in _userService.GetAllUsers(loggedInUser.GroupId, false)
                                                 select new SelectListItem { Value = user.Id.ToString(), Text = user.Nickname }).ToList();
                                    users.Insert(0, new SelectListItem { Value = "", Text = "Please Select ..." });

                                    ViewData["Users"] = users;
                                    return new SaveAccountViewModel();
                                })
                        .Execute();
        }

        [HttpPost]
        public virtual ActionResult AddAccount([Bind(Exclude = "Id")]SaveAccountViewModel accountViewModel)
        {
            return _serviceAction.Put()
                        .WithCommand(
                                () =>
                                {
                                    return _accountService.AddAccount(accountViewModel.Name, accountViewModel.UserId);
                                })
                        .Execute();
        }

        [HttpGet]
        public virtual ActionResult UpdateAccount(Guid id)
        {
            return _serviceAction.Fetch()
                        .WithResult<AccountDto, SaveAccountViewModel>(MVC.Account.Views._upgradeAccount,
                                () =>
                                {
                                    var loggedInUser = Session.GetValue<LoggedUserInfoDto>(SessionKey.LoggedUser);
                                    var users = (from user in _userService.GetAllUsers(loggedInUser.GroupId, false)
                                                 select new SelectListItem { Value = user.Id.ToString(), Text = user.Nickname }).ToList();
                                    users.Insert(0, new SelectListItem { Value = "", Text = "Please Select ..." });

                                    ViewData["Users"] = users;
                                    return _accountService.GetSingleAccount(id);
                                })
                        .Execute();
        }

        [HttpPost]
        public virtual ActionResult UpdateAccount(SaveAccountViewModel accountViewModel)
        {
            return _serviceAction.Put()
                        .WithCommand(
                                () =>
                                {
                                    return _accountService.UpdateAccount(accountViewModel.Id, accountViewModel.Name, accountViewModel.UserId);
                                })
                        .Execute();
        }

        [HttpDelete]
        public virtual ActionResult DeleteAccount(Guid id)
        {
            return _serviceAction.Fetch()
                        .WithResult<MyNotes.UI.Web.AccountServiceRef.MessageResultDto>(
                                () =>
                                {
                                    return _accountService.DeleteAccount(id);
                                })
                        .Execute();
        }
    }
}
