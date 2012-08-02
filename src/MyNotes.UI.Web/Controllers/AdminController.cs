namespace MyNotes.UI.Web.Controllers
{
    using System;
    using System.Web.Mvc;
    using MyNotes.UI.Web.GroupServiceRef;
    using MyNotes.UI.Web.UserServiceRef;
    using MyNotes.UI.Web.AccountServiceRef;
    using MyNotes.UI.Web.Setup.ActionApi;
    using MyNotes.UI.Web.Setup.Helper;
    using System.Collections.Generic;
    using AutoMapper;
    using MyNotes.UI.Web.Setup.Extensions;
    using MyNotes.UI.Web.ViewModels.Admin.Group;
    using MyNotes.UI.Web.ViewModels.Admin.User;
    using System.Linq;
    using MyNotes.UI.Web.Setup.Attributes;
    using MyNotes.UI.Web.ViewModels.Admin.Account;

    public partial class AdminController : MyNotesControllerBase
    {
        IServiceAction _serviceAction;
        IGroupService _groupService;
        IUserService _userService;
        IAccountService _accountService;

        public AdminController(IServiceAction serviceAction, IGroupService groupService, IUserService userService, IAccountService accountService)
        {
            _serviceAction = serviceAction;
            _groupService = groupService;
            _userService = userService;
            _accountService = accountService;
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
            return _serviceAction.Fetch(this)
                        .WithContent<IList<GroupViewModel>>(MVC.Admin.Views._groups,
                                () =>
                                {
                                    var groups = _groupService.GetAllGroups();
                                    return Mapper.Map<IList<GroupViewModel>>(groups);
                                })
                        .Execute();
        }

        [HttpGet]
        public virtual ActionResult AddGroup()
        {
            return _serviceAction.Fetch(this)
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
            return _serviceAction.Put(this)
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
            return _serviceAction.Fetch(this)
                        .WithResult<SaveGroupViewModel>(MVC.Admin.Views._upgradeGroup,
                                () =>
                                {
                                    var group = _groupService.GetSingleGroup(id);
                                    return Mapper.Map<SaveGroupViewModel>(group);
                                })
                        .Execute();
        }

        [HttpPost]
        public virtual ActionResult UpdateGroup(SaveGroupViewModel groupViewModel)
        {
            return _serviceAction.Put(this)
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
            return _serviceAction.Fetch(this)
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
            return _serviceAction.Fetch(this)
                        .WithContent<IList<UserViewModel>>(MVC.Admin.Views._users,
                                () =>
                                {
                                    var loggedInUser = Session.GetValue<LoggedUserInfoDto>(SessionKey.LoggedUser);
                                    var users = _userService.GetAllUsers(loggedInUser.GroupId, loggedInUser.IsSysAccount);
                                    return Mapper.Map<IList<UserViewModel>>(users);
                                })
                        .Execute();
        }
        
        [HttpGet]
        public virtual ActionResult AddUser()
        {
            return _serviceAction.Fetch(this)
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
            return _serviceAction.Put(this)
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
            return _serviceAction.Fetch(this)
                        .WithResult<SaveUserViewModel>(MVC.Admin.Views._upgradeUser,
                                () =>
                                {
                                    var groups = (from gp in _groupService.GetAllGroups()
                                                  where !gp.IsSysAccount
                                                  select new SelectListItem { Value = gp.Id.ToString(), Text = gp.Name }).ToList();
                                    groups.Insert(0, new SelectListItem { Value = "", Text = "Please Select ..." });

                                    ViewData["Groups"] = groups;
                                    var user = _userService.GetSingleUser(id);
                                    return Mapper.Map<SaveUserViewModel>(user);
                                })
                        .Execute();
        }

        [HttpPost]
        public virtual ActionResult UpdateUser([Bind(Exclude="Password, ConfirmPassword")]SaveUserViewModel userViewModel)
        {
            return _serviceAction.Put(this)
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
            return _serviceAction.Fetch(this)
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
            return _serviceAction.Put(this)
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
            return _serviceAction.Put(this)
                        .WithCommand(
                                () =>
                                {
                                    return _userService.UserLockStatus(id, isLocked);
                                })
                        .Execute();
        }

        //[HttpGet]
        //public virtual ActionResult Accounts()
        //{
        //    return new ServiceAction(this)
        //                .Fetch()
        //                .WithContent<IList<UserViewModel>>(MVC.Admin.Views._users,
        //                        () =>
        //                        {
        //                            var loggedInUser = Session.GetValue<LoggedUserInfoDto>(SessionKey.LoggedUser);
        //                            var users = _adminService.GetAllUsers(loggedInUser.GroupId, loggedInUser.IsSysAccount);
        //                            return Mapper.Map<IList<UserViewModel>>(users);
        //                        })
        //                .Execute();
        //}

        //[HttpGet]
        //public virtual ActionResult AddAccounts()
        //{
        //    return new ServiceAction(this)
        //                .Fetch()
        //                .WithPopup<SaveUserViewModel>(MVC.Admin.Views._addUser,
        //                        () =>
        //                        {
        //                            var groups = (from gp in _adminService.GetAllGroups()
        //                                          where !gp.IsSysAccount
        //                                          select new SelectListItem { Value = gp.Id.ToString(), Text = gp.Name }).ToList();
        //                            groups.Insert(0, new SelectListItem { Value = "", Text = "Please Select ..." });

        //                            ViewData["Groups"] = groups;
        //                            return new SaveUserViewModel();
        //                        })
        //                .Execute();
        //}

        //[HttpPost]
        //public virtual ActionResult AddAccounts([Bind(Exclude = "Id")]SaveAccountViewModel accountViewModel)
        //{
        //    return new ServiceAction(this)
        //                .Put()
        //                .WithCommand(
        //                        () =>
        //                        {
        //                            return _adminService.AddUser(accountViewModel.Firstname, accountViewModel.Lastname, accountViewModel.Nickname,
        //                                accountViewModel.Username, accountViewModel.GroupId);
        //                        })
        //                .Execute();
        //}

        //[HttpGet]
        //public virtual ActionResult UpdateAccounts(Guid id)
        //{
        //    return new ServiceAction(this)
        //                .Fetch()
        //                .WithResult<SaveUserViewModel>(MVC.Admin.Views._upgradeUser,
        //                        () =>
        //                        {
        //                            var groups = (from gp in _adminService.GetAllGroups()
        //                                          where !gp.IsSysAccount
        //                                          select new SelectListItem { Value = gp.Id.ToString(), Text = gp.Name }).ToList();
        //                            groups.Insert(0, new SelectListItem { Value = "", Text = "Please Select ..." });

        //                            ViewData["Groups"] = groups;
        //                            var user = _adminService.GetSingleUser(id);
        //                            return Mapper.Map<SaveUserViewModel>(user);
        //                        })
        //                .Execute();
        //}

        //[HttpPost]
        //public virtual ActionResult UpdateAccounts([Bind(Exclude = "Password, ConfirmPassword")]SaveAccountViewModel accountViewModel)
        //{
        //    return new ServiceAction(this)
        //                .Put()
        //                .WithCommand(
        //                        () =>
        //                        {
        //                            return _adminService.UpdateUser(accountViewModel.Id, accountViewModel.Firstname, accountViewModel.Lastname,
        //                                accountViewModel.Nickname, accountViewModel.Username, accountViewModel.GroupId);
        //                        })
        //                .Execute();
        //}

        //[HttpDelete]
        //public virtual ActionResult DeleteAccounts(Guid id)
        //{
        //    return new ServiceAction(this)
        //                .Fetch()
        //                .WithResult<MessageResultDto>(
        //                        () =>
        //                        {
        //                            return _adminService.DeleteUser(id);
        //                        })
        //                .Execute();
        //}
    }
}
