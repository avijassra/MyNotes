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
                        .Execute();
        }

        [HttpGet]
        public virtual ActionResult AddGroup()
        {
            return _serviceAction.Fetch()
                        .WithPopup<SaveGroupViewModel>(MVC.Admin.Views._addGroup,
                                () =>
                                {
                                    return new SaveGroupViewModel();
                                })
                        .Execute();
        }

        [HttpPost]
        public virtual ActionResult AddGroup([Bind(Exclude="Id")]SaveGroupViewModel groupViewModel)
        {
            return _serviceAction.Put()
                        .WithCommand(
                                () =>
                                {
                                    return _groupService.AddGroup(groupViewModel.Name);
                                })
                        .Execute();
        }

        [HttpGet]
        public virtual ActionResult UpdateGroup(Guid id)
        {
            return _serviceAction.Fetch()
                        .WithResult<GroupDto, SaveGroupViewModel>(MVC.Admin.Views._upgradeGroup,
                                () =>
                                {
                                    return _groupService.GetSingleGroup(id);
                                })
                        .Execute();
        }

        [HttpPost]
        public virtual ActionResult UpdateGroup(SaveGroupViewModel groupViewModel)
        {
            return _serviceAction.Put()
                        .WithCommand(
                                () =>
                                {
                                    return _groupService.UpdateGroup(groupViewModel.Id, groupViewModel.Name);
                                })
                        .Execute();
        }

        [HttpDelete]
        public virtual ActionResult DeleteGroup(Guid id)
        {
            return _serviceAction.Fetch()
                        .WithResult<MyNotes.UI.Web.GroupServiceRef.MessageResultDto>(
                                () =>
                                {
                                    return _groupService.DeleteGroup(id);
                                })
                        .Execute();
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
                        .Execute();
        }
        
        [HttpGet]
        public virtual ActionResult AddUser()
        {
            return _serviceAction.Fetch()
                        .WithPopup<SaveUserViewModel>(MVC.Admin.Views._addUser,
                                () =>
                                {
                                    var groups = (from gp in _groupService.GetAllGroups()
                                                    where !gp.IsSysAccount
                                                    select new SelectListItem { Value = gp.Id.ToString(), Text = gp.Name }).ToList();
                                    groups.Insert(0, new SelectListItem { Value = "", Text = "Please Select ..." });

                                    ViewData["Groups"] = groups;
                                    return new SaveUserViewModel();
                                })
                        .Execute();
        }

        [HttpPost]
        public virtual ActionResult AddUser([Bind(Exclude="Id")]SaveUserViewModel userViewModel)
        {
            return _serviceAction.Put()
                        .WithCommand(
                                () =>
                                {
                                    return _userService.AddUser(userViewModel.Firstname, userViewModel.Lastname, userViewModel.Nickname,
                                        userViewModel.Username, userViewModel.GroupId);
                                })
                        .Execute();
        }

        [HttpGet]
        public virtual ActionResult UpdateUser(Guid id)
        {
            return _serviceAction.Fetch()
                        .WithResult<UserDto, SaveUserViewModel>(MVC.Admin.Views._upgradeUser,
                                () =>
                                {
                                    var groups = (from gp in _groupService.GetAllGroups()
                                                  where !gp.IsSysAccount
                                                  select new SelectListItem { Value = gp.Id.ToString(), Text = gp.Name }).ToList();
                                    groups.Insert(0, new SelectListItem { Value = "", Text = "Please Select ..." });

                                    ViewData["Groups"] = groups;
                                    return _userService.GetSingleUser(id);
                                })
                        .Execute();
        }

        [HttpPost]
        public virtual ActionResult UpdateUser([Bind(Exclude="Password, ConfirmPassword")]SaveUserViewModel userViewModel)
        {
            return _serviceAction.Put()
                        .WithCommand(
                                () =>
                                {
                                    return _userService.UpdateUser(userViewModel.Id, userViewModel.Firstname, userViewModel.Lastname, 
                                        userViewModel.Nickname, userViewModel.Username, userViewModel.GroupId);
                                })
                        .Execute();
        }

        [HttpDelete]
        public virtual ActionResult DeleteUser(Guid id)
        {
            return _serviceAction.Fetch()
                        .WithResult<MyNotes.UI.Web.UserServiceRef.MessageResultDto>(
                                () =>
                                {
                                    return _userService.DeleteUser(id);
                                })
                        .Execute();
        }

        [HttpPut]
        public virtual ActionResult ResetPassword(Guid id)
        {
            return _serviceAction.Put()
                        .WithCommand(
                                () =>
                                {
                                    return _userService.ResetPassword(id);
                                })
                        .Execute();
        }

        [HttpPut]
        public virtual ActionResult UserLockStatus(Guid id, bool isLocked)
        {
            return _serviceAction.Put()
                        .WithCommand(
                                () =>
                                {
                                    return _userService.UserLockStatus(id, isLocked);
                                })
                        .Execute();
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
                        .WithPopup<SaveAccountViewModel>(MVC.Admin.Views._addAccount,
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
                        .WithResult<AccountDto, SaveAccountViewModel>(MVC.Admin.Views._upgradeAccount,
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
