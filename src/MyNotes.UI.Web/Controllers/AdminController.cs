namespace MyNotes.UI.Web.Controllers
{
    using System;
    using System.Web.Mvc;
    using MyNotes.UI.Web.AdminServiceRef;
    using MyNotes.UI.Web.Setup.ActionApi;
    using MyNotes.UI.Web.Setup.Common;
    using System.Collections.Generic;
    using AutoMapper;
    using MyNotes.UI.Web.Setup.Extensions;
    using MyNotes.UI.Web.ViewModels.Admin.Group;
    using MyNotes.UI.Web.ViewModels.Admin.User;
    using MyNotes.UI.Web.UserServiceRef;
    using System.Linq;

    public partial class AdminController : Controller
    {
        IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public virtual ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public virtual ActionResult Groups()
        {
            return new ServiceAction(this)
                        .Fetch(SessionKey.Empty)
                        .WithContent<IList<GroupViewModel>>(MVC.Admin.Views._groups,
                                () =>
                                {
                                    var groups = _adminService.GetAllGroups();
                                    return Mapper.Map<IList<GroupViewModel>>(groups);
                                })
                        .Execute();
        }

        [HttpGet]
        public virtual ActionResult Users()
        {
            return new ServiceAction(this)
                        .Fetch(SessionKey.Empty)
                        .WithContent<IList<UserViewModel>>(MVC.Admin.Views._users,
                                () =>
                                {
                                    var loggedInUser = Session.GetValue<LoggedUserInfoDto>(SessionKey.LoggedUser);
                                    var users = _adminService.GetAllUsers(loggedInUser.GroupId, loggedInUser.IsSysAccount);
                                    return Mapper.Map<IList<UserViewModel>>(users);
                                })
                        .Execute();
        }

        [HttpGet]
        public virtual ActionResult AddGroup()
        {
            return new ServiceAction(this)
                        .Fetch(SessionKey.Empty)
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
            return new ServiceAction(this)
                        .Put(SessionKey.Empty)
                        .WithCommand(
                                () =>
                                {
                                    return _adminService.AddGroup(groupViewModel.Name);
                                })
                        .Execute();
        }

        [HttpGet]
        public virtual ActionResult UpdateGroup(Guid id)
        {
            return new ServiceAction(this)
                        .Fetch(SessionKey.Empty)
                        .WithResult<SaveGroupViewModel>(MVC.Admin.Views._upgradeGroup,
                                () =>
                                {
                                    var group = _adminService.GetSingleGroup(id);
                                    return Mapper.Map<SaveGroupViewModel>(group);
                                })
                        .Execute();
        }

        [HttpPost]
        public virtual ActionResult UpdateGroup(SaveGroupViewModel groupViewModel)
        {
            return new ServiceAction(this)
                        .Put(SessionKey.Empty)
                        .WithCommand(
                                () =>
                                {
                                    return _adminService.UpdateGroup(groupViewModel.Id, groupViewModel.Name);
                                })
                        .Execute();
        }

        [HttpDelete]
        public virtual ActionResult DeleteGroup(Guid id)
        {
            return new ServiceAction(this)
                        .Fetch(SessionKey.Empty)
                        .WithResult<MessageResultDto>(
                                () =>
                                {
                                    return _adminService.DeleteGroup(id);
                                })
                        .Execute();
        }

        [HttpGet]
        public virtual ActionResult AddUser()
        {
            return new ServiceAction(this)
                        .Fetch(SessionKey.Empty)
                        .WithPopup<AddUserViewModel>(MVC.Admin.Views._addUser,
                                () =>
                                {
                                    var groups = (from gp in _adminService.GetAllGroups()
                                                    where !gp.IsSysAccount
                                                    select new SelectListItem { Value = gp.Id.ToString(), Text = gp.Name }).ToList();
                                    groups.Insert(0, new SelectListItem { Value = null, Text = "Please Select ..." });

                                    ViewData["Groups"] = groups;
                                    return new AddUserViewModel();
                                })
                        .Execute();
        }

        [HttpPost]
        public virtual ActionResult AddUser([Bind(Exclude="Id")]AddUserViewModel userViewModel)
        {
            return new ServiceAction(this)
                        .Put(SessionKey.Empty)
                        .WithCommand(
                                () =>
                                {
                                    return _adminService.AddUser(userViewModel.Firstname, userViewModel.Lastname, userViewModel.Nickname,
                                        userViewModel.Username, userViewModel.Password, userViewModel.GroupId);
                                })
                        .Execute();
        }

        [HttpGet]
        public virtual ActionResult UpdateUser(Guid id)
        {
            return new ServiceAction(this)
                        .Fetch(SessionKey.Empty)
                        .WithResult<UpdateUserViewModel>(MVC.Admin.Views._upgradeUser,
                                () =>
                                {
                                    var groups = (from gp in _adminService.GetAllGroups()
                                                  where !gp.IsSysAccount
                                                  select new SelectListItem { Value = gp.Id.ToString(), Text = gp.Name }).ToList();
                                    groups.Insert(0, new SelectListItem { Value = null, Text = "Please Select ..." });

                                    ViewData["Groups"] = groups;
                                    var user = _adminService.GetSingleUser(id);
                                    return Mapper.Map<UpdateUserViewModel>(user);
                                })
                        .Execute();
        }

        [HttpPost]
        public virtual ActionResult UpdateUser([Bind(Exclude="Password, ConfirmPassword")]UpdateUserViewModel userViewModel)
        {
            return new ServiceAction(this)
                        .Put(SessionKey.Empty)
                        .WithCommand(
                                () =>
                                {
                                    return _adminService.UpdateUser(userViewModel.Id, userViewModel.Firstname, userViewModel.Lastname, 
                                        userViewModel.Nickname, userViewModel.Username, userViewModel.GroupId);
                                })
                        .Execute();
        }

        [HttpDelete]
        public virtual ActionResult DeleteUser(Guid id)
        {
            return new ServiceAction(this)
                        .Fetch(SessionKey.Empty)
                        .WithResult<MessageResultDto>(
                                () =>
                                {
                                    return _adminService.DeleteUser(id);
                                })
                        .Execute();
        }
    }
}
